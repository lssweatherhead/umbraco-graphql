using GraphQL.Types;
using Umbraco.Core;
using Website.Core.Services;
using Website.Data.Models;

namespace Our.Umbraco.GraphQL.Types.Custom
{
    public class CarbonFootprintCategoryGraphType : ObjectGraphType<ItemType>
    {
        public CarbonFootprintCategoryGraphType()
        {
            Name = "CarbonFootprintCategory";
            Field<NonNullGraphType<IntGraphType>>(
                "id",
                resolve: context => context.Source.Id
            );
            Field<NonNullGraphType<StringGraphType>>(
                "name",
                resolve: context => context.Source.Name
            );
            Field<NonNullGraphType<ListGraphType<CarbonFootprintItemGraphType>>>(
                "items",
                resolve: context => ApplicationContext.Current.DatabaseContext.GetItemsByCategoryId(context.Source.Id)
            );
        }
    }
}
