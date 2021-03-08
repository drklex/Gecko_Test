using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingService.Core.DataModels
{
    public class PackValue
    {
        public PackValue()
        {
            this.Indexes = new List<int>();
        }

        public decimal Price { get; set; }
        public List<int> Indexes { get; set; }
    }
}
