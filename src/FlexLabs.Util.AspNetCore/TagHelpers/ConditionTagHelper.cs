﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlexLabs.AspNetCore.TagHelpers
{
    [HtmlTargetElement(Attributes = ConditionAttribute)]
    public class ConditionTagHelper : TagHelper
    {
        private const string ConditionAttribute = "fl-condition";

        [HtmlAttributeName(ConditionAttribute)]
        public bool? Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(ConditionAttribute);

            if (Condition == false)
                output.SuppressOutput();
        }
    }
}
