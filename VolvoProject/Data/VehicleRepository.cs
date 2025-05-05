using Microsoft.EntityFrameworkCore;
using VolvoProject.Data.Interfaces;
using VolvoProject.Infrastructure;
using VolvoProject.Models;
using VolvoProject.Services;

namespace VolvoProject.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ILogger<VehicleRepository> _logger;
        private readonly ApplicationDbContext _dbContext;


        public VehicleRepository(ApplicationDbContext dbContext, ILogger<VehicleRepository> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;

        }

        public IList<Vehicle> GetAllVehicles()
        {
            return _dbContext.Vehicles.ToList();
        }

        public Vehicle GetVehicleById(int id)
        {
            return _dbContext.Vehicles.FirstOrDefault(v => v.Id == id)!;
        }

        public Vehicle GetByChassis(string chassisSeries, uint chassisNumber)
        {
            return _dbContext.Vehicles.FirstOrDefault(v => v.ChassisSeries == chassisSeries && v.ChassisNumber == chassisNumber)!;
        }

        public void InsertVehicle(Vehicle vehicle)
        {

            _dbContext.Vehicles.Add(vehicle);
            _dbContext.SaveChanges();

        }

        public void UpdateVehicle(Vehicle vehicle)
        {

            _dbContext.Vehicles.Update(vehicle);
            _dbContext.SaveChanges();

        }

        public Vehicle GetVehicleByChassisId(string chassisSeries, uint chassisNumber)
        {
            return _dbContext.Vehicles
                .FirstOrDefault(v => v.ChassisSeries == chassisSeries && v.ChassisNumber == chassisNumber);
        }
    }
}
