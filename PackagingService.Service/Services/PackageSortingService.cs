using PackagingService.Core.DataModels;
using PackagingService.Core.Entities;
using PackagingService.Core.Helpers;
using PackagingService.Service.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingService.Service.Services
{
    public class PackageSortingService : IPackageSortingService
    {
        private readonly ValidatePackage _validatePackage;

        public PackageSortingService(ValidatePackage validatePackage)
        {
            _validatePackage = validatePackage;
        }

        public List<int> CalculatePackage(ShipmentDTO shippment)
        {
            try
            {
                //TODO: AutoMapper from DTO to entites

                // validate packages base on creteria required
                List<PackageDTO> validPackages = _validatePackage.Validate(shippment.Packages, shippment.MaxWeightAllowed);
                if (!validPackages.Any())
                    return null;

                List<PackageDTO> validPackagesOrdered = validPackages
                                                            .OrderByDescending(x => x.Price)
                                                            .ThenByDescending(z => z.Weight).ToList();

                List<PackValue> packVal = new List<PackValue>();    // will hold final list of all packages combinations that are fulfill the criteria
                if (validPackagesOrdered.Count > 1)     // if there are more then one packages select them
                {
                    for (int i = 0; i < validPackagesOrdered.Count; i++)
                    {
                        var currentPack = validPackagesOrdered[i];

                        // add this package to the list as single first
                        packVal.Add(new PackValue()
                        {
                            Price = decimal.Parse(currentPack.Price),
                            Indexes = new List<int>() { currentPack.Index }
                        });

                        // foreach valid packages find other packages to match and increase the value but not more then max weight value
                        var stackPack = new Stack<PackageDTO>(validPackagesOrdered);
                        while (stackPack.Count > 0)
                        {
                            PackageDTO packFromStack = stackPack.Pop();

                            if (currentPack.Index != packFromStack.Index)
                            {
                                if ((currentPack.Weight + packFromStack.Weight) <= shippment.MaxWeightAllowed)
                                {
                                    List<int> newIndexList = new List<int>();
                                    newIndexList.Add(currentPack.Index);
                                    newIndexList.Add(packFromStack.Index);

                                    decimal currentMaxPrice = (decimal.Parse(currentPack.Price) + decimal.Parse(packFromStack.Price));

                                    // if there is unused weight then search packages with weight less then unused and add them to the list (to increase the value)
                                    var unusedWeight = shippment.MaxWeightAllowed - (currentPack.Weight + packFromStack.Weight);
                                    var packagesList = validPackagesOrdered.Where(
                                                            x => x.Weight <= unusedWeight &&
                                                            x.Index != currentPack.Index &&
                                                            x.Index != packFromStack.Index)
                                                            .OrderByDescending(y => y.Price)
                                                            .ThenByDescending(z => z.Weight).ToList();

                                    if (packagesList.Any())
                                    {
                                        newIndexList.Add(packagesList.First().Index);
                                        currentMaxPrice = currentMaxPrice + decimal.Parse(packagesList.First().Price);
                                    }

                                    // add the pack
                                    packVal.Add(new PackValue()
                                    {
                                        Price = currentMaxPrice,
                                        Indexes = newIndexList
                                    }); ;
                                }
                            }
                        }

                    }
                }
                else
                {
                    // add the pack
                    packVal.Add(new PackValue()
                    {
                        Price = decimal.Parse(validPackagesOrdered.First().Price),
                        Indexes = new List<int>() { validPackagesOrdered.First().Index }
                    }); ;
                }

                // order valid packages by value(price)
                var maxValuePack = packVal.OrderByDescending(x => x.Price);

                // return packages with most combine price
                return maxValuePack.First().Indexes;
            }
            catch (Exception)
            {
                // TODO: Log error

                return null;
            }

        }
    }


}
