using System;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    public abstract class GraphQLValueResolver<T> : IGraphQLValueResolver<T> where T: class
    {
        public abstract Type GetGraphQLType(T propertyType);

        public virtual object Resolve(T propertyType, object value)
        {
            return value;
        }

        public abstract bool IsResolver(T propertyType);
    }
}
