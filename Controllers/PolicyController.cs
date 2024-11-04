using Microsoft.AspNetCore.Mvc;
using MIRSAL.BusinessLogic;
using MIRSAL.Services;

namespace MIRSAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController(PolicyService policyService, VehicleService vehicleService) : ControllerBase
    {
        private readonly PolicyBL policyBL = new PolicyBL(policyService,vehicleService);

        [HttpGet("GetPolicyVehicleByCustomer")]
        public async Task<IActionResult> GetPolicyVehicleByCustomer(string customerId)
        {
            var PolicyVehicleList = await policyBL.GetPolicyVehicles(customerId);

            if (PolicyVehicleList != null)
            {
                return Ok(PolicyVehicleList);
            }

            return NotFound();
        }

    }
}