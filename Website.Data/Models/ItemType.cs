using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Website.Data.Models
{

    [TableName("ItemTypes")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class ItemType
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
