using FlexLabs.Util.Web.Tests.Models;
using System.Linq;
using Xunit;

namespace FlexLabs.Util.Web.Tests
{
    class CustomTableModelTests
    {
        [Fact]
        public void TableModel_ContructorParamsSet()
        {
            var model = new TestEntityModel(TestEntitySorter.ID, false, false);
            Assert.Equal(TestEntitySorter.ID, model.DefaultSortBy);
            Assert.Equal(false, model.DefaultSortAsc);
            Assert.Equal(false, model.PagingEnabled);
        }

        [Fact]
        public void TableModel_NoPaging()
        {
            var model = new TestEntityModel(TestEntitySorter.ID, true, false);
            model.SetPageItems(DefaultTableModelTests.SampleDataSet);
            Assert.Equal(DefaultTableModelTests.SampleDataSet.Length, model.PageItems.Count);
            Assert.Collection(model.PageItems, DefaultTableModelTests.SampleDataSet.Select(DefaultTableModelTests.EntityMatcher).ToArray());
        }

        [Fact]
        public void TableModel_NoPaging_Page2() // Page number and size should be ignored
        {
            var model = new TestEntityModel(TestEntitySorter.ID, true, false);
            model.Page = 2;
            model.PageSize = DefaultTableModelTests.PageSize;
            model.SetPageItems(DefaultTableModelTests.SampleDataSet);
            Assert.Equal(DefaultTableModelTests.SampleDataSet.Length, model.PageItems.Count);
            Assert.Collection(model.PageItems, DefaultTableModelTests.SampleDataSet.Select(DefaultTableModelTests.EntityMatcher).ToArray());
        }
    }
}
