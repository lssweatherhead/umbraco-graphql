using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;

namespace Our.Umbraco.GraphQL.Types.Custom
{
    public class CarbonFootprintItemGraphType: ObjectGraphType<Item>
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
                resolve: context => context.Source.ItemType
            );

            Field<NonNullGraphType<FloatGraphType>>(
                "minCarbonDioxideEquivalentInGrams",
                resolve: context => context.Source.MinCarbonDioxideEquivalent
            );

            Field<FloatGraphType>(
                "maxCarbonDioxideEquivalentInGrams",
                resolve: context => context.Source.MaxCarbonDioxideEquivalent
            );
        }
    }
}
