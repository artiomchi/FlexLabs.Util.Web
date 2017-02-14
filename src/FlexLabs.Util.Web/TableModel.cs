using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexLabs.Util.Web
{
    public class TableModel
    {
        public static Int32 DefaultPageSize = 25;
        public static Int32 DefaultPageRange = 10;
        public static Int32[] DefaultPageSizes = new[] { 10, 25, 50, 100 };

        public static IEnumerable<Int32> GetPageSizes(Int32[] pageSizes = null, Int32? currentSize = null)
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
        public TableModel(TSorter defaultSorter, Boolean defaultAscending, Boolean pagingEnabled = true)
            : base(defaultSorter, defaultAscending, pagingEnabled)
        { }

        public override TModel TranslateItem(TModel item)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class TableModel<TSorter, TSource, TModel> : TableModel, ITableModel where TSorter : struct
    {
        private readonly TSorter DefaultSortBy;
        private readonly Boolean DefaultSortAsc;
        private readonly Boolean PagingEnabled = true;
        private Func<TSource, Int64> FirstID64Selector = null;
        private Func<TSource, Int32> FirstID32Selector = null;

        public TableModel(TSorter defaultSorter, Boolean defaultAscending, Boolean pagingEnabled = true)
        {
            DefaultSortBy = defaultSorter;
            DefaultSortAsc = defaultAscending;
            PagingEnabled = pagingEnabled;
        }
        public TSorter? ChangeSort { get; set; }
        public TSorter? SortBy { get; set; }
        public Boolean? SortAsc { get; set; }
        public Int32? PageSize { get; set; }
        public Int32? Page { get; set; }
        public Int64? FirstItemID { get; set; }
        object ITableModel.SortBy { get { return SortBy; } set { SortBy = (TSorter?)value; } }

        public IPagedList<TModel> PageItems { get; private set; }
        IPagedList ITableModel.PageItems => PageItems;

        public abstract TModel TranslateItem(TSource item);

        public void SetPageItems(IEnumerable<TSource> items, Int32? totalItemCount = null)
        {
            IEnumerable<TSource> dataSet;
            var pageNumber = Page ?? 1;
            var pageSize = PageSize ?? DefaultPageSize;

            if (PagingEnabled)
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

            if (PageItems.TotalItemCount > 0 && (FirstID32Selector != null || FirstID64Selector != null))
            {
                FirstItemID = FirstID64Selector != null
                    ? dataSet.Select(FirstID64Selector).FirstOrDefault()
                    : dataSet.Select(FirstID32Selector).FirstOrDefault();
            }
        }

        public Int64? GetFirstItemID(Func<TSource, Int64> idSelector)
        {
            var showNewResults = !Page.HasValue && !ChangeSort.HasValue;

            if (showNewResults)
            {
                FirstID64Selector = idSelector;
                return null;
            }

            return FirstItemID;
        }

        public Int32? GetFirstItemID(Func<TSource, Int32> idSelector)
        {
            var showNewResults = !Page.HasValue && !ChangeSort.HasValue;

            if (showNewResults)
            {
                FirstID32Selector = idSelector;
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
                if (ChangeSort.Value.Equals(SortBy ?? DefaultSortBy))
                {
                    SortAsc = !SortAsc.GetValueOrDefault(DefaultSortAsc);
                }
                else
                {
                    SortBy = ChangeSort;
                    SortAsc = true;
                }
                if (SortAsc.HasValue && SortAsc == DefaultSortAsc)
                    SortAsc = null;
                if (SortBy.HasValue && SortBy.Value.Equals(DefaultSortBy))
                    SortBy = null;
            }
        }

        public TSorter GetSortBy() => SortBy ?? DefaultSortBy;
        public Boolean GetSortAsc() => SortAsc ?? DefaultSortAsc;
    }
}
