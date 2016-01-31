using FlexLabs.Web.TablePager;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FlexLabs.Web.Html
{
    public static class TableExtensions
    {
        public static MvcHtmlString TableHeader(this HtmlHelper html, IEnumerable<ITableHeader> headers)
        {
            return TableHeader(html, new[] { headers });
        }

        public static MvcHtmlString TableHeader(this HtmlHelper html, IEnumerable<IEnumerable<ITableHeader>> headersSet)
        {
            var theadTag = new TagBuilder("thead");

            foreach (var headers in headersSet)
            {
                var rowTag = new TagBuilder("tr");
                foreach (var iheader in headers)
                {
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
                        if (header.Value != null)
                        {
                            var buttonTag = new TagBuilder("button");
                            buttonTag.MergeAttribute("type", "submit");
                            buttonTag.MergeAttribute("name", "changeSort");
                            buttonTag.MergeAttribute("value", header.Value.ToString());
                            if (header.ToolTip != null)
                                buttonTag.MergeAttribute("title", header.ToolTip);
                            buttonTag.SetInnerText(header.Title);
                            thTag.InnerHtml = buttonTag.ToString();
                        }
                        else if (header.ToolTip != null)
                        {
                            var span = new TagBuilder("span");
                            span.MergeAttribute("title", header.ToolTip);
                            span.SetInnerText(header.Title);
                            thTag.InnerHtml = span.ToString();
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
