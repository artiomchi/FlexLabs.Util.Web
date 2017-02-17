using FlexLabs.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections.Generic;

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
