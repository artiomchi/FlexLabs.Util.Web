using FlexLabs.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.AspNetCore.TagHelpers
{
    [OutputElementHint("select")]
    [HtmlTargetElement("fl-pagesizer", TagStructure = TagStructure.WithoutEndTag)]
    public class PageSizerTagHelper : TagHelper
    {
        public ITableModel Model { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pageSize = TableModel.DefaultPageSize;

            var selectTag = new TagBuilder("select");
            selectTag.MergeAttribute("name", "PageSize");
            selectTag.MergeAttribute("onchange", "$(this).closest('form').submit();");

            foreach (var size in TableModel.GetPageSizes())
            {
                var optionTag = new TagBuilder("option");
                optionTag.MergeAttribute("value", size.ToString());
                if (size == pageSize)
                    optionTag.MergeAttribute("selected", "selected");
                optionTag.InnerHtml.Append(size.ToString());
                selectTag.InnerHtml.AppendHtml(optionTag);
            }

            output.TagName = null;
            output.Content.AppendHtml(selectTag);
        }
    }
}
