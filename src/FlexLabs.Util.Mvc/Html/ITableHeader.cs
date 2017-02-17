namespace FlexLabs.Mvc.Html
{
    public interface ITableHeader
    {
        string CssClass { get; set; }
        int? ColSpan { get; set; }
        int? RowSpan { get; set; }
        bool IsRendered { get; set; }
    }
}
