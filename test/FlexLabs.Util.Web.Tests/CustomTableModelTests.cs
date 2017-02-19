using FlexLabs.Util.Web.Tests.Models;
using System.Linq;
using Xunit;

namespace FlexLabs.Util.Web.Tests
{
    class CustomTableModelTests
    {
        [Theory]
        [InlineData(TestEntitySorter.ID, true, true)]
        [InlineData(TestEntitySorter.ID, false, true)]
        [InlineData(TestEntitySorter.ID, true, false)]
        [InlineData(TestEntitySorter.ID, false, false)]
        [InlineData(TestEntitySorter.Name, false, false)]
        public void TableModel_ContructorParamsSet(TestEntitySorter sortBy, bool sortAsc, bool enablePaging)
        {
            var model = new TestEntityModel(sortBy, sortAsc, enablePaging);
            Assert.Equal(TestEntitySorter.ID, model.DefaultSortBy);
            Assert.Equal(false, model.DefaultSortAsc);
            Assert.Equal(false, model.PagingEnabled);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 20)]
        [InlineData(3, 5)]
        public void TableModel_NoPaging(int? page, int? pageSize)
        {
            var model = new TestEntityModel(TestEntitySorter.ID, true, false);
            model.SetPageItems(DefaultTableModelTests.SampleDataSet);
            Assert.Equal(DefaultTableModelTests.SampleDataSet.Length, model.PageItems.Count);
            Assert.Collection(model.PageItems, DefaultTableModelTests.SampleDataSet.Select(DefaultTableModelTests.EntityMatcher).ToArray());
        }
    }
}
