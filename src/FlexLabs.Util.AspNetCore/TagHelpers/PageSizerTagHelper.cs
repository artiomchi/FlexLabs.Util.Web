using FlexLabs.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.AspNetCore.TagHelpers
{
    /// <summary>
    /// Tag helper that renders a dropdown to change the page size
    /// </summary>
    [OutputElementHint("select")]
    [HtmlTargetElement("fl-pagesizer", TagStructure = TagStructure.WithoutEndTag)]
    public class PageSizerTagHelper : TableModelTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pageSize = Model.PageItems.PageSize;

            var inputName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(nameof(Model.PageSize));
            var inputID = TagBuilder.CreateSanitizedId(inputName, "_");
            output.Attributes.SetAttribute("name", inputName);
            output.Attributes.SetAttribute("id", inputID);
            output.Attributes.SetAttribute("onchange", "$(this).closest('form').submit();");

            foreach (var size in TableModel.GetPageSizes())
            {
                var optionTag = new TagBuilder("option");
                optionTag.MergeAttribute("value", size.ToString());
                if (size == pageSize)
                    optionTag.MergeAttribute("selected", "selected");
                optionTag.InnerHtml.Append(size.ToString());
                output.Content.AppendHtml(optionTag);
            }

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "select";
        }
    }
}
