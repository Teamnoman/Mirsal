using MIRSAL.DTO;
using MIRSAL.Services;

namespace MIRSAL.BusinessLogic
{
    public class PolicyBL
    {
        public readonly PolicyService _policyService;
        public readonly VehicleService _vehicleService;
        public PolicyBL(PolicyService policyService, VehicleService vehicleService){
            _policyService  =  policyService;
            _vehicleService = vehicleService;
        }
        public async Task<List<PolicyVehicleToReturndDto>> GetPolicyVehicles(string customerId){
            var policies = await _policyService.GetPolicyByCustomer(customerId);
            var result = new List<PolicyVehicleToReturndDto>();

            foreach (var policy in policies)
            {
                var policyVehicleDto = await GetPolicyVehicle(policy.Id,policy.VehicleID);
                result.Add(policyVehicleDto);
            }

            return result;
        }

        public async Task<PolicyVehicleToReturndDto> GetPolicyVehicle(string policyId, string vehicleId){
            var vehicle = await _vehicleService.GetAsync(vehicleId);;
            var policyVehicleDto = new PolicyVehicleToReturndDto
            {
                PolicyID = policyId,
                VehicleModel = vehicle.Model,
                VehicleYear = vehicle.Year,
                RegistrationNumber = vehicle.RegistrationNumber
            };
            return policyVehicleDto;
        }
    }
}