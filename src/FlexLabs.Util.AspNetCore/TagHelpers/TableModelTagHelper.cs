using FlexLabs.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace FlexLabs.AspNetCore.TagHelpers
{
    public abstract class TableModelTagHelper : TagHelper
    {
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        protected ITableModel Model
            => ViewContext.ViewData.Model as ITableModel ?? throw new InvalidOperationException("To use this tag helper your model has to derive from TableModel<>, or you have to pass a TableModel<> to the For attribute");
    }
}
