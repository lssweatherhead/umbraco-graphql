using GraphQL.Types;
using Our.Umbraco.GraphQL.Extensions;
using Umbraco.Core;
using Website.Core.Models;
using Website.Core.Services;
using Website.Data.Models;

namespace Our.Umbraco.GraphQL.Types.Custom
{
    public class CarbonFootprintItemGraphType: ObjectGraphType<ItemDataObject>
    {
        public CarbonFootprintItemGraphType()
        {
            Name = "CarbonFootprintItem";
            Field<NonNullGraphType<IntGraphType>>(
                "id",
                resolve: context => context.Source.Id
            );
            Field<NonNullGraphType<StringGraphType>>(
                "name",
                resolve: context => context.Source.ItemName
            );
            Field<NonNullGraphType<CarbonFootprintCategoryGraphType>>(
                "category",
                resolve: context => ApplicationContext.Current.DatabaseContext.GetCategoryById(context.Source.ItemType)
            );

            Field<NonNullGraphType<FloatGraphType>>(
                "minCarbonDioxideEquivalentInGrams",
                resolve: context => context.Source.MinCarbonDioxideEquivalent
            );

            Field<FloatGraphType>(
                "maxCarbonDioxideEquivalentInGrams",
                resolve: context => context.Source.MaxCarbonDioxideEquivalent
            );

            Field<NonNullGraphType<ListGraphType<CarbonFootprintVariantGraphType>>>(
                "variants",
                resolve: context => ApplicationContext.Current.DatabaseContext.GetVariantsByItemId(context.Source.Id)
            );

            //Connection<CarbonFootprintVariantGraphType>()
            //    .Name("variants")
            //    .Description("A list of a character's friends.")
            //    .Bidirectional()
            //    .Resolve(context =>
            //    {
            //        var variants = ApplicationContext.Current.DatabaseContext.GetVariantsByItemId(context.Source.Id);
            //        return context.GetPagedResults<ItemDataObject, VariantDataObject>(variants);
            //    });

            //this.FilteredConnection<CarbonFootprintVariantGraphType, Item>()
            //    .Name("variants")
            //    .Description("Item variants")
            //    .Bidirectional()
            //    .Resolve(context => ApplicationContext.Current.DatabaseContext.GetVariantsByItemId(context.Source.Id).Filter(context).ToConnection(context));

            //var builder = Connection<CarbonFootprintVariantGraphType>();
            //builder.FieldType.Arguments.Add(new QueryArgument(typeof(CarbonFootprintVariantGraphType)));
        }
    }
}
