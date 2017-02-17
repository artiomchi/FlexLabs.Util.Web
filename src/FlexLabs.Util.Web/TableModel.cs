using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexLabs.Web
{
    public class TableModel
    {
        public static int DefaultPageSize = 25;
        public static int DefaultPageRange = 10;
        public static int[] DefaultPageSizes = new[] { 10, 25, 50, 100 };

        public static IEnumerable<int> GetPageSizes(int[] pageSizes = null, int? currentSize = null)
        {
            if (pageSizes == null)
                pageSizes = TableModel.DefaultPageSizes;
            if (currentSize.HasValue && !pageSizes.Contains(currentSize.Value))
                pageSizes = pageSizes.Union(new[] { currentSize.Value }).OrderBy(p => p).ToArray();
            return pageSizes;
        }
    }

    public abstract class TableModel<TSorter, TModel> : TableModel<TSorter, TModel, TModel>, ITableModel where TSorter : struct
    {
        public TableModel(TSorter defaultSorter, bool defaultAscending, bool pagingEnabled = true)
            : base(defaultSorter, defaultAscending, pagingEnabled)
        { }

        public override TModel TranslateItem(TModel item)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class TableModel<TSorter, TSource, TModel> : TableModel, ITableModel where TSorter : struct
    {
        private readonly TSorter _defaultSortBy;
        private readonly bool _defaultSortAsc;
        private readonly bool _pagingEnabled = true;
        private Func<TSource, long> _firstID64Selector = null;
        private Func<TSource, int> _firstID32Selector = null;

        public TableModel(TSorter defaultSorter, bool defaultAscending, bool pagingEnabled = true)
        {
            _defaultSortBy = defaultSorter;
            _defaultSortAsc = defaultAscending;
            _pagingEnabled = pagingEnabled;
        }
        public TSorter? ChangeSort { get; set; }
        public TSorter? SortBy { get; set; }
        public bool? SortAsc { get; set; }
        public int? PageSize { get; set; }
        public int? Page { get; set; }
        public long? FirstItemID { get; set; }
        object ITableModel.SortBy { get { return SortBy; } set { SortBy = (TSorter?)value; } }

        public IPagedList<TModel> PageItems { get; private set; }
        IPagedList ITableModel.PageItems => PageItems;

        public abstract TModel TranslateItem(TSource item);

        public void SetPageItems(IEnumerable<TSource> items, int? totalItemCount = null)
        {
            IEnumerable<TSource> dataSet;
            var pageNumber = Page ?? 1;
            var pageSize = PageSize ?? DefaultPageSize;

            if (_pagingEnabled)
            {
                if (!totalItemCount.HasValue)
                {
                    var pagedItems = items.ToPagedList(pageNumber, pageSize);
                    totalItemCount = pagedItems.TotalItemCount;
                    dataSet = pagedItems;
                }
                else
                {
                    dataSet = items.AsQueryable().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            else
            {
                dataSet = items.ToList();
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

        public long? GetFirstItemID(Func<TSource, long> idSelector)
        {
            var showNewResults = !Page.HasValue && !ChangeSort.HasValue;

            if (showNewResults)
            {
                _firstID64Selector = idSelector;
                return null;
            }

            return FirstItemID;
        }

        public int? GetFirstItemID(Func<TSource, int> idSelector)
        {
            var showNewResults = !Page.HasValue && !ChangeSort.HasValue;

            if (showNewResults)
            {
                _firstID32Selector = idSelector;
                return null;
            }

            if (FirstItemID.HasValue)
                return Convert.ToInt32(FirstItemID.Value);
            return null;
        }

        public void UpdateSorter()
        {
            if (ChangeSort.HasValue)
            {
                if (ChangeSort.Value.Equals(SortBy ?? _defaultSortBy))
                {
                    SortAsc = !SortAsc.GetValueOrDefault(_defaultSortAsc);
                }
                else
                {
                    SortBy = ChangeSort;
                    SortAsc = true;
                }
                if (SortAsc.HasValue && SortAsc == _defaultSortAsc)
                    SortAsc = null;
                if (SortBy.HasValue && SortBy.Value.Equals(_defaultSortBy))
                    SortBy = null;
            }
        }

        public TSorter GetSortBy() => SortBy ?? _defaultSortBy;
        public bool GetSortAsc() => SortAsc ?? _defaultSortAsc;
    }
}
