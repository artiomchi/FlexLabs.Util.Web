using System;

namespace FlexLabs.Web
{
    public interface ITableModel
    {
        object DefaultSortBy { get; }
        bool DefaultSortAsc { get; }
        object SortBy { get; set; }
        bool? SortAsc { get; set; }
        long? FirstItemID { get; set; }
        int? PageSize { get; set; }

        IPagedList PageItems { get; }

        object GetSortBy();
        bool GetSortAsc();
    }
}
