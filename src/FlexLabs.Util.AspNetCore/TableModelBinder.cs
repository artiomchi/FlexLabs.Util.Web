using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using FlexLabs.Web;

namespace FlexLabs.AspNetCore
{
    public class TableModelBinder : ComplexTypeModelBinder
    {
        public TableModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders) : base(propertyBinders) { }

        protected override object CreateModel(ModelBindingContext bindingContext)
        {
            var result = base.CreateModel(bindingContext);
            (result as ITableModel)?.UpdateSorter();
            return result;
        }
    }
}
