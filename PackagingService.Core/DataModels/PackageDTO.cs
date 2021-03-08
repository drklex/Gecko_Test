using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingService.Core.DataModels
{
    public class PackageDTO
    {
        public int Index { get; set; }
        public decimal Weight { get; set; }
        public string Price { get; set; }
    }
}
