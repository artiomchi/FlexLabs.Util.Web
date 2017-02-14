using FlexLabs.Util.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.Util.AspNetCore.TagHelpers
{

    [HtmlTargetElement("form", Attributes = SortableAttribute, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SortableFormTagHelper : TagHelper
    {
        public const string SortableAttribute = "fl-sortable";

        [HtmlAttributeName(SortableAttribute)]
        public ITableModel Sortable { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(SortableAttribute);

            if (Sortable == null) return;

            if (Sortable.SortBy != null)
            {
                var sortBy = new TagBuilder("input");
                sortBy.TagRenderMode = TagRenderMode.SelfClosing;
                sortBy.MergeAttribute("type", "hidden");
                sortBy.MergeAttribute("name", "SortBy");
                sortBy.MergeAttribute("value", Sortable.SortBy.ToString());
                output.PreContent.AppendHtml(sortBy);
            }

            if (Sortable.SortAsc != null)
            {
                var sortAsc = new TagBuilder("input");
                sortAsc.TagRenderMode = TagRenderMode.SelfClosing;
                sortAsc.MergeAttribute("type", "hidden");
                sortAsc.MergeAttribute("name", "SortAsc");
                sortAsc.MergeAttribute("value", Sortable.SortAsc.ToString());
                output.PreContent.AppendHtml(sortAsc);
            }

            if (Sortable.FirstItemID != null)
            {
                var firstItemID = new TagBuilder("input");
                firstItemID.TagRenderMode = TagRenderMode.SelfClosing;
                firstItemID.MergeAttribute("type", "hidden");
                firstItemID.MergeAttribute("name", "FirstItemID");
                firstItemID.MergeAttribute("value", Sortable.FirstItemID.ToString());
                output.PreContent.AppendHtml(firstItemID);
            }
        }
    }
}
