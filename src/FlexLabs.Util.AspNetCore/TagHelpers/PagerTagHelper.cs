using FlexLabs.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.AspNetCore.TagHelpers
{
    /// <summary>
    /// Tag helpers that renders a list with page number button elements
    /// </summary>
    [OutputElementHint("ul")]
    [HtmlTargetElement("fl-pager", TagStructure = TagStructure.WithoutEndTag)]
    public class PagerTagHelper : TableModelTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pageData = new PagedListData(Model.PageItems);
            var inputName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName("Page");

            if (!pageData.CanSeeFirstPage())
                output.Content.AppendHtml(RenderPage(pageData, inputName, 1));
            foreach (var page in pageData.PageRange)
                output.Content.AppendHtml(RenderPage(pageData, inputName, page));
            if (!pageData.CanSeeLastPage())
                output.Content.AppendHtml(RenderPage(pageData, inputName, pageData.PageCount));

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "ul";
        }

        private TagBuilder RenderPage(PagedListData pageData, string inputName, int page)
        {
            var liTag = new TagBuilder("li");
            if (page == pageData.PageNumber)
            {
                liTag.AddCssClass("page-current");
                liTag.InnerHtml.Append(page.ToString());
            }
            else
            {
                var buttonTag = new TagBuilder("button");
                buttonTag.MergeAttribute("type", "submit");
                buttonTag.MergeAttribute("name", inputName);
                buttonTag.MergeAttribute("value", page.ToString());
                buttonTag.InnerHtml.Append(page.ToString());
                liTag.InnerHtml.AppendHtml(buttonTag);
            }
            return liTag;
        }
    }
}
