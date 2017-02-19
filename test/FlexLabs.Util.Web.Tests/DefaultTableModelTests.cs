using FlexLabs.Util.Web.Tests.Models;
using System;
using System.Linq;
using Xunit;

namespace FlexLabs.Util.Web.Tests
{
    public class DefaultTableModelTests
    {
        public const TestEntitySorter DefaultSortBy = TestEntitySorter.ID;
        public const bool DefaultSortAsc = true;
        public const int PageSize = 30;

        private TestEntityModel _model;
        public DefaultTableModelTests()
        {
            _model = new TestEntityModel(DefaultSortBy, DefaultSortAsc, true);
        }

        public static readonly TestEntity[] SampleDataSet = Enumerable.Range(1, 1000)
            .Select(i => new TestEntity
            {
                ID = i,
                Name = $"Entity {i}",
                Created = new DateTime(2000, 1, 1).AddDays(i),
            })
            .ToArray();

        [Fact]
        public void TableModel_GetSortByDefault()
        {
            Assert.Equal(DefaultSortBy, _model.GetSortBy());
        }

        [Fact]
        public void TableModel_GetSortAscDefault()
        {
            Assert.Equal(DefaultSortAsc, _model.GetSortAsc());
        }

        [Fact]
        public void TableModel_GetSortBySet()
        {
            _model.SortBy = TestEntitySorter.Name;
            Assert.Equal(TestEntitySorter.Name, _model.GetSortBy());
        }

        [Fact]
        public void TableModel_GetSortAscSet()
        {
            _model.SortAsc = false;
            Assert.Equal(false, _model.GetSortAsc());
        }

        [Fact]
        public void TableModel_ChangeSortAscending()
        {
            _model.ChangeSort = TestEntitySorter.Created.ToString();
            Assert.Equal(TestEntitySorter.Created, _model.GetSortBy());
            Assert.Equal(true, _model.GetSortAsc());
        }

        [Fact]
        public void TableModel_ChangeSortDescending()
        {
            _model.ChangeSort = "!" + TestEntitySorter.Created;
            Assert.Equal(TestEntitySorter.Created, _model.GetSortBy());
            Assert.Equal(false, _model.GetSortAsc());
        }

        public static Action<TestEntity> EntityMatcher(TestEntity entity)
            => e => Assert.Equal(entity, e);

        [Theory]
        [InlineData(null)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void TableModel_Paging(int? page)
        {
            _model.Page = page;
            _model.PageSize = PageSize;
            _model.SetPageItems(SampleDataSet);
            Assert.Equal(PageSize, _model.PageItems.Count);
            Assert.Collection(_model.PageItems, SampleDataSet.Skip(((page ?? 1) - 1) * PageSize).Take(PageSize).Select(EntityMatcher).ToArray());
        }

        [Fact]
        public void TableModel_FirstItemID_FirstPageNoID()
        {
            var firstItemID = _model.GetFirstItemID(i => i.ID);
            _model.SetPageItems(SampleDataSet);

            Assert.Null(firstItemID);
            Assert.Equal(1, _model.FirstItemID);
        }

        [Fact]
        public void TableModel_GettingFirstItemID()
        {
            var firstItemID = 37;
            _model.FirstItemID = firstItemID;

            Assert.Null(_model.GetFirstItemID(i => i.ID));
        }

        [Fact]
        public void TableModel_GettingFirstItemID_Paging()
        {
            var firstItemID = 37;
            _model.FirstItemID = firstItemID;
            _model.Page = 2;

            Assert.Equal(firstItemID, _model.GetFirstItemID(i => i.ID));
        }

        [Fact]
        public void TableModel_GettingFirstItemID_Sorting()
        {
            var firstItemID = 37;
            _model.FirstItemID = firstItemID;
            _model.ChangeSort = TestEntitySorter.ID.ToString();

            Assert.Equal(firstItemID, _model.GetFirstItemID(i => i.ID));
        }

        [Fact]
        public void TableModel_StoringFirstItemID_Page2()
        {
            var firstItemID = 37;
            _model.FirstItemID = 37;
            _model.Page = 2;

            Assert.Equal(firstItemID, _model.GetFirstItemID(i => i.ID));
            _model.SetPageItems(SampleDataSet);
            Assert.Equal(firstItemID, _model.FirstItemID);
        }
    }
}
