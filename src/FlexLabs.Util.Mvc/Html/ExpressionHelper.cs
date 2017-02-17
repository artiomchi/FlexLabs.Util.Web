using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FlexLabs.Mvc.Html
{
    internal static class ExpressionHelper
    {
        public static MemberInfo GetMemberInfo(Expression expression)
        {
            if (expression == null)
                return null;

            if (expression is LambdaExpression lambdaExpression)
                return GetMemberInfo(lambdaExpression.Body);

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
