using FlexLabs.Web.TablePager;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FlexLabs.Web.Html
{
    public static class PagingExtensions
    {
        public static MvcHtmlString EnableFormSorting<TModel>(this HtmlHelper<TModel> html)
            where TModel : ITableModel
        {
            var model = html.ViewData.Model;
            String result = String.Empty;

            if (model.SortBy != null)
            {
                var sortBy = new TagBuilder("input");
                sortBy.MergeAttribute("type", "hidden");
                sortBy.MergeAttribute("name", "SortBy");
                sortBy.MergeAttribute("value", model.SortBy.ToString());
                result += sortBy.ToString(TagRenderMode.SelfClosing);
            }

            if (model.SortAsc != null)
            {
                var sortAsc = new TagBuilder("input");
                sortAsc.MergeAttribute("type", "hidden");
                sortAsc.MergeAttribute("name", "SortAsc");
                sortAsc.MergeAttribute("value", model.SortAsc.ToString());
                result += sortAsc.ToString(TagRenderMode.SelfClosing);
            }

            if (model.FirstItemID != null)
            {
                var firstItemID = new TagBuilder("input");
                firstItemID.MergeAttribute("type", "hidden");
                firstItemID.MergeAttribute("name", "FirstItemID");
                firstItemID.MergeAttribute("value", model.FirstItemID.ToString());
                result += firstItemID.ToString(TagRenderMode.SelfClosing);
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString PageSizer<TModel>(this HtmlHelper<TModel> html, String lavel = "Page Size: ", Int32[] pageSizes = null, Int32? currentSize = null)
            where TModel : ITableModel
        {
            var label = html.LabelFor(m => m.PageSize);
            var editor = html.DropDownListFor(m => m.PageSize, TableModel.GetPageSizes(pageSizes, currentSize), new { onchange = "$(this).closest('form').submit();" });
            var validation = html.ValidationMessageFor(m => m.PageSize);

            return MvcHtmlString.Create(label.ToString() + editor.ToString() + validation.ToString());
        }

        public static MvcHtmlString Pager<TModel>(this HtmlHelper<TModel> html, PagedListData pageData, String label = "Page: ")
            where TModel : ITableModel
        {
            var labelTag = new TagBuilder("label");
            labelTag.SetInnerText(label);

            var ulTag = new TagBuilder("ul");

            if (!pageData.CanSeeFirstPage())
                ulTag.InnerHtml += PagerLink(1);

            foreach (var page in pageData.PageRange)
            {
                if (page == pageData.PageNumber)
                    ulTag.InnerHtml += $"<li class=\"page-current\">{page}</li>";
                else
                    ulTag.InnerHtml += PagerLink(page);
            }

            if (!pageData.CanSeeLastPage())
                ulTag.InnerHtml += PagerLink(pageData.PageCount);

            var divTag = new TagBuilder("div");
            divTag.AddCssClass("table-pager");
            divTag.InnerHtml += labelTag.ToString();
            divTag.InnerHtml += ulTag;
            return MvcHtmlString.Create(divTag.ToString());
        }

        private static String PagerLink(Int32 pageNumber)
        {
            return $"<li><button type=\"submit\" name=\"page\" value=\"{pageNumber}\">{pageNumber}</button></li>";
        }

        public static MvcHtmlString Pager<TModel>(this HtmlHelper<TModel> html, String label = "Page: ")
            where TModel : ITableModel
        {
            if (html.ViewData.Model?.PageItems?.PageCount == 0)
                return null;

            var pageItems = html.ViewData.Model.PageItems;
            var pageData = new PagedListData(pageItems.PageNumber, pageItems.PageSize, pageItems.PageCount, pageItems.TotalItemCount);
            return Pager(html, pageData, label);
        }
    }
}
