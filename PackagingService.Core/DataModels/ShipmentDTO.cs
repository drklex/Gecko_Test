using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingService.Core.DataModels
{
    public class ShipmentDTO
    {
        public ShipmentDTO()
        {
            this.Packages = new List<PackageDTO>();
        }

        public int MaxWeightAllowed { get; set; }

        public List<PackageDTO> Packages { get; set; }
    }

}
