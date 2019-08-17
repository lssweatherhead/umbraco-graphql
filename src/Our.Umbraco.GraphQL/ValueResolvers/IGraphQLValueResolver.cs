using System;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    public interface IGraphQLValueResolver<in T> where T : class
    {
        Type GetGraphQLType(T propertyType);
        bool IsResolver(T propertyType);
        //TODO: Provide context?
        object Resolve(T propertyType, object value);
    }
}
