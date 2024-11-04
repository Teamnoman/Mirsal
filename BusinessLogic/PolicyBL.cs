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
        public async Task<List<PolicyVehicleToReturndDto>> GetPolicyVehicle(string customerId){
            var policies = await _policyService.GetPolicyByCustomer(customerId);
            var result = new List<PolicyVehicleToReturndDto>();

            foreach (var policy in policies)
            {
                var vehicle = await _vehicleService.GetAsync(policy.VehicleID);;
                var policyVehicleDto = new PolicyVehicleToReturndDto
                {
                    PolicyID = policy.Id,
                    VehicleModel = vehicle.Model,
                    VehicleYear = vehicle.Year,
                    RegistrationNumber = vehicle.RegistrationNumber
                };
                result.Add(policyVehicleDto);
            }

            return result;
        }
    }
}