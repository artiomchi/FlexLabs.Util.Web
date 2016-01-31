using System.Web.Mvc;

namespace FlexLabs.Web.TablePager
{
    public class TableModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ITableModel tableModel = base.BindModel(controllerContext, bindingContext) as ITableModel;
            if (tableModel != null)
            {
                tableModel.UpdateSorter();
            }
            return tableModel;
        }
    }
}
