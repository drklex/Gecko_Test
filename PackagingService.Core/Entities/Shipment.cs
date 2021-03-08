using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingService.Core.Entities
{
    public class Shipment
    {
        public int MaxWeightAllowed { get; set; }

        public IEnumerable<Package> Packages { get; set; }
    }
}
