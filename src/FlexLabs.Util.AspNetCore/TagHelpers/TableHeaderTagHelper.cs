using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace FlexLabs.AspNetCore.TagHelpers
{
    [HtmlTargetElement("th", Attributes = SortByAttribute, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TableHeaderTagHelper : TableModelTagHelper
    {
        private const string SortByAttribute = "fl-sortby";

        [HtmlAttributeName(SortByAttribute)]
        public object SortBy { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(SortByAttribute);

            if (SortBy == null) return;

            var sortBy = SortBy?.ToString() == Model.GetSortBy()?.ToString() && Model.GetSortAsc()
                ? $"!{SortBy}"
                : SortBy.ToString();

            var button = new TagBuilder("button");
            button.MergeAttribute("type", "submit");
            button.MergeAttribute("name", "changeSort");
            button.MergeAttribute("value", sortBy);
            var contents = await output.GetChildContentAsync();
            contents.CopyTo(button.InnerHtml);

            output.Content.SetHtmlContent(button);
        }
    }
}
