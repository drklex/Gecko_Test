using PackagingService.Core.DataModels;
using PackagingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingService.Service.Inerfaces
{
    public interface IPackageSortingService
    {
        public List<int> CalculatePackage(ShipmentDTO shippment); 
    }
}
