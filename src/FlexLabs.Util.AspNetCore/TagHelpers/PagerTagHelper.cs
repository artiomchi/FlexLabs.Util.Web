using FlexLabs.Util.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.Util.AspNetCore.TagHelpers
{
    [OutputElementHint("ul")]
    [HtmlTargetElement("fl-pager", TagStructure = TagStructure.WithoutEndTag)]
    public class PagerTagHelper : TagHelper
    {
        public PagedListData PageData { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pageSize = TableModel.DefaultPageSize;

            var ulTag = new TagBuilder("ul");

            foreach (var page in PageData.PageRange)
            {
                var liTag = new TagBuilder("li");
                if (page == PageData.PageNumber)
                {
                    liTag.AddCssClass("page-current");
                    liTag.InnerHtml.Append(page.ToString());
                }
                else
                {
                    var buttonTag = new TagBuilder("button");
                    buttonTag.MergeAttribute("type", "submit");
                    buttonTag.MergeAttribute("name", "page");
                    buttonTag.MergeAttribute("value", page.ToString());
                    buttonTag.InnerHtml.Append(page.ToString());
                    liTag.InnerHtml.AppendHtml(buttonTag);
                }
                ulTag.InnerHtml.AppendHtml(liTag);
            }

            // add label and validation?

            output.TagName = null;
            output.Content.AppendHtml(ulTag);
        }
    }
}
