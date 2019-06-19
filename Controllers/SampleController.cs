using ClaimBasedAuthorization.Entity;
using ClaimBasedAuthorization.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult GetCommonInformation()
        {
            return Ok("You have common info now");
        }

        [HttpGet("[action]")]
        [PermissionBasedAuthorize(PermissionType.ProductRead)]
        public IActionResult GetProducts()
        {

            return Ok("You have your products now");
        }

        [HttpGet("[action]")]
        [PermissionBasedAuthorize(PermissionType.ProductWrite)]
        public IActionResult CreateProduct()
        {
            return Ok("You created product now");
        }

        [HttpGet("[action]")]
        [PermissionBasedAuthorize(PermissionType.SalesOrderWrite)]
        public IActionResult CreateSalesOrder()
        {
            return Ok("You created salesOrder now");
        }

    }
}
