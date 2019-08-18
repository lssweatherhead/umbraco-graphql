using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Website.Core.Models;
using Website.Data.Models;

namespace Website.Core.Services
{
    public static class VariantService
    {
        public static VariantDataObject GetVariantById(this DatabaseContext dbContext, int id)
        {
            var varQry = new Sql().Select("*").From<Variant>(dbContext.SqlSyntax).Where<Variant>(i => i.Id == id, dbContext.SqlSyntax);
            return MapVariant(dbContext.Database.FirstOrDefault<Variant>(varQry));
        }

        public static IEnumerable<VariantDataObject> GetVariantsByItemId(this DatabaseContext dbContext, int itemId)
        {
            var varQry = new Sql().Select("*").From<Variant>(dbContext.SqlSyntax)
                .Where<Variant>(v => v.Item == itemId, dbContext.SqlSyntax);
            return dbContext.Database.Fetch<Variant>(varQry).Select(MapVariant);
        }

        public static IEnumerable<VariantDataObject> GetAllVariants(this DatabaseContext dbContext)
        {
            return dbContext.Database.Fetch<Variant>(new Sql().Select("*").From<Variant>(dbContext.SqlSyntax)).Select(MapVariant);
        }

        private static VariantDataObject MapVariant(Variant variant)
        {
            return new VariantDataObject
            {
                Id = variant.Id,
                Item = variant.Item,
                VariantName = variant.VariantName,
                CarbonDioxideEquivalent = variant.CarbonDioxideEquivalent
            };
        }
    }
}
