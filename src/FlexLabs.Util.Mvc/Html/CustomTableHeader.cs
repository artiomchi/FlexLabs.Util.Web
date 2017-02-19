using System;
using System.Web;

namespace FlexLabs.Mvc.Html
{
    /// <summary>
    /// Represents a single table header in a sortable table. Use this class to customize the contents of the header
    /// </summary>
    public class CustomTableHeader : ITableHeader
    {
        public Func<object, IHtmlString> Content { get; set; }
        public string CssClass { get; set; }
        public int? ColSpan { get; set; }
        public int? RowSpan { get; set; }
        public bool IsRendered { get; set; } = true;
    }
}
