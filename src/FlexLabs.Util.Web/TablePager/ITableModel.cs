using PagedList;
using System;

namespace FlexLabs.Web.TablePager
{
    public interface ITableModel
    {
        void UpdateSorter();
        Object SortBy { get; set; }
        Boolean? SortAsc { get; set; }
        Int64? FirstItemID { get; set; }
        Int32? PageSize { get; set; }

        IPagedList PageItems { get; }
    }
}
