using System;
using System.Collections.Generic;
using GraphQL.Types;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    [DefaultGraphQLValueResolver]
    public class FloatValueResolver : GraphQLValueResolver<PublishedPropertyType>
    {
        public override Type GetGraphQLType(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(float)
                ? typeof(FloatGraphType)
                : typeof(ListGraphType<FloatGraphType>);
        }

        public override bool IsResolver(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(float) ||
                   propertyType.ClrType == typeof(IEnumerable<float>);
        }
    }
}
