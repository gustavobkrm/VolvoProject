using VolvoProject.Data.Interfaces;
using VolvoProject.Models;
using VolvoProject.Services.Interfaces;

namespace VolvoProject.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<VehicleService> _logger;


        public VehicleService(IVehicleRepository vehicleRepository, ILogger<VehicleService> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        public IList<Vehicle> GetAllVehicles()
        {
            try
            {
                return _vehicleRepository.GetAllVehicles();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching all vehicles: {ex.Message}", ex);
                throw new Exception("An error occurred while fetching all vehicles.", ex);
            }
        }

        public Vehicle GetVehicleById(int id)
        {
            try
            {
                var vehicle = _vehicleRepository.GetVehicleById(id);
                if (vehicle == null)
                {
                    _logger.LogWarning($"Vehicle with id {id} not found.");
                }
                return vehicle!;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching the vehicle with id {id}: {ex.Message}", ex);
                throw new Exception($"An error occurred while fetching the vehicle with id {id}.", ex);
            }
        }

        public Vehicle GetByChassis(string chassisSeries, uint chassisNumber)
        {
            return null;
        }

        public bool InsertVehicle(Vehicle vehicle)
        {
            try
            {
                if (ChassiIdAlreadyExists(vehicle))
                    return false;
                

                var newVehicle = new Vehicle(
                    vehicle.VType,
                    vehicle.ChassisSeries,
                    vehicle.ChassisNumber,
                    vehicle.Color
                );

                _vehicleRepository.InsertVehicle(newVehicle);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while inserting a new vehicle: {ex.Message}", ex);
                throw new Exception("An error occurred while inserting the vehicle.", ex);
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                _vehicleRepository.UpdateVehicle(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating the vehicle with id {vehicle.Id}: {ex.Message}", ex);
                throw new Exception($"An error occurred while updating the vehicle with id {vehicle.Id}.", ex);
            }
        }

        private bool ChassiIdAlreadyExists(Vehicle vehicle)
        {
            var existingVehicle = _vehicleRepository.GetVehicleByChassisId(vehicle.ChassisSeries, vehicle.ChassisNumber);
            
            if (existingVehicle != null)
                return true;

            return false;

        }
    }
}
