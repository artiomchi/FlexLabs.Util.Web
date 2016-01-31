﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FlexLabs.Web.Html
{
    public static class EditorHelpers
    {
        public static MvcHtmlString AutoEditorFieldFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, String templateName = null, Object additionalViewData = null)
        {
            var label = html.LabelFor(expression);
            var validation = html.ValidationMessageFor(expression);

            MvcHtmlString editor = null;
            var member = ExpressionHelper.GetMemberInfo(expression);

            if (editor == null && ExpressionHelper.GetAttribute<AutoDropDownListAttribute>(member) != null)
                editor = html.AutoDropDownListFor(expression);
            if (editor == null && ExpressionHelper.GetAttribute<AutoTextBoxAttribute>(member) != null)
                editor = html.AutoTextBoxFor(expression);
            if (editor == null)
                editor = html.EditorFor(expression, templateName, additionalViewData);

            return MvcHtmlString.Create(label.ToString() + editor.ToString() + validation.ToString());
        }

        public static MvcHtmlString AutoDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes = null)
        {
            var member = ExpressionHelper.GetMemberInfo(expression);
            var attr = ExpressionHelper.GetAttribute<AutoDropDownListAttribute>(member);
            if (attr == null)
                throw new InvalidOperationException("Missing AutoDropDownListAttribute on property");

            var field = member.DeclaringType.GetField(attr.OptionsFieldName);
            if (field == null)
                throw new MissingFieldException($"Could not find field {attr.OptionsFieldName} on type {member.DeclaringType}");
            if (!typeof(IEnumerable<SelectListItem>).IsAssignableFrom(field.FieldType))
                throw new Exception($"Field {attr.OptionsFieldName} is not of type IEnumerable<SelectListItem>");

            var model = html.ViewData.Model;
            var options = field.GetValue(model) as IEnumerable<SelectListItem>;
            return html.DropDownListFor(expression, options, attr.OptionsLabel, htmlAttributes);
        }

        public static MvcHtmlString AutoTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes = null)
        {
            var member = ExpressionHelper.GetMemberInfo(expression);
            var attr = ExpressionHelper.GetAttribute<AutoTextBoxAttribute>(member);
            if (attr == null)
                throw new InvalidOperationException("Missing AutoTextBoxAttribute on property");

            var attributes = htmlAttributes != null
                ? (IDictionary<String, Object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)
                : new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase);

            if (attr.Type != null && !attributes.ContainsKey("type"))
                attributes["type"] = attr.Type;

            return html.TextBoxFor(expression, attr.Format, attributes);
        }
    }
}
