using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Website.Data.Models
{

    [TableName("Variants")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class Variant
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("VariantName")]
        public string VariantName { get; set; }

        [Column("Item")]
        [ForeignKey(typeof(Item), Name = "FK_Variant_ItemId")]
        public int Item { get; set; }

        [Column("CO2eGrams")]
        public double CarbonDioxideEquivalent { get; set; }
    }
}
