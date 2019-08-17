using System;
using System.Collections.Generic;
using GraphQL.Types;
using Our.Umbraco.GraphQL.Types.Custom;
using Umbraco.Core.Configuration;
using Umbraco.Core.Persistence;
using Umbraco.Web.Routing;
using Website.Data.Models;

namespace Our.Umbraco.GraphQL.Types
{
    public class CarbonFootprintQueryGraphType : ObjectGraphType
    {
        public CarbonFootprintQueryGraphType(IEnumerable<IGraphType> carbonFootprintTypes)
        {
            Name = "CarbonFootprintQuery";

            Field<CarbonFootprintItemGraphType>()
                .Name("byId")
                .Argument<NonNullGraphType<IdGraphType>>("id", "The unique content id")
                .Resolve(context =>
                {
                    var userContext = (UmbracoGraphQLContext)context.UserContext;
                    var id = context.GetArgument<int>("id");
                    var itemQry = new Sql().Select("*").From<Item>(userContext.DatabaseContext.SqlSyntax).Where<Item>(i => i.Id == id, userContext.DatabaseContext.SqlSyntax);
                    return userContext.DatabaseContext.Database.FirstOrDefault<Item>(itemQry);
                });

            Field<NonNullGraphType<CarbonFootprintItemGraphType>>()
                .Name("byCategory")
                .Argument<NonNullGraphType<StringGraphType>>("category", "The category name")
                .Resolve(context =>
                {
                    var userContext = (UmbracoGraphQLContext)context.UserContext;
                    var name = context.GetArgument<string>("category");

                    var categorySql = new Sql().Select("*").From<ItemType>(userContext.DatabaseContext.SqlSyntax)
                        .Where<ItemType>(i => string.Equals(i.Name, name, StringComparison.InvariantCultureIgnoreCase),
                            userContext.DatabaseContext.SqlSyntax);
                    var category = userContext.DatabaseContext.Database.FirstOrDefault<ItemType>(categorySql);

                    var itemsSql = new Sql().Select("*").From<Item>(userContext.DatabaseContext.SqlSyntax)
                        .Where<Item>(i => i.ItemType == category.Id,
                            userContext.DatabaseContext.SqlSyntax);
                    var items = userContext.DatabaseContext.Database.Fetch<Item>(itemsSql);

                    return items;
                });
            //.Type(new NonNullGraphType(new CarbonFootprintQueryGraphType(carbonFootprintTypes)));

            //Field<NonNullGraphType<PublishedContentAtRootQueryGraphType>>()
            //    .Name("atRoot")
            //    .Resolve(context => context.ReturnType)
            //    .Type(new NonNullGraphType(new CarbonFootprintQueryGraphType(carbonFootprintTypes)));
        }
    }
}
