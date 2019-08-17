using System;
using System.Collections.Generic;
using GraphQL.Types;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    [DefaultGraphQLValueResolver]
    public class DateValueResolver : GraphQLValueResolver<PublishedPropertyType>
    {
        public override Type GetGraphQLType(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(DateTime) ||
                   propertyType.ClrType == typeof(DateTimeOffset)
                ? typeof(DateGraphType)
                : typeof(ListGraphType<DateGraphType>);
        }

        public override bool IsResolver(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(DateTime) ||
                   propertyType.ClrType == typeof(DateTimeOffset) ||
                   propertyType.ClrType == typeof(IEnumerable<DateTime>) ||
                   propertyType.ClrType == typeof(IEnumerable<DateTimeOffset>);
        }
    }
}
