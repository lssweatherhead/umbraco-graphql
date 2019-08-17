using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
