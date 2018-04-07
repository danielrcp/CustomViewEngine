using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CustomViewEngine.ViewEngine
{
    public class CustomView : IView
    {
        public string ViewFilePath { get; set; }
        public object Model { get; set; }
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            Model = viewContext.ViewData.Model;
            var ViewFileContent = File.ReadAllText(ViewFilePath);
            string HTMLContent = GetHtml(ViewFileContent);
            writer.Write(HTMLContent);
        }

        private string GetHtml(string viewFileContent)
        {
            string Result = viewFileContent;
            string PatternControl = "<" + 
                                    "(?<Control>[a-z+])"+
                                    ".*"+
                                    "?/>";
            Result = Regex.Replace(viewFileContent,PatternControl,ControlCodeGenerator,RegexOptions.IgnoreCase);
            return Result;
        }

        private string ControlCodeGenerator(Match currentMatch)
        {
            string Value = currentMatch.Value;
            string Control = currentMatch.Result("${Control}");
            switch (Control.ToLower())
            {
                case "datagrid":
                    if (Model!=null)
                    {
                        Value = RenderDataGrid(Value);
                    }
                    break;
                case "time":
                    Value = DateTime.Now.ToShortTimeString();
                    break;
            }
            return Value;
        }

        private string RenderDataGrid(string value)
        {
            string Pattern = "{Binding\\s+}"+
                             "(?<Items>[a-z_][a-z0-9_]*)"+
                             "\\s*}";
            var r = new Regex(Pattern, RegexOptions.IgnoreCase);
            var m = r.Match(value);
            if (m.Success)
            {
                string BindingExpression = m.Value;
                string ItemsPropertyName = m.Result("${Items}");

                var sb = new StringBuilder();
                try
                {
                    var Items = Model.GetType().GetProperty(ItemsPropertyName).GetValue(Model) as IEnumerable;
                    if (Items != null)
                    {
                        sb.Append("<table class=\"DataGrid\">");
                        string[] PropertyNames = null;
                        bool Alt = false;
                        foreach (var item in Items)
                        {
                            if (PropertyNames == null)
                            {
                                PropertyNames = item.GetType().GetProperties().Select(p => p.Name).ToArray();
                                sb.Append("<tr>");
                                foreach (var e in PropertyNames)
                                {
                                    sb.AppendFormat("<th>{0}</th>", e);
                                }
                                sb.Append("</tr>");
                            }
                            sb.AppendFormat("<tr {0}",Alt ? "class=\"Alt\"":string.Empty);
                            foreach (var e in PropertyNames)
                            {
                                sb.AppendFormat("<td>{0}</td>",item.GetType().GetProperty(e).GetValue(item));
                            }
                            sb.Append("</tr>");
                            Alt = !Alt;
                        }
                        sb.Append("</table>");
                        value = sb.ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return value;
        }
    }
}