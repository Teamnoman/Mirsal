using MIRSAL.Common;
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
        public readonly string[] VideoUrls = {
            "https://drive.google.com/file/d/1gpIzjHP_o0Lv4luP9dHJVA_FoaJ6SLKJ/view?usp=sharing",
            "https://drive.google.com/file/d/1GQfLBSLugTUP-VrloP1l6iUxgGkrE7gz/view?usp=sharing"
        };

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
            Random random = new Random();
            var newRequest = new Request {
                CustomerID = request.CustomerID,
                PolicyID = request.PolicyID,
                Location = request.Location,
                IncidentStartTime = request.IncidentStartTime,
                IncidentEndTime = request.IncidentEndTime,
                IncidentDate = request.IncidentDate,
                Description = request.Description,
                Status = RequestStatus.Pending,
                FootageUrl = VideoUrls[random.Next(0,2)],
                CreatedDateTime = DateTime.Now,
                RequestPath = CreateRequestPath()
            };
            await _requestService.CreateAsync(newRequest);
            return newRequest;
        }

        
        public async Task<DashboardDto?> GetDashboardData(string requestId){

            var request = await _requestService.GetAsync(requestId);
            if(request == null) {
                return null;
            }
            request.Status = request.Status ?? RequestStatus.Pending;
            request.FootageUrl = request.FootageUrl ?? "https://drive.google.com/file/d/1gpIzjHP_o0Lv4luP9dHJVA_FoaJ6SLKJ/view?usp=drive_link";
            
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

        public async Task<List<DashboardDto>> GetAllRequests()
        {
            List<DashboardDto> RequestsList = new List<DashboardDto>();
            var requests = await _requestService.GetAllAsync();
            foreach(var request in requests){
                var details = await GetDashboardData(request.Id);
                if(details != null)
                {
                    RequestsList.Add(details);
                }
            }
            return RequestsList;
        }

       private static Random random = new Random();

        public static Coordinates GeneratePoint(double minLat, double maxLat, double minLon, double maxLon)
        {
            double latitude = Math.Round(minLat + (random.NextDouble() * (maxLat - minLat)), 6);
            double longitude = Math.Round(minLon + (random.NextDouble() * (maxLon - minLon)), 6);

            return new Coordinates{
                Latitude = latitude,
                Longitude = longitude
            };
        }
        private static Coordinates GenerateIntermediatePoint(Coordinates start, Coordinates end)
        {
            // Generate a point closer to the mid-range of the start and end
            double latitude = Math.Round((start.Latitude + end.Latitude) / 2 + random.NextDouble() - 0.5, 6);
            double longitude = Math.Round((start.Longitude + end.Longitude) / 2 + random.NextDouble() - 0.5, 6);

            return new Coordinates{
                Latitude = latitude,
                Longitude = longitude
            };
        }

        public static RequestPath CreateRequestPath()
        {
            // Define boundaries for Saudi Arabia
            double minLat = 16.5, maxLat = 32.2;
            double minLon = 34.5, maxLon = 55.7;

            // Step 1: Generate Start and End points
            var startPoint = GeneratePoint(minLat, maxLat, minLon, maxLon);
            var endPoint = GeneratePoint(minLat, maxLat, minLon, maxLon);

            // Step 2: Generate Intermediate points (somewhere in between start and end)
            var midPoint1 = GenerateIntermediatePoint(startPoint, endPoint);
            var midPoint2 = GenerateIntermediatePoint(startPoint, endPoint);

            var requestPath = new RequestPath()
            {
                StartPoint = startPoint,
                MiddlePoints = new List<Coordinates> {
                    midPoint1,midPoint2
                },
                EndPoint = endPoint
            };

            return requestPath;
        }
    }
}