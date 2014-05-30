using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Caml.Maker.Model
{
    public class SharePointList : System.Windows.Forms.ListViewItem
    {
        /// <summary>
        /// Get lists
        /// </summary>
        /// <param name="url"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static IEnumerable<SharePointList> GetListCollection(string url, System.Net.ICredentials credentials)
        {
            // Connect to sharepoint site web service
            using (var service = SharePointList.GetServiceInstance(url, credentials))
            {
                #region Get lists
                var response = service.GetListCollection();
                if (response != null)
                {
                    LogHandler.Clear();
                    LogHandler.Log("{0}", response.OuterXml);

                    var xdoc = response.ToXDocument();

                    var items = xdoc.Document.Root.Nodes()
                                    .Cast<XElement>().Select(n => new SharePointList()
                                    {
                                        DefaultViewUrl = n.Attribute("DefaultViewUrl").Value,
                                        ID = n.Attribute("ID").Value,
                                        Title = n.Attribute("Title").Value,
                                        Description = n.Attribute("Description").Value,
                                        Image = GetIconFromWeb(service.Url, n.Attribute("ImageUrl").Value),
                                        ImageKey = System.IO.Path.GetFileNameWithoutExtension(n.Attribute("ImageUrl").Value),
                                        Hidden = Convert.ToBoolean(n.Attribute("Hidden").Value)
                                    })
                                    .OrderBy(n => n.Title);

                    return items.ToArray();
                }
                #endregion
            }

            return null;
        }

        public static DataTable GetList(string url, System.Net.ICredentials credentials, string listName, out SharePointField[] fieldsOut)
        {
            #region Get list info
            using (var service = SharePointList.GetServiceInstance(url, credentials))
            {
                var response = service.GetList(listName);
                if (response != null)
                {
                    LogHandler.Clear();
                    LogHandler.Log("{0}", response.OuterXml);

                    var fieldList = new List<SharePointField>();
                    var xdoc = response.ToXDocument();
                    var fieldElement = xdoc.Document.Root.Nodes().OfType<XElement>().First();
                    var dt = new DataTable();

                    #region Gather column names
                    var columns = new HashSet<string>();
                    fieldElement.Nodes().OfType<XElement>().Each(field =>
                    {
                        field.Attributes().Each<XAttribute>(attr =>
                        {
                            columns.Add(attr.Name.LocalName);
                        });
                    });
                    #endregion

                    #region Create columns
                    string[] preColumns = new string[] { "ID", "DisplayName", "Name", "StaticName", "Type", "Required", "ReadOnly", "Hidden" };
                    preColumns.Each(c =>
                    {
                        if (columns.Contains(c))
                        {
                            dt.Columns.Add(c);
                            columns.Remove(c);
                        }
                    });
                    columns.OrderBy(c => c).Each(c => dt.Columns.Add(c));
                    #endregion

                    #region Insert row
                    fieldElement.Nodes()
                                .OfType<XElement>()
                                .OrderBy(f => f.Attribute("DisplayName").Value)
                                .Each(field =>
                                {
                                    var row = dt.Rows.Add();

                                    dt.Columns.Each<DataColumn>(col =>
                                    {
                                        var attr = field.Attribute(col.Caption);
                                        if (attr != null)
                                        {
                                            row[attr.Name.LocalName] = attr.Value;
                                        }
                                    });

                                    #region Generate field selector
                                    if (field.Attribute("Hidden") != null && field.Attribute("Hidden").Value == "TRUE") return ActionResult.Continue;
                                    if (field.Attribute("Type") != null && (field.Attribute("Type").Value == "Computed" || field.Attribute("Type").Value == "Attachments")) return ActionResult.Continue;

                                    var f = new SharePointField();
                                    if (field.Attribute("DisplayName") != null) f.DisplayName = field.Attribute("DisplayName").Value;
                                    if (field.Attribute("Name") != null) f.InternalName = field.Attribute("Name").Value;
                                    if (field.Attribute("Type") != null) f.Type = field.Attribute("Type").Value;

                                    switch (field.Attribute("Type").Value)
                                    {
                                        case "Boolean":
                                            f.Values = new object[] { true, false };
                                            break;
                                        case "Choice":
                                            var choices = field.Nodes().OfType<XElement>().FirstOrDefault(n => n.Name.LocalName == "CHOICES");
                                            if (choices != null)
                                            {
                                                var vs = new List<object>();
                                                choices.Nodes().OfType<XElement>().Each(n => vs.Add(n.Value));
                                                f.Values = vs.ToArray();
                                            }
                                            break;
                                        case "Lookup":
                                            break;
                                        case "Attachments":
                                        case "Calculated":
                                        case "Currency":
                                        case "DateTime":
                                        case "Note":
                                        case "Number":
                                        case "Text":
                                        case "URL":
                                        case "User":
                                        default:
                                            break;
                                    }

                                    fieldList.Add(f);
                                    #endregion

                                    return ActionResult.None;
                                });
                    #endregion

                    fieldsOut = fieldList.ToArray();
                    return dt;
                }
            }
            #endregion

            fieldsOut = null;
            return null;
        }

        public static DataTable GetListItems(string url, System.Net.ICredentials credentials,
            string listName, string viewName, string query, string[] viewFields, string rowLimit, QueryOptions queryOptions, string webID)
        {
            using (var service = SharePointList.GetServiceInstance(url, credentials))
            {
                var xmlDoc = new System.Xml.XmlDocument();

                var ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", string.Empty);
                ndQuery.InnerXml = query.Replace("<Query>", string.Empty).Replace("</Query>", string.Empty);

                XmlNode ndViewFields = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", string.Empty);
                if (viewFields != null && viewFields.Length > 0)
                {
                    ndViewFields.InnerXml = string.Join(string.Empty, viewFields.Select(f => string.Format(@"<FieldRef Name='{0}' />", f)).ToArray());
                }

                XmlNode ndQueryOptions = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", string.Empty);
                if (queryOptions != null)
                {
                    ndQueryOptions.InnerXml = queryOptions.ToString();
                }

                if (viewName == null) viewName = string.Empty;

                var response = service.GetListItems(listName, viewName, ndQuery, ndViewFields, rowLimit, ndQueryOptions, webID);
                if (response != null)
                {
                    LogHandler.Clear();
                    LogHandler.Log("{0}", response.OuterXml);

                    var xdoc = response.ToXDocument();
                    var fieldElement = xdoc.Document.Root.Nodes().OfType<XElement>().First();
                    var dt = new DataTable();
                    var columnsTemp = new HashSet<string>();
                    var columns = new List<string>();

                    #region Gather column names
                    fieldElement.Nodes().OfType<XElement>().Each<XElement>(field =>
                    {
                        field.Attributes().Each<XAttribute>(attr =>
                        {
                            columnsTemp.Add(attr.Name.LocalName);
                        });
                    });
                    #endregion

                    #region Create columns
                    columns.AddRange(columnsTemp.Except(columnsTemp.Where(c => c.StartsWith("ows__"))).OrderBy(c => c));
                    columns.AddRange(columnsTemp.Where(c => c.StartsWith("ows__")).OrderBy(c => c));
                    columns.Each(c => dt.Columns.Add(c));
                    #endregion

                    #region Insert row
                    fieldElement.Nodes().OfType<XElement>().Each<XElement>(node =>
                    {
                        var dr = dt.Rows.Add();
                        node.Attributes().Each<XAttribute>(attr =>
                        {
                            dr[attr.Name.LocalName] = attr.Value;
                        });
                    });

                    // Mapping column name
                    dt.Columns.Each<DataColumn>(c =>
                    {
                        c.ColumnName = c.ColumnName.Replace("ows_", string.Empty);
                        var field = RowItem.AllField.OfType<SharePointField>().FirstOrDefault(f => f.InternalName == c.ColumnName);
                        if (field != null) c.Caption = field.DisplayName;
                    });
                    #endregion

                    return dt;
                }
            }
            return null;
        }

        static ListWebService.Lists GetServiceInstance(string url, System.Net.ICredentials credentials)
        {
            return new ListWebService.Lists()
            {
                Credentials = credentials,
                Url = GetServiceUrl(url)
            };
        }

        public static string GetServiceUrl(string input)
        {
            string inputUrl = input.Trim().ToLower();
            if (!inputUrl.StartsWith("http")) inputUrl = "http://" + inputUrl;

            Uri uri = new Uri(inputUrl);
            if (uri.Segments.Last().EndsWith(".aspx"))
            {
                uri = new Uri(uri.ToString().Replace(uri.Segments.Last(), string.Empty));
            }
            if (new string[] { "_layouts/", "sitepages/", "pages/" }.Contains(uri.Segments.Last().ToLower()))
            {
                uri = new Uri(uri.ToString().Replace(uri.Segments.Last(), string.Empty));
            }

            return string.Format("{0}{1}_vti_bin/Lists.asmx", uri, uri.ToString().EndsWith("/") ? string.Empty : "/");
        }

        static System.Drawing.Image GetIconFromWeb(string webUrl, string imageUrl)
        {
            var fileName = System.IO.Path.GetFileName(imageUrl);
            if (!System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons/" + fileName)))
            {
                try
                {
                    #region Get icon image from web
                    var imageUri = new Uri(new Uri(webUrl), imageUrl);
                    var request = System.Net.WebRequest.Create(imageUri);
                    var response = request.GetResponse();
                    var stream = response.GetResponseStream();
                    byte[] buffer = ReadFully(stream);

                    var file = new System.IO.FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons/" + fileName));
                    if (!file.Directory.Exists) file.Directory.Create();

                    System.IO.File.WriteAllBytes(
                        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons/" + fileName),
                        buffer
                    );
                    stream.Close();
                    response.Close();
                    #endregion

                    return System.Drawing.Image.FromStream(new System.IO.MemoryStream(buffer));
                }
                catch { }
            }
            return null;
        }

        static byte[] ReadFully(System.IO.Stream input)
        {
            var buffer = new byte[512];
            using (var ms = new System.IO.MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #region Properties
        public string DefaultViewUrl { get; set; }
        public string ID { get; set; }
        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }
        public string Description
        {
            get { return ToolTipText; }
            set { ToolTipText = value; }
        }
        public System.Drawing.Image Image { get; private set; }
        public bool Hidden { get; set; }
        #endregion

        public override string ToString()
        {
            return Title;
        }


    }
}
