using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model.CM
{
    public class ClientModelConnector : SPConnect
    {
        public override IEnumerable<SPList> GetListCollection()
        {
            ClientContext ctx = new ClientContext(WebUrl);
            ListCollection lists = ctx.Web.Lists;
            ctx.Load(lists);
            ctx.ExecuteQuery();

            List<SPList> values = new List<SPList>();
            foreach (List list in lists)
            {
                SPList value = new SPList()
                {
                    DefaultViewUrl = list.DefaultViewUrl,
                    ID = list.Id,
                    Title = list.Title,
                    Description = list.Description,
                    Image = GetIconFromWeb(Url, list.ImageUrl),
                    ImageKey = System.IO.Path.GetFileNameWithoutExtension(list.ImageUrl),
                    Hidden = list.Hidden
                };

                values.Add(value);
            }

            return values;
        }

        public override SPList GetFields(SPList source)
        {
            ClientContext ctx = new ClientContext(WebUrl);
            List list = ctx.Web.Lists.GetById(source.ID);
            ctx.Load(list);
            ctx.Load(list.Fields);
            ctx.ExecuteQuery();

            List<SPField> values = new List<SPField>();
            foreach (Field field in list.Fields)
            {
                SPField value = new SPField();
                value.Attributes["CanBeDeleted"] = field.CanBeDeleted.ToString();
                value.Attributes["Description"] = field.Description.ToString();
                value.Attributes["Direction"] = field.Direction.ToString();
                value.Attributes["EnforceUniqueValues"] = field.EnforceUniqueValues.ToString();
                value.Attributes["FieldTypeKind"] = field.FieldTypeKind.ToString();
                value.Attributes["Filterable"] = field.Filterable.ToString();
                value.Attributes["FromBaseType"] = field.FromBaseType.ToString();
                value.Attributes["Group"] = field.Group.ToString();
                value.Attributes["Hidden"] = field.Hidden.ToString();
                value.Attributes["ID"] = field.Id.ToString();
                value.Attributes["Name"] = field.InternalName.ToString();
                value.Attributes["ReadOnly"] = field.ReadOnlyField.ToString();
                value.Attributes["Required"] = field.Required.ToString();
                value.Attributes["Scope"] = field.Scope.ToString();
                value.Attributes["Sealed"] = field.Sealed.ToString();
                value.Attributes["Sortable"] = field.Sortable.ToString();
                value.Attributes["StaticName"] = field.StaticName.ToString();
                value.Attributes["DisplayName"] = field.Title.ToString();
                value.Attributes["Type"] = field.TypeDisplayName.ToString();
                value.Attributes["TypeShortDescription"] = field.TypeShortDescription.ToString();

                values.Add(value);
            }

            source.Fields = values.ToArray();
            return source;
        }

        public override SPListItemCollection GetListItems(string listName, string viewName, string query, string[] viewFields, string rowLimit, QueryOptions queryOptions, string webID)
        {
            SPListItemCollection collection = new SPListItemCollection();

            ClientContext ctx = new ClientContext(WebUrl);
            List list = ctx.Web.Lists.GetByTitle(listName);
            CamlQuery caml = new CamlQuery();
            caml.ViewXml = query;
            ListItemCollection items = list.GetItems(caml);
            ctx.Load(items);
            ctx.ExecuteQuery();

            List<SPListItem> values = new List<SPListItem>();
            foreach (ListItem item in items)
            {
                SPListItem value = new SPListItem();
                foreach (string name in item.FieldValues.Keys)
                {
                    if (item.FieldValues[name] == null)
                    {
                        value.Attributes[name] = null;
                    }
                    else
                    {
                        object fv = item.FieldValues[name];
                        if (fv is FieldUserValue)
                        {
                            FieldUserValue v = fv as FieldUserValue;
                            value.Attributes[name] = v.LookupValue;
                        }
                        else if (fv is FieldUserValue[])
                        {
                            FieldUserValue[] v = fv as FieldUserValue[];
                            value.Attributes[name] = string.Join(", ", v.Select(ff => ff.LookupValue).ToArray());
                        }
                        else if (fv is FieldLookupValue)
                        {
                            FieldLookupValue v = fv as FieldLookupValue;
                            value.Attributes[name] = v.LookupValue;
                        }
                        else if (fv is FieldLookupValue[])
                        {
                            FieldLookupValue[] v = fv as FieldLookupValue[];
                            value.Attributes[name] = string.Join(", ", v.Select(ff => ff.LookupValue).ToArray());
                        }
                        else
                        {
                            value.Attributes[name] = item.FieldValues[name].ToString();
                        }
                    }
                }
                values.Add(value);
                collection.Items = values.ToArray();
            }

            return collection;
        }
    }
}
