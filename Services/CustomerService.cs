using MongoDB.Driver;
using MIRSAL.Models;

namespace MIRSAL.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _CustomerModel;
        public CustomerService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(config.GetConnectionString("DatabaseName"));
            _CustomerModel = database.GetCollection<Customer>("Customer");
        }

        public async Task<List<Customer>> GetAllAsync() => 
            await _CustomerModel.Find(item => true).ToListAsync();

        public async Task<Customer> GetAsync(string id) => 
            await _CustomerModel.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customer item) =>
            await _CustomerModel.InsertOneAsync(item);

        public async Task UpdateAsync(string id, Customer itemIn) =>
            await _CustomerModel.ReplaceOneAsync(item => item.Id == id, itemIn);

        public async Task RemoveAsync(string id) =>
            await _CustomerModel.DeleteOneAsync(item => item.Id == id);

        public async Task<Customer> GetCustomerByUsername(string username) =>
            await _CustomerModel.Find(item => item.Username == username).FirstOrDefaultAsync();

    }
}