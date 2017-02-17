using System;

namespace FlexLabs.Web
{
    public interface ITableModel
    {
        void UpdateSorter();
        object SortBy { get; set; }
        bool? SortAsc { get; set; }
        long? FirstItemID { get; set; }
        int? PageSize { get; set; }

        IPagedList PageItems { get; }
    }
}
