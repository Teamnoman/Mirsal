using MongoDB.Driver;
using MIRSAL.Models;

namespace MIRSAL.Services
{
    public class VehicleService
    {
        private readonly IMongoCollection<Vehicle> _VehicleModel;
        public VehicleService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(config.GetConnectionString("DatabaseName"));
            _VehicleModel = database.GetCollection<Vehicle>("Vehicle");
        }

        public async Task<List<Vehicle>> GetAllAsync() => 
            await _VehicleModel.Find(item => true).ToListAsync();

        public async Task<Vehicle> GetAsync(string id) => 
            await _VehicleModel.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Vehicle item) =>
            await _VehicleModel.InsertOneAsync(item);

        public async Task UpdateAsync(string id, Vehicle itemIn) =>
            await _VehicleModel.ReplaceOneAsync(item => item.Id == id, itemIn);

        public async Task RemoveAsync(string id) =>
            await _VehicleModel.DeleteOneAsync(item => item.Id == id);
    }
}