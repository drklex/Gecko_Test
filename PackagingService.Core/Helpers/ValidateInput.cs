using PackagingService.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PackagingService.Core.Helpers
{
    public class ValidateInput
    {
        public ShipmentDTO Validate(string inputString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputString))
                    return null;

                // TODO: if input string is valid json string => then we need to deserialize that json string

                // this will deserialize the invalid string format (not json) shown as in example
                var arrStrShippment = inputString.Split(':');
                var arrStrPackages = arrStrShippment[1].Split(' ');

                // Deserialize the input string to valid object
                ShipmentDTO oneShipment = new ShipmentDTO();
                oneShipment.MaxWeightAllowed = Int32.Parse(arrStrShippment[0]);

                foreach (var package in arrStrPackages)
                {
                    var arrPackage = package.Split(',');

                    PackageDTO onePackage = new PackageDTO();
                    onePackage.Index = Int32.Parse(arrPackage[0].Replace("(", ""));
                    onePackage.Weight = Decimal.Parse(arrPackage[1]);
                    onePackage.Price = arrPackage[2].Replace(")", "").Replace("€", ""); // "e.g. €45"

                    oneShipment.Packages.Add(onePackage);
                }

                return oneShipment;
            }
            catch (Exception)
            {
                //TODO: Log exception

                return null;
            }

        }
    }
}
