using MongoDB.Driver;
using MIRSAL.Models;

namespace MIRSAL.Services
{
    public class PolicyService
    {
        private readonly IMongoCollection<Policy> _PolicyModel;
        public PolicyService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(config.GetConnectionString("DatabaseName"));
            _PolicyModel = database.GetCollection<Policy>("Policy");
        }

        public async Task<List<Policy>> GetAllAsync() => 
            await _PolicyModel.Find(item => true).ToListAsync();

        public async Task<Policy> GetAsync(string id) => 
            await _PolicyModel.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Policy item) =>
            await _PolicyModel.InsertOneAsync(item);

        public async Task UpdateAsync(string id, Policy itemIn) =>
            await _PolicyModel.ReplaceOneAsync(item => item.Id == id, itemIn);

        public async Task RemoveAsync(string id) =>
            await _PolicyModel.DeleteOneAsync(item => item.Id == id);

        public async Task<bool> IsValidPolicyAndCustomer(string customerId, string policyNumber) =>
            await _PolicyModel.Find(item => item.Id == policyNumber && item.CustomerID == customerId).AnyAsync();
    }
}