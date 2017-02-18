using FlexLabs.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.AspNetCore.TagHelpers
{
    [OutputElementHint("ul")]
    [HtmlTargetElement("fl-pager", TagStructure = TagStructure.WithoutEndTag)]
    public class PagerTagHelper : TableModelTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pageData = new PagedListData(Model.PageItems);

            var ulTag = new TagBuilder("ul");

            var inputName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName("Page");
            foreach (var page in pageData.PageRange)
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
                ulTag.InnerHtml.AppendHtml(liTag);
            }

            output.TagName = null;
            output.Content.AppendHtml(ulTag);
        }
    }
}
