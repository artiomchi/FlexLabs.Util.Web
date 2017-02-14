using System;

namespace FlexLabs.Util.Mvc.Html
{
    public interface ITableHeader
    {
        String CssClass { get; set; }
        Int32? ColSpan { get; set; }
        Int32? RowSpan { get; set; }
        Boolean IsRendered { get; set; }
    }
}
