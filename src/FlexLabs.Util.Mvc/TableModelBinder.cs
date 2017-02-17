using FlexLabs.Web;
using System.Web.Mvc;

namespace FlexLabs.Mvc
{
    public class TableModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = base.BindModel(controllerContext, bindingContext);
            (result as ITableModel)?.UpdateSorter();
            return result;
        }
    }
}
