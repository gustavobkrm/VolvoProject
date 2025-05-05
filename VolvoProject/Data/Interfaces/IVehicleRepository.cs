using VolvoProject.Models;

namespace VolvoProject.Data.Interfaces
{
    public interface IVehicleRepository
    {
        IList<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int id);
        Vehicle GetByChassis(string chassisSeries, uint chassisNumber);
        void InsertVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        Vehicle GetVehicleByChassisId(string chassisSeries, uint chassisNumber);

    }
}
