using FlexLabs.Web;
using System;
using System.Linq;
using Xunit;

namespace FlexLabs.Util.Web.Tests
{
    public class PagedListDataTests
    {
        private static Action<int> PageMatcher(int i)
            => j => Assert.Equal(i, j);

        [Fact]
        public void PagedListData_Left()
        {
            int pageNumber = 1, pageSize = 10, pages = 20, totalCount = pageSize * pages;
            var data = Enumerable.Range(1, totalCount);

            var pagedList = data.ToPagedList(pageNumber, pageSize);
            var pagedListData = new PagedListData(pagedList);

            Assert.Equal(pageNumber, pagedListData.PageNumber);
            Assert.Equal(pages, pagedListData.PageCount);
            Assert.Equal(true, pagedListData.CanSeeFirstPage());
            Assert.Equal(false, pagedListData.CanSeeLastPage());
            Assert.Collection(pagedListData.PageRange, Enumerable.Range(pageNumber, TableModel.DefaultPageRange).Select(PageMatcher).ToArray());
        }

        [Fact]
        public void PagedListData_Middle()
        {
            int pageNumber = 10, pageSize = 10, pages = 20, totalCount = pageSize * pages;
            var data = Enumerable.Range(1, totalCount);

            var pagedList = data.ToPagedList(pageNumber, pageSize);
            var pagedListData = new PagedListData(pagedList);

            Assert.Equal(pageNumber, pagedListData.PageNumber);
            Assert.Equal(pages, pagedListData.PageCount);
            Assert.Equal(false, pagedListData.CanSeeFirstPage());
            Assert.Equal(false, pagedListData.CanSeeLastPage());
            Assert.Collection(pagedListData.PageRange, Enumerable.Range(6, TableModel.DefaultPageRange).Select(PageMatcher).ToArray());
        }

        [Fact]
        public void PagedListData_Right()
        {
            int pageNumber = 15, pageSize = 10, pages = 20, totalCount = pageSize * pages;
            var data = Enumerable.Range(1, totalCount);

            var pagedList = data.ToPagedList(pageNumber, pageSize);
            var pagedListData = new PagedListData(pagedList);

            Assert.Equal(pageNumber, pagedListData.PageNumber);
            Assert.Equal(pages, pagedListData.PageCount);
            Assert.Equal(false, pagedListData.CanSeeFirstPage());
            Assert.Equal(true, pagedListData.CanSeeLastPage());
            Assert.Collection(pagedListData.PageRange, Enumerable.Range(11, TableModel.DefaultPageRange).Select(PageMatcher).ToArray());
        }
    }
}
