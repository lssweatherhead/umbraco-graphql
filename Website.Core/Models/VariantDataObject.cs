using System;
using System.Collections.Generic;
using System.Text;

namespace Website.Core.Models
{
    public class VariantDataObject: DataObject
    {
        public string VariantName { get; set; }
        public double CarbonDioxideEquivalent { get; set; }
        public int Item { get; set; }
    }
}
