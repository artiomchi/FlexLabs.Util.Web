using System;

namespace FlexLabs.Util.Web
{
    public sealed class AutoDropDownListAttribute : Attribute
    {
        /// <summary>
        /// Defines the settings to initialise the DropDownList with
        /// </summary>
        /// <param name="optionsFieldName">The field in the model class that contains the IEnumerable&lt;SelectListItem&gt;</param>
        public AutoDropDownListAttribute(String optionsFieldName)
        {
            OptionsFieldName = optionsFieldName;
        }

        public String OptionsFieldName { get; private set; }
        public String OptionsLabel { get; set; }
    }
}
