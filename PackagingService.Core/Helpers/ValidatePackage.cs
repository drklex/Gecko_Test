using PackagingService.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PackagingService.Core.Helpers
{
    public class ValidatePackage
    {
        public List<PackageDTO> Validate(List<PackageDTO> inputPackages, int shippmentMaxWeight)
        {
            try
            {
                if (!inputPackages.Any())
                    return null;

                List<PackageDTO> validPackages = new List<PackageDTO>();
                foreach (var pack in inputPackages)
                {
                    var isValidPrice = Decimal.TryParse(pack.Price, out decimal resultPrice);
                    if (isValidPrice)
                    {
                        // TODO: get shippment requiremnts from appsetting
                        if (pack.Weight < shippmentMaxWeight && pack.Weight <= 100 && (pack.Weight + resultPrice <= 100))
                        {
                            validPackages.Add(pack);
                        }
                    }
                    else
                        return null;
                }

                return validPackages;
            }
            catch (Exception)
            {
                //TODO: Log exception

                return null;
            }

        }
    }
}
