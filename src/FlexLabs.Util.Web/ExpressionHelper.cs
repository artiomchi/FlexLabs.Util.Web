﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FlexLabs.Web.Html
{
    internal static class ExpressionHelper
    {
        public static MemberInfo GetMemberInfo(Expression expression)
        {
            if (expression == null)
                return null;

            if (expression is LambdaExpression)
                return GetMemberInfo((expression as LambdaExpression).Body);

            switch (expression.NodeType)
            {
                case ExpressionType.ArrayIndex:
                    var binaryExpression = expression as BinaryExpression;
                    return GetMemberInfo(binaryExpression.Left);

                case ExpressionType.MemberAccess:
                    var memberExpression = expression as MemberExpression;
                    return memberExpression.Member;

                default:
                    return null;
            }
        }

        public static TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo)
            where TAttribute: Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(TAttribute), false);
            if (attributes.Length > 0)
                return attributes[0] as TAttribute;

            return null;
        }
    }
}
