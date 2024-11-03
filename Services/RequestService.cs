using MIRSAL.Models;
using MongoDB.Driver;

namespace MIRSAL.Services
{
    public class RequestService
    {
        private readonly IMongoCollection<Request> _RequestModel;

        public RequestService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(config.GetConnectionString("DatabaseName"));
            _RequestModel = database.GetCollection<Request>("Request");
        }

        public async Task<List<Request>> GetAllAsync() => 
            await _RequestModel.Find(item => true).ToListAsync();

        public async Task<Request> GetAsync(string id) => 
            await _RequestModel.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Request item) =>
            await _RequestModel.InsertOneAsync(item);

        public async Task UpdateAsync(string id, Request itemIn) =>
            await _RequestModel.ReplaceOneAsync(item => item.Id == id, itemIn);

        public async Task RemoveAsync(string id) =>
            await _RequestModel.DeleteOneAsync(item => item.Id == id);

        public async Task<List<Request>> GetAllRequestsByCustomer(string id) => 
            await _RequestModel.Find(item => item.CustomerID == id).ToListAsync();   

    }
}