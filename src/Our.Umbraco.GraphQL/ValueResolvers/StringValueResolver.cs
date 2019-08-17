using System;
using System.Collections.Generic;
using GraphQL.Types;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    [DefaultGraphQLValueResolver]
    public class StringValueResolver : GraphQLValueResolver<PublishedPropertyType>
    {
        public override Type GetGraphQLType(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(string) 
                ? typeof(StringGraphType)
                : typeof(ListGraphType<StringGraphType>);
        }

        public override bool IsResolver(PublishedPropertyType propertyType)
        {
            return propertyType.ClrType == typeof(string) ||
                   propertyType.ClrType == typeof(IEnumerable<string>);
        }
    }
}
