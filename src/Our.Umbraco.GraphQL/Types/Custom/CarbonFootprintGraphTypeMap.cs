using GraphQL.Types;
using Umbraco.Core;
using Website.Core.Services;
using Website.Data.Models;

namespace Our.Umbraco.GraphQL.Types.Custom
{
    public class CarbonFootprintGraphTypeMap: ObjectGraphType<ItemType>
    {
        public CarbonFootprintGraphTypeMap(DatabaseContext dbContext, ItemType category)
        {
            Name = category.Name;
            Description = "Carbon footprint items that fall under " + category.Name;
            IsTypeOf = content => ((ItemType) content).Id == category.Id;

            this.Field<NonNullGraphType<IdGraphType>>()
                .Name("id")
                .Description("Unique id of the category")
                .Resolve(context => category.Id);

            this.Field<NonNullGraphType<IdGraphType>>()
                .Name("name")
                .Description("Category name")
                .Resolve(context => category.Name);

            this.FilteredConnection<CarbonFootprintItemGraphType, ItemType>()
                .Name("items")
                .Description("Carbon footprint items")
                .Bidirectional()
                .Resolve(context => dbContext.GetItemsByCategoryId(category.Id));
        }
    }
}
