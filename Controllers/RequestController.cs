using Microsoft.AspNetCore.Mvc;
using MIRSAL.BusinessLogic;
using MIRSAL.DTO;
using MIRSAL.Services;

namespace MIRSAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController(RequestService requestService, PolicyService policyService, CustomerService customerService, VehicleService vehicleService) : ControllerBase
    {
        private readonly RequestService _requestService = requestService;
        private readonly RequestBL requestBL = new RequestBL(policyService,requestService,vehicleService,customerService);

        [HttpGet("GetRequestsByCustomer")]
        public async Task<IActionResult> GetRequestsByCustomer(string id)
        {
            var RequestList = await _requestService.GetAllRequestsByCustomer(id);

            if (RequestList != null)
            {
                return Ok(RequestList);
            }

            return NotFound();
        }

        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var RequestList = await requestBL.GetAllRequests();

            if (RequestList != null)
            {
                return Ok(RequestList);
            }

            return NotFound();
        }

        [HttpGet("GetRequest")]
        public async Task<IActionResult> GetRequest(string id)
        {
            var Request = await _requestService.GetAsync(id);

            if (Request != null)
            {
                return Ok(Request);
            }

            return NotFound();
        }

       [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequest(RequestToCreateDto request)
        {
            var createdRequest = await requestBL.CreateNewRequest(request);

            if (createdRequest != null)
            {
                return Ok(createdRequest);
            }

            return BadRequest();
        }
        
        [HttpGet("GetRequestDashboardData")]
        public async Task<IActionResult> GetRequestDashboardData(string requestId)
        {
            var dashboardData = await requestBL.GetDashboardData(requestId);

            if (dashboardData != null)
            {
                return Ok(dashboardData);
            }

            return BadRequest();
        }
    }
}   