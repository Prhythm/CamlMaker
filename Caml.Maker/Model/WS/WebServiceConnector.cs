using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Caml.Maker.Model.WS
{
    public class WebServiceConnector : SPConnect
    {
        ListWebService.Lists ServiceInstance
        {
            get
            {
                return new ListWebService.Lists()
                {
                    Credentials = Credentials,
                    Url = ServiceUrl
                };
            }
        }

        public string ServiceUrl
        {
            get
            {
                string url = WebUrl;
                return string.Format("{0}{1}_vti_bin/Lists.asmx", url, url.EndsWith("/") ? string.Empty : "/");
            }
        }

        public override IEnumerable<SPList> GetListCollection()
        {
            // Connect to sharepoint site web service
            using (var service = ServiceInstance)
            {
                #region Get lists
                XmlNode response = service.GetListCollection();
                if (response != null)
                {
                    LogHandler.Clear();
                    LogHandler.Log("{0}", response.OuterXml);

                    return response.ChildNodes
                        .OfType<XmlElement>()
                        .Select(n => new SPList()
                        {
                            DefaultViewUrl = n.GetAttribute("DefaultViewUrl"),
                            ID = new Guid(n.GetAttribute("ID")),
                            Title = n.GetAttribute("Title"),
                            Description = n.GetAttribute("Description"),
                            Image = GetIconFromWeb(service.Url, n.GetAttribute("ImageUrl")),
                            ImageKey = System.IO.Path.GetFileNameWithoutExtension(n.GetAttribute("ImageUrl")),
                            Hidden = Convert.ToBoolean(n.GetAttribute("Hidden"))
                        })
                        .OrderBy(n => n.Title);
                }
                #endregion
            }

            return null;
        }

        public override SPList GetFields(SPList list)
        {
            #region Get list info
            using (var service = ServiceInstance)
            {
                XmlNode response = service.GetList(list.Title);
                if (response != null)
                {
                    LogHandler.Clear();
                    LogHandler.Log(response.OuterXml);

                    List<SPField> fieldList = new List<SPField>();
                    XNode node = response.ToXDocument();
                    XmlNodeList fieldNodes = response.FirstChild.ChildNodes;
                    fieldNodes.OfType<XmlElement>().Each(n =>
                    {
                        SPField field = new SPField();

                        n.Attributes.OfType<XmlAttribute>().Each(a =>
                        {
                            field.Attributes.Add(a.LocalName, a.Value);
                        });

                        fieldList.Add(field);
                    });

                    list.Fields = fieldList.ToArray();
                }
            }
            #endregion

            return list;
        }

        public override SPListItemCollection GetListItems(string listName, string viewName, string query, string[] viewFields, string rowLimit, QueryOptions queryOptions, string webID)
        {
            SPListItemCollection collection = new SPListItemCollection();

            using (var service = ServiceInstance)
            {
                XmlDocument xmlDoc = new System.Xml.XmlDocument();

                XmlNode ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", string.Empty);
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

                XmlNode response = service.GetListItems(listName, viewName, ndQuery, ndViewFields, rowLimit, ndQueryOptions, webID);
                if (response != null)
                {
                    LogHandler.Clear();
                    LogHandler.Log(response.OuterXml);

                    XmlElement data = response.ChildNodes.OfType<XmlElement>().First() as XmlElement;
                    collection.ListItemCollectionPositionNext = data.GetAttribute("ListItemCollectionPositionNext");

                    collection.Items = data.ChildNodes
                        .OfType<XmlElement>()
                        .Select(n =>
                        {
                            SPListItem item = new SPListItem();

                            n.Attributes.OfType<XmlAttribute>()
                                .Each(a =>
                                {
                                    item.Attributes[a.LocalName] = a.Value;
                                });

                            return item;
                        })
                        .ToArray();
                }
            }

            return collection;
        }
    }
}
