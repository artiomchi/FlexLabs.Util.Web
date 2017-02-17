using System;

namespace FlexLabs.Web
{
    public sealed class AutoDropDownListAttribute : Attribute
    {
        /// <summary>
        /// Defines the settings to initialise the DropDownList with
        /// </summary>
        /// <param name="optionsFieldName">The field in the model class that contains the IEnumerable&lt;SelectListItem&gt;</param>
        public AutoDropDownListAttribute(string optionsFieldName)
        {
            OptionsFieldName = optionsFieldName;
        }

        public string OptionsFieldName { get; private set; }
        public string OptionsLabel { get; set; }
    }
}
