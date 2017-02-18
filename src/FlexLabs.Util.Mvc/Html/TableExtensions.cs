using System.Collections.Generic;
using System.Web.Mvc;
using FlexLabs.Web;

namespace FlexLabs.Mvc.Html
{
    public static class TableExtensions
    {
        public static MvcHtmlString TableHeader(this HtmlHelper html, IEnumerable<ITableHeader> headers)
        {
            return TableHeader(html, new[] { headers });
        }

        public static MvcHtmlString TableHeader(this HtmlHelper html, IEnumerable<IEnumerable<ITableHeader>> headersSet)
        {
            var model = html.ViewData.Model as ITableModel;

            var theadTag = new TagBuilder("thead");

            foreach (var headers in headersSet)
            {
                var rowTag = new TagBuilder("tr");
                foreach (var iheader in headers)
                {
                    if (!iheader.IsRendered) continue;

                    var thTag = new TagBuilder("th");
                    if (iheader.CssClass != null)
                        thTag.AddCssClass(iheader.CssClass);
                    if (iheader.ColSpan.HasValue)
                        thTag.MergeAttribute("colspan", iheader.ColSpan.ToString());
                    if (iheader.RowSpan.HasValue)
                        thTag.MergeAttribute("rowspan", iheader.RowSpan.ToString());

                    if (iheader is CustomTableHeader)
                    {
                        thTag.InnerHtml = (iheader as CustomTableHeader).Content(html.ViewData.Model).ToString();
                    }
                    else
                    {
                        var header = iheader as TableHeader;
                        if (header.ToolTip != null)
                            thTag.MergeAttribute("title", header.ToolTip);

                        if (header.Value != null)
                        {
                            var sortBy = header.Value?.ToString() == model.GetSortBy()?.ToString() && model.GetSortAsc()
                                ? $"!{header.Value}"
                                : header.Value.ToString();

                            var buttonTag = new TagBuilder("button");
                            buttonTag.MergeAttribute("type", "submit");
                            buttonTag.MergeAttribute("name", "changeSort");
                            buttonTag.MergeAttribute("value", sortBy);
                            buttonTag.SetInnerText(header.Title);
                            thTag.InnerHtml = buttonTag.ToString();
                        }
                        else
                        {
                            thTag.SetInnerText(header.Title);
                        }
                    }

                    rowTag.InnerHtml += thTag.ToString();
                }
                theadTag.InnerHtml += rowTag.ToString();
            }

            return MvcHtmlString.Create(theadTag.ToString());
        }
    }
}
