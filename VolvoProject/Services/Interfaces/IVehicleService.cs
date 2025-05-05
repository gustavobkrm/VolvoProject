using VolvoProject.Models;

namespace VolvoProject.Services.Interfaces
{
    public interface IVehicleService
    {
        IList<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int id);
        Vehicle GetByChassis(string chassisSeries, uint chassisNumber);
        bool InsertVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
    }
}
