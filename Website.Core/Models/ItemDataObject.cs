using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Website.Data.Models;

namespace Website.Core.Models
{
    public class ItemDataObject: DataObject
    {
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public double MinCarbonDioxideEquivalent { get; set; }
        public double MaxCarbonDioxideEquivalent { get; set; }
    }
}
