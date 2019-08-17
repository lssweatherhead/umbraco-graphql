using System;
using System.Collections.Generic;
using GraphQL.Types;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    [DefaultGraphQLValueResolver]
    public class IntValueResolver : GraphQLValueResolver<PublishedPropertyType>
    {
        public override Type GetGraphQLType(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(short) ||
                   propertyType.ClrType == typeof(int) ||
                   propertyType.ClrType == typeof(long)
                ? typeof(IntGraphType)
                : typeof(ListGraphType<IntGraphType>);
        }

        public override bool IsResolver(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(short) ||
                   propertyType.ClrType == typeof(int) ||
                   propertyType.ClrType == typeof(long) ||
                   propertyType.ClrType == typeof(IEnumerable<short>) ||
                   propertyType.ClrType == typeof(IEnumerable<int>) ||
                   propertyType.ClrType == typeof(IEnumerable<long>);
        }
    }
}
