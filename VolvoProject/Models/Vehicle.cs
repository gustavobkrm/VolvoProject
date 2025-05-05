namespace VolvoProject.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public VehicleType VType { get; set; }
        public int NumberOfPassengers { get; set; }
        public string Color { get; set; }

        public enum VehicleType
        {
            Bus,
            Truck,
            Car
        }

        public Vehicle() { }

        public Vehicle(VehicleType type, string chassisSeries, uint chassisNumber, string color)
        {
            VType = type;
            NumberOfPassengers = type switch
            {
                VehicleType.Bus => 42,
                VehicleType.Truck => 1,
                VehicleType.Car => 4,
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid vehicle type")
            };
            ChassisSeries = chassisSeries;
            ChassisNumber = chassisNumber;
            Color = color;
        }
    }
}

