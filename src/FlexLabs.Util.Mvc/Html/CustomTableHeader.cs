using System;
using System.Web;

namespace FlexLabs.Mvc.Html
{
    public class CustomTableHeader : ITableHeader
    {
        public Func<object, IHtmlString> Content { get; set; }
        public string CssClass { get; set; }
        public int? ColSpan { get; set; }
        public int? RowSpan { get; set; }
        public bool IsRendered { get; set; } = true;
    }
}
