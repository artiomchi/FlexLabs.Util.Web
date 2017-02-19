using System;

namespace FlexLabs.Web
{
    public interface ITableModel
    {
        object DefaultSortBy { get; }
        bool DefaultSortAsc { get; }
        /// <summary>
        /// Stores the selected sort column between requests.
        /// To get which column should the data be sorted by call <see cref="GetSortBy()"/> instead
        /// </summary>
        object SortBy { get; set; }
        /// <summary>
        /// Stores the selected sort direction between requests.
        /// To get which direction should the data be sorted by call <see cref="GetSortAsc()"/> instead
        /// </summary>
        bool? SortAsc { get; set; }
        /// <summary>
        /// The ID of the first item in the data source. Useful to prevent newly added items from shifting results while paging through the data set
        /// </summary>
        long? FirstItemID { get; set; }
        int? PageSize { get; set; }

        /// <summary>
        /// A subset of the dataset representing the current view (page) of the results
        /// </summary>
        IPagedList PageItems { get; }

        /// <summary>
        /// Use this method to get which column should the resultset be sorted by
        /// </summary>
        /// <returns></returns>
        object GetSortBy();
        /// <summary>
        /// Use this method to get which direction should the resultset be sorted by
        /// </summary>
        /// <returns></returns>
        bool GetSortAsc();
    }
}
