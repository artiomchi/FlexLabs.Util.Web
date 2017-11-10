using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlexLabs.Web
{
    public static class TableModel
    {
        /// <summary>
        /// Default page size to be used when paging results
        /// </summary>
        public static int DefaultPageSize = 25;
        /// <summary>
        /// Default number of page links to be rendered by the pager
        /// </summary>
        public static int DefaultPageRange = 10;
        /// <summary>
        /// Default page sizes to be displayed by the page sizer
        /// </summary>
        public static int[] DefaultPageSizes = new[] { 10, 25, 50, 100 };

        public static IEnumerable<int> GetPageSizes(int[] pageSizes = null, int? currentSize = null)
        {
            if (pageSizes == null)
                pageSizes = DefaultPageSizes;
            if (currentSize.HasValue && !pageSizes.Contains(currentSize.Value))
                pageSizes = pageSizes.Union(new[] { currentSize.Value }).OrderBy(p => p).ToArray();
            return pageSizes;
        }
    }

    /// <summary>
    /// Base class for view models that will display a pageable/sortable set
    /// </summary>
    /// <typeparam name="TSorter">An enum type that identifies which column to sort by</typeparam>
    /// <typeparam name="TModel">The model type. A collection of these will be passed to the model</typeparam>
    public abstract class TableModel<TSorter, TModel> : TableModel<TSorter, TModel, TModel>, ITableModel where TSorter : struct
    {
        public TableModel(TSorter defaultSorter, bool defaultAscending, bool pagingEnabled = true)
            : base(defaultSorter, defaultAscending, pagingEnabled)
        { }

        public override TModel TranslateItem(TModel item)
            => throw new NotImplementedException();
    }

    /// <summary>
    /// Base class for view models that will display a pageable/sortable set with a custom display model class for your data
    /// </summary>
    /// <typeparam name="TSorter">An enum type that identifies which column to sort by</typeparam>
    /// <typeparam name="TSource">The model type. A collection of these will be passed to the model</typeparam>
    /// <typeparam name="TModel">Your <typeparamref name="TSource"/> will be converted to a <typeparamref name="TModel"/> before being passed to the view</typeparam>
    public abstract class TableModel<TSorter, TSource, TModel> :  ITableModel where TSorter : struct
    {
        private Func<TSource, long> _firstID64Selector = null;
        private Func<TSource, int> _firstID32Selector = null;

        public TableModel(TSorter defaultSorter, bool defaultAscending, bool pagingEnabled = true)
        {
            DefaultSortBy = defaultSorter;
            DefaultSortAsc = defaultAscending;
            PagingEnabled = pagingEnabled;
        }

        public TSorter DefaultSortBy { get; }
        public bool DefaultSortAsc { get; }
        public bool PagingEnabled { get; }

        /// <summary>
        /// Used by the TableModel to change the sorting of the data
        /// </summary>
        public string ChangeSort { get; set; }
        /// <summary>
        /// Stores the selected sort column between requests.
        /// To get which column should the data be sorted by call <see cref="GetSortBy()"/> instead
        /// </summary>
        public TSorter? SortBy { get; set; }
        /// <summary>
        /// Stores the selected sort direction between requests.
        /// To get which direction should the data be sorted by call <see cref="GetSortAsc()"/> instead
        /// </summary>
        public bool? SortAsc { get; set; }
        public int? PageSize { get; set; }
        /// <summary>
        /// Current page number (1 based)
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// The ID of the first item in the data source. Useful to prevent newly added items from shifting results while paging through the data set
        /// </summary>
        public long? FirstItemID { get; set; }

        /// <summary>
        /// A subset of the dataset representing the current view (page) of the results
        /// </summary>
        public IPagedList<TModel> PageItems { get; private set; }

        /// <summary>
        /// If <typeparamref name="TSource"/> and <typeparamref name="TModel"/> are different, this method will be used to convert the model items
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract TModel TranslateItem(TSource item);

        /// <summary>
        /// Populate the model with the FULL dataset to be paged. Usually this will be an <see cref="IQueryable"/> set, so that paging will be performed on the server side
        /// </summary>
        /// <param name="items">FULL dataset to be paged</param>
        /// <param name="totalItemCount">Optional value that represents the total number of items in your dataset. If null, the TableModel will run items.Count() to get that</param>
        public void SetPageItems(IEnumerable<TSource> items, int? totalItemCount = null)
            => SetPageItems(() => items, totalItemCount);

        /// <summary>
        /// Populate the model with the FULL dataset to be paged. Usually this will be an <see cref="IQueryable"/> set, so that paging will be performed on the server side
        /// </summary>
        /// <param name="itemsSource">FULL dataset to be paged</param>
        /// <param name="totalItemCount">Optional value that represents the total number of items in your dataset. If null, the TableModel will run items.Count() to get that</param>
        public void SetPageItems(Func<IEnumerable<TSource>> itemsSource, int? totalItemCount = null)
        {
            IEnumerable<TSource> dataSet;
            var pageNumber = Page ?? 1;
            var pageSize = PageSize ?? TableModel.DefaultPageSize;

            if (PagingEnabled)
            {
                if (!totalItemCount.HasValue)
                {
                    var pagedItems = itemsSource.ToPagedList(pageNumber, pageSize);
                    totalItemCount = pagedItems.TotalItemCount;
                    dataSet = pagedItems;
                }
                else
                {
                    dataSet = itemsSource().AsQueryable().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            else
            {
                dataSet = itemsSource().ToList();
                pageSize = dataSet.Count() + 1;
            }

            if (typeof(TSource) == typeof(TModel))
                PageItems = new PagedList<TModel>(pageNumber, pageSize, dataSet.OfType<TModel>(), totalItemCount ?? dataSet.Count());
            else
                PageItems = new PagedList<TModel>(pageNumber, pageSize, dataSet.Select(i => TranslateItem(i)), totalItemCount ?? dataSet.Count());

            if (PageItems.TotalItemCount > 0 && (_firstID32Selector != null || _firstID64Selector != null))
            {
                FirstItemID = _firstID64Selector != null
                    ? dataSet.Select(_firstID64Selector).FirstOrDefault()
                    : dataSet.Select(_firstID32Selector).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get the cached ID of the first item in your result set. Has to be called before (!) the call to <see cref="SetPageItems(IEnumerable{TSource}, int?)"/>.
        /// The result of this call can be used to filter the results from the data source, so that no items after this ID are selected (assuming the query defaults to descending order by ID)
        /// </summary>
        /// <param name="idSelector"></param>
        /// <returns>Null if no id was cached (the case with the first page load), and the first cached ID if requested during paging/sorting queries</returns>
        public long? GetFirstItemID(Func<TSource, long> idSelector)
        {
            var showNewResults = !Page.HasValue && string.IsNullOrEmpty(ChangeSort);

            if (showNewResults)
            {
                _firstID64Selector = idSelector;
                return null;
            }

            return FirstItemID;
        }

        /// <summary>
        /// Get the cached ID of the first item in your result set. Has to be called before (!) the call to <see cref="SetPageItems(IEnumerable{TSource}, int?)"/>.
        /// The result of this call can be used to filter the results from the data source, so that no items after this ID are selected (assuming the query defaults to descending order by ID)
        /// </summary>
        /// <param name="idSelector"></param>
        /// <returns>Null if no id was cached (the case with the first page load), and the first cached ID if requested during paging/sorting queries</returns>
        public int? GetFirstItemID(Func<TSource, int> idSelector)
        {
            var showNewResults = !Page.HasValue && string.IsNullOrEmpty(ChangeSort);

            if (showNewResults)
            {
                _firstID32Selector = idSelector;
                return null;
            }

            if (FirstItemID.HasValue)
                return Convert.ToInt32(FirstItemID.Value);
            return null;
        }

        private static bool IsTypeEnum(Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsEnum;
#else
            return type.IsEnum;
#endif
        }

        /// <summary>
        /// Use this method to get which column should the resultset be sorted by
        /// </summary>
        /// <returns></returns>
        public TSorter GetSortBy()
        {
            if (!string.IsNullOrEmpty(ChangeSort))
            {
                var changeSort = ChangeSort.TrimStart('!');
                return IsTypeEnum(typeof(TSorter))
                    ? (TSorter)Enum.Parse(typeof(TSorter), changeSort)
                    : (TSorter)Convert.ChangeType(changeSort, typeof(TSorter));
            }
            return SortBy ?? DefaultSortBy;
        }

        /// <summary>
        /// Use this method to get which direction should the resultset be sorted by
        /// </summary>
        /// <returns></returns>
        public bool GetSortAsc()
            => !string.IsNullOrEmpty(ChangeSort)
                ? ChangeSort[0] != '!'
                : SortAsc ?? DefaultSortAsc;

        #region ITableModel
        object ITableModel.DefaultSortBy => DefaultSortBy;
        object ITableModel.SortBy { get => SortBy; set => SortBy = (TSorter?)value; }
        IPagedList ITableModel.PageItems => PageItems;
        object ITableModel.GetSortBy() => GetSortBy();
        #endregion
    }
}
