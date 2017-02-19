namespace FlexLabs.Mvc.Html
{
    /// <summary>
    /// Represents a single table header in a sortable table
    /// </summary>
    public interface ITableHeader
    {
        string CssClass { get; set; }
        int? ColSpan { get; set; }
        int? RowSpan { get; set; }
        bool IsRendered { get; set; }
    }
}
