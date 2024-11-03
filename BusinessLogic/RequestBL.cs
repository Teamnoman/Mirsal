using MIRSAL.DTO;
using MIRSAL.Models;
using MIRSAL.Services;

namespace MIRSAL.BusinessLogic
{
    public class RequestBL
    {
        public readonly PolicyService _policyService;
        public readonly RequestService _requestService;
        public readonly VehicleService _vehicleService;
        public readonly CustomerService _customerService;

        public RequestBL(PolicyService policyService, RequestService requestService, 
                            VehicleService vehicleService, CustomerService customerService){
            _policyService = policyService;
            _requestService = requestService;
            _vehicleService = vehicleService;
            _customerService = customerService;
        }

        public async Task<Request?> CreateNewRequest(RequestToCreateDto request){
            if(!await _policyService.IsValidPolicyAndCustomer(request.CustomerID, request.PolicyID)) {
                return null;
            }
            var newRequest = new Request {
                CustomerID = request.CustomerID,
                PolicyID = request.PolicyID,
                Location = request.Location,
                IncidentStartTime = request.IncidentStartTime,
                IncidentEndTime = request.IncidentEndTime,
                IncidentDate = request.IncidentDate,
                CreatedDateTime = DateTime.Now,
            };
            await _requestService.CreateAsync(newRequest);
            return newRequest;
        }

        
        public async Task<DashboardDto?> GetDashboardData(string requestId){

            var request = await _requestService.GetAsync(requestId);
            if(request == null) {
                return null;
            }
            
            var policy = await _policyService.GetAsync(request.PolicyID);
            if(policy == null) {
                return null;
            }

            var vehicle = await _vehicleService.GetAsync(policy.VehicleID);
            if(vehicle == null) {
                return null;
            }

            var customer = await _customerService.GetAsync(request.CustomerID);
            if(customer == null) {
                return null;
            }
            
            return new DashboardDto {
                RequestInfo = request,
                CustomerInfo = customer,
                PolicyInfo = policy,
                VehicleInfo = vehicle
            };
        }  
    }
}