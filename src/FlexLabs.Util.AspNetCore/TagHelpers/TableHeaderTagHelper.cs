using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace FlexLabs.Util.AspNetCore.TagHelpers
{
    [HtmlTargetElement("th", Attributes = SortByAttribute, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TableHeaderTagHelper : TagHelper
    {
        public const String SortByAttribute = "fl-sortby";

        [HtmlAttributeName(SortByAttribute)]
        public Object SortBy { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(SortByAttribute);

            if (SortBy == null) return;

            var button = new TagBuilder("button");
            button.MergeAttribute("type", "submit");
            button.MergeAttribute("name", "changesort");
            button.MergeAttribute("value", SortBy.ToString());
            var contents = await output.GetChildContentAsync();
            contents.CopyTo(button.InnerHtml);

            output.Content.SetHtmlContent(button);
        }
    }
}
