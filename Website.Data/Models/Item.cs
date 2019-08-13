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

        [Column("CO2E")]
        public double CarbonDioxideEquivalent { get; set; }
    }
}
