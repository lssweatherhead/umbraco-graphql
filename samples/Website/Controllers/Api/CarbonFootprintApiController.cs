
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web.WebApi;
using Website.Data.Models;

namespace Website.Controllers.Api
{
    public class CarbonFootprintApiController : UmbracoAuthorizedApiController
    {
        public readonly DatabaseContext DbContext;
        public readonly UmbracoDatabase Db;

        public CarbonFootprintApiController() {
            DbContext = ApplicationContext.DatabaseContext;
            Db = DbContext.Database;
        }

        [HttpGet]
        public object GetVariants(int itemId)
        {
            var itemQry = new Sql().Select("*").From<Item>(DbContext.SqlSyntax).Where<Item>(i => i.Id == itemId, DbContext.SqlSyntax);
            var item = Db.Single<Item>(itemQry);

            var varQry = new Sql().Select("*").From<Variant>(DbContext.SqlSyntax).Where<Variant>(i => i.Item == itemId, DbContext.SqlSyntax);
            var variants = Db.Fetch<Variant>(varQry);

            return new VariantDisplayViewModel(item.ItemName, variants.Select(x => new VariantViewModel(x.VariantName, x.CarbonDioxideEquivalent)));
        }
    }

    public class VariantViewModel
    {
        [JsonProperty("name")]
        public string VariantName { get; set; }
        [JsonProperty("co2e")]
        public double CarbonDioxideEquivalent { get; set; }

        public VariantViewModel(string variantName, double carbonDioxideEquivalent)
        {
            VariantName = variantName;
            CarbonDioxideEquivalent = carbonDioxideEquivalent;
        }
    }

    public class VariantDisplayViewModel
    {
        [JsonProperty("variants")]
        public IEnumerable<VariantViewModel> Variants { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public VariantDisplayViewModel(string name, IEnumerable<VariantViewModel> variants)
        {
            Name = name;
            Variants = variants;
        }
    }
}
