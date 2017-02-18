using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.AspNetCore.TagHelpers
{

    [HtmlTargetElement("form", Attributes = SortableAttribute, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SortableFormTagHelper : TableModelTagHelper
    {
        public const string SortableAttribute = "fl-sortable";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(SortableAttribute);

            if (Model.GetSortBy()?.ToString() != Model.DefaultSortBy?.ToString())
            {
                var inputName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(nameof(Model.SortBy));
                var inputID = TagBuilder.CreateSanitizedId(inputName, "_");
                var sortByTag = new TagBuilder("input");
                sortByTag.TagRenderMode = TagRenderMode.SelfClosing;
                sortByTag.MergeAttribute("type", "hidden");
                sortByTag.MergeAttribute("name", inputName);
                sortByTag.MergeAttribute("id", inputID);
                sortByTag.MergeAttribute("value", Model.GetSortBy().ToString());
                output.PreContent.AppendHtml(sortByTag);
            }

            if (Model.GetSortAsc() != Model.DefaultSortAsc)
            {
                var inputName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(nameof(Model.SortAsc));
                var inputID = TagBuilder.CreateSanitizedId(inputName, "_");
                var sortAscTag = new TagBuilder("input");
                sortAscTag.TagRenderMode = TagRenderMode.SelfClosing;
                sortAscTag.MergeAttribute("type", "hidden");
                sortAscTag.MergeAttribute("name", inputName);
                sortAscTag.MergeAttribute("id", inputID);
                sortAscTag.MergeAttribute("value", Model.GetSortAsc().ToString());
                output.PreContent.AppendHtml(sortAscTag);
            }

            if (Model.FirstItemID != null)
            {
                var inputName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(nameof(Model.FirstItemID));
                var inputID = TagBuilder.CreateSanitizedId(inputName, "_");
                var firstItemIDTag = new TagBuilder("input");
                firstItemIDTag.TagRenderMode = TagRenderMode.SelfClosing;
                firstItemIDTag.MergeAttribute("type", "hidden");
                firstItemIDTag.MergeAttribute("name", inputName);
                firstItemIDTag.MergeAttribute("id", inputID);
                firstItemIDTag.MergeAttribute("value", Model.FirstItemID.ToString());
                output.PreContent.AppendHtml(firstItemIDTag);
            }
        }
    }
}
