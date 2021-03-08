using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PackagingService.Core.DataModels;
using PackagingService.Service.Inerfaces;
using PackagingService.Service.Services;
using PackagingService.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PackagingService.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v1/pack")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageSortingService _packageSortingService;
        private readonly ValidateInput _validateInput;

        public PackageController(IPackageSortingService packageSortingService, ValidateInput validateInput)
        {
            _packageSortingService = packageSortingService;
            _validateInput = validateInput;
        }

        // POST api/v1/pack
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            /*
                Case 1 = Input string: "81:(1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)"
                Case 2 = Input string: "8:(1,15.3,€34)"
                Case 3 = Input string: "75:(1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)"
                Case 4 = Input string: "56:(1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)"
             */

            //TODO: Authentication check here
            // validate token
            // return Unautorized if it's not valid

            // Validate input string (json)
            var validInput = _validateInput.Validate(value);
            if (validInput == null)
                return Problem(null, null, statusCode: 400, "Invalid input string", null); //Bad Request

            // Call service
            var result = _packageSortingService.CalculatePackage(validInput);

            return Ok(result);
        }
    }
}
