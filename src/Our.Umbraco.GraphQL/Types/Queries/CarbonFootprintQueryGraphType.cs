using GraphQL.Types;
using Our.Umbraco.GraphQL.Types.Custom;
using System.Collections.Generic;
using Website.Core.Services;

namespace Our.Umbraco.GraphQL.Types
{
    public class CarbonFootprintQueryGraphType : ObjectGraphType
    {
        public CarbonFootprintQueryGraphType(IEnumerable<IGraphType> carbonFootprintTypes)
        {
            Name = "CarbonFootprintQuery";

            Field<NonNullGraphType<CarbonFootprintCategoryGraphType>>()
                .Name("byId")
                .Argument<NonNullGraphType<IdGraphType>>("id", "The unique content id")
                .Resolve(context =>
                {
                    var userContext = (UmbracoGraphQLContext)context.UserContext;
                    var id = context.GetArgument<int>("id");

                    return userContext.DatabaseContext.GetCategoryById(id);
                });

            Field<NonNullGraphType<CarbonFootprintCategoryGraphType>>()
                .Name("byCategory")
                .Argument<NonNullGraphType<StringGraphType>>("category", "The category name")
                .Resolve(context =>
                {
                    var userContext = (UmbracoGraphQLContext)context.UserContext;
                    var name = context.GetArgument<string>("category");

                    return userContext.DatabaseContext.GetCategoryByName(name);
                });
        }
    }
}
