using System.Collections.Generic;
using GraphQL.Types;
using Our.Umbraco.GraphQL.Types.Custom;

namespace Our.Umbraco.GraphQL.Types
{
    public class UmbracoQuery : ObjectGraphType
    {
        public UmbracoQuery(IEnumerable<IGraphType> documentTypes, IEnumerable<IGraphType> carbonFootprintingData)
        {
            Field<PublishedContentQueryGraphType>()
                .Name("content")
                .Resolve(context => context.ReturnType)
                .Type(new NonNullGraphType(new PublishedContentQueryGraphType(documentTypes)));

            Field<CarbonFootprintQueryGraphType>()
                .Name("carbonFootprintItems")
                .Resolve(context => context.ReturnType)
                .Type(new NonNullGraphType(new CarbonFootprintQueryGraphType(carbonFootprintingData)));
        }
    }
}
