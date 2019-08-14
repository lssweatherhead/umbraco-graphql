using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Website.Data.Models
{

    [TableName("Items")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class Item
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("ItemName")]
        public string ItemName { get; set; }

        [Column("ItemType")]
        [ForeignKey(typeof(ItemType), Name = "FK_Item_TypeId")]
        public int ItemType { get; set; }

        [Column("MinCO2eGrams")]
        public double MinCarbonDioxideEquivalent { get; set; }

        [Column("MaxCO2eGrams")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public double MaxCarbonDioxideEquivalent { get; set; }
    }
}
