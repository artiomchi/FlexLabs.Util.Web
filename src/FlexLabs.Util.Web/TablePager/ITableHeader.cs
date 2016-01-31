using System;

namespace FlexLabs.Web.TablePager
{
    public interface ITableHeader
    {
        String CssClass { get; set; }
        Int32? ColSpan { get; set; }
        Int32? RowSpan { get; set; }
    }
}
