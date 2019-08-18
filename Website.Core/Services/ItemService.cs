using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Website.Core.Models;
using Website.Data.Models;

namespace Website.Core.Services
{
    public static class ItemService
    {
        public static ItemDataObject GetItemById(this DatabaseContext dbContext, int id)
        {
            var itemQry = new Sql().Select("*").From<Item>(dbContext.SqlSyntax).Where<Item>(i => i.Id == id, dbContext.SqlSyntax);
            return MapItem(dbContext.Database.FirstOrDefault<Item>(itemQry));
        }

        public static IEnumerable<ItemDataObject> GetItemsByCategoryId(this DatabaseContext dbContext, int categoryId)
        {
            var itemsSql = new Sql().Select("*").From<Item>(dbContext.SqlSyntax)
                .Where<Item>(i => i.ItemType == categoryId, dbContext.SqlSyntax);
            return dbContext.Database.Fetch<Item>(itemsSql).Select(MapItem);
        }

        public static IEnumerable<ItemDataObject> GetAllItems(this DatabaseContext dbContext)
        {
            return dbContext.Database.Fetch<Item>(new Sql().Select("*").From<Item>(dbContext.SqlSyntax)).Select(MapItem);
        }

        private static ItemDataObject MapItem(Item item)
        {
            return new ItemDataObject
            {
                Id = item.Id,
                ItemName = item.ItemName,
                ItemType = item.ItemType,
                MaxCarbonDioxideEquivalent = item.MaxCarbonDioxideEquivalent,
                MinCarbonDioxideEquivalent = item.MinCarbonDioxideEquivalent
            };
        }
    }
}
