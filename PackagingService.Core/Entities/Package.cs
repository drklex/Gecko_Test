using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingService.Core.Entities
{
    public class Package
    {
        public int Index { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
    }
}
