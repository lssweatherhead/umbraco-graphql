using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Website.Data.Models;

namespace Website.Core.Services
{
    public static class CategoryService
    {
        public static ItemType GetCategoryById(this DatabaseContext dbContext, int id)
        {
            var categorySql = new Sql().Select("*").From<ItemType>(dbContext.SqlSyntax)
                .Where<ItemType>(i => i.Id == id, dbContext.SqlSyntax);
            return dbContext.Database.FirstOrDefault<ItemType>(categorySql);
        }

        public static ItemType GetCategoryByName(this DatabaseContext dbContext, string name)
        {
            var categorySql = new Sql().Select("*").From<ItemType>(dbContext.SqlSyntax)
                .Where<ItemType>(i => string.Equals(i.Name, name, StringComparison.InvariantCultureIgnoreCase), dbContext.SqlSyntax);
            return dbContext.Database.FirstOrDefault<ItemType>(categorySql);
        }

        public static List<ItemType> GetAllCategories(this DatabaseContext dbContext)
        {
            return dbContext.Database.Fetch<ItemType>(new Sql().Select("*").From<ItemType>(dbContext.SqlSyntax));
        }
    }
}
