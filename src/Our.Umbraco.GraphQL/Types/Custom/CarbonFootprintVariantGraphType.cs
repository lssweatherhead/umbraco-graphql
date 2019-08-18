using GraphQL.Types;
using Umbraco.Core;
using Website.Core.Models;
using Website.Core.Services;
using Website.Data.Models;

namespace Our.Umbraco.GraphQL.Types.Custom
{
    public class CarbonFootprintVariantGraphType: ObjectGraphType<VariantDataObject>
    {
        public CarbonFootprintVariantGraphType()
        {
            Name = "CarbonFootprintItemVariant";
            Field<NonNullGraphType<IntGraphType>>(
                "id",
                resolve: context => context.Source.Id
            );
            Field<NonNullGraphType<StringGraphType>>(
                "name",
                resolve: context => context.Source.VariantName
            );

            Field<NonNullGraphType<CarbonFootprintItemGraphType>>(
                "item",
                resolve: context => ApplicationContext.Current.DatabaseContext.GetItemById(context.Source.Item)
            );

            Field<NonNullGraphType<FloatGraphType>>(
                "carbonDioxideEquivalentInGrams",
                resolve: context => context.Source.CarbonDioxideEquivalent
            );
        }
    }
}
