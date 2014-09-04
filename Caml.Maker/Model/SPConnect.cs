using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Caml.Maker.Model
{
    abstract public class SPConnect
    {
        public string Url { get; set; }
        public System.Net.ICredentials Credentials { get; set; }

        public string WebUrl
        {
            get
            {
                if (string.IsNullOrEmpty(Url)) return null;

                string inputUrl = Url.Trim().ToLower();
                if (!inputUrl.StartsWith("http://") && !inputUrl.StartsWith("https://")) inputUrl = "http://" + inputUrl;

                Uri uri = new Uri(inputUrl);
                if (uri.Segments.Last().EndsWith(".aspx"))
                {
                    uri = new Uri(uri.ToString().Replace(uri.Segments.Last(), string.Empty));
                }
                if (new string[] { "_layouts/", "sitepages/", "pages/" }.Contains(uri.Segments.Last().ToLower()))
                {
                    uri = new Uri(uri.ToString().Replace(uri.Segments.Last(), string.Empty));
                }

                return uri.ToString();
            }
        }

        protected System.Drawing.Image GetIconFromWeb(string webUrl, string imageUrl)
        {
            string fileName = System.IO.Path.GetFileName(imageUrl);
            if (!System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons/" + fileName)))
            {
                try
                {
                    #region Get icon image from web
                    Uri imageUri = new Uri(new Uri(webUrl), imageUrl);
                    WebRequest request = WebRequest.Create(imageUri);
                    WebResponse response = request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    byte[] buffer = ReadFully(stream);

                    FileInfo file = new System.IO.FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons/" + fileName));
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

        byte[] ReadFully(System.IO.Stream input)
        {
            byte[] buffer = new byte[512];
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

        abstract public IEnumerable<SPList> GetListCollection();

        abstract public SPList GetFields(SPList list);

        abstract public SPListItemCollection GetListItems(string listName, string viewName, string query, string[] viewFields, string rowLimit, QueryOptions queryOptions, string webID);
    }
}
