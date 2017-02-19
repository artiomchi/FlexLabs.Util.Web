using FlexLabs.Web;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FlexLabs.Mvc.Html
{
    public static class PagingExtensions
    {
        /// <summary>
        /// Adds the necessary elements to the &lt;form&gt; to keep track of which column and direction it is sorted by
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString EnableFormSorting<TModel>(this HtmlHelper<TModel> html)
            where TModel : ITableModel
        {
            var model = html.ViewData.Model;
            string result = String.Empty;

            if (model.GetSortBy()?.ToString() != model.DefaultSortBy?.ToString())
            {
                var sortBy = new TagBuilder("input");
                sortBy.MergeAttribute("type", "hidden");
                sortBy.MergeAttribute("name", "SortBy");
                sortBy.MergeAttribute("value", model.GetSortBy().ToString());
                result += sortBy.ToString(TagRenderMode.SelfClosing);
            }

            if (model.GetSortAsc() != model.DefaultSortAsc)
            {
                var sortAsc = new TagBuilder("input");
                sortAsc.MergeAttribute("type", "hidden");
                sortAsc.MergeAttribute("name", "SortAsc");
                sortAsc.MergeAttribute("value", model.GetSortAsc().ToString());
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

        /// <summary>
        /// Render a dropdown to change the page size, with a label before it
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="label"></param>
        /// <param name="pageSizes"></param>
        /// <param name="currentSize"></param>
        /// <returns></returns>
        public static MvcHtmlString PageSizer<TModel>(this HtmlHelper<TModel> html, string label = "Page Size: ", int[] pageSizes = null, int? currentSize = null)
            where TModel : ITableModel
        {
            var pageSizeItems = TableModel.GetPageSizes(pageSizes, currentSize)
                .Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = i == (currentSize ?? TableModel.DefaultPageSize) });

            var labelTag = html.LabelFor(m => m.PageSize, label);
            var editor = html.DropDownListFor(m => m.PageSize, pageSizeItems, new { onchange = "$(this).closest('form').submit();" });
            var validation = html.ValidationMessageFor(m => m.PageSize);

            return MvcHtmlString.Create(labelTag.ToString() + editor.ToString() + validation.ToString());
        }

        /// <summary>
        /// Renders a list with page number button elements, and a label before it
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="pageData"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static MvcHtmlString Pager<TModel>(this HtmlHelper<TModel> html, PagedListData pageData, string label = "Page: ")
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

        private static string PagerLink(int pageNumber)
        {
            return $"<li><button type=\"submit\" name=\"page\" value=\"{pageNumber}\">{pageNumber}</button></li>";
        }

        /// <summary>
        /// Renders a list with page number button elements, and a label before it
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static MvcHtmlString Pager<TModel>(this HtmlHelper<TModel> html, string label = "Page: ")
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
