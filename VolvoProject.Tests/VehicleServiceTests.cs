using Microsoft.Extensions.Logging;
using Moq;
using VolvoProject.Data.Interfaces;
using VolvoProject.Models;
using VolvoProject.Services;
using static VolvoProject.Models.Vehicle;

namespace VolvoProject.Tests
{
    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepoMock;
        private readonly Mock<ILogger<VehicleService>> _loggerMock;
        private readonly VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _loggerMock = new Mock<ILogger<VehicleService>>();
            _vehicleRepoMock = new Mock<IVehicleRepository>();
            _vehicleService = new VehicleService(_vehicleRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAllVehicles_ShouldReturnListOfVehicles()
        {
            // Arrange
            var vehicles = new List<Vehicle> {
            new Vehicle(VehicleType.Truck, "ABC", 123, "Blue"),
            new Vehicle(VehicleType.Bus, "XYZ", 456, "Red")
        };

            _vehicleRepoMock.Setup(r => r.GetAllVehicles()).Returns(vehicles);

            // Act
            var result = _vehicleService.GetAllVehicles();

            // Assert
            Assert.Equal(2, result.Count);
            _vehicleRepoMock.Verify(r => r.GetAllVehicles(), Times.Once);
        }

        [Fact]
        public void GetAllVehicles_ShouldThrowException()
        {
            // Arrange
            _vehicleRepoMock.Setup(r => r.GetAllVehicles()).Throws(new Exception("DB error"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _vehicleService.GetAllVehicles());
            Assert.Contains("An error occurred while fetching all vehicles", ex.Message);
        }

        [Fact]
        public void GetVehicleById_ShouldReturnVehicle()
        {
            var vehicle = new Vehicle(VehicleType.Truck, "ABC", 123, "Blue") { Id = 1 };
            _vehicleRepoMock.Setup(r => r.GetVehicleById(1)).Returns(vehicle);

            var result = _vehicleService.GetVehicleById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetVehicleById_ShouldReturnNull()
        {
            _vehicleRepoMock.Setup(r => r.GetVehicleById(999)).Returns((Vehicle)null);

            var result = _vehicleService.GetVehicleById(999);

            Assert.Null(result);
        }

        [Fact]
        public void GetVehicleById_ShouldThrowException()
        {
            _vehicleRepoMock.Setup(r => r.GetVehicleById(It.IsAny<int>())).Throws(new Exception("DB fail"));

            var ex = Assert.Throws<Exception>(() => _vehicleService.GetVehicleById(5));

            Assert.Contains("An error occurred while fetching the vehicle with id", ex.Message);
        }

        [Fact]
        public void InsertVehicle_ShouldCallRepository()
        {
            var vehicle = new Vehicle(VehicleType.Truck, "DEF", 999, "Green");

            _vehicleService.InsertVehicle(vehicle);

            _vehicleRepoMock.Verify(r => r.InsertVehicle(It.Is<Vehicle>(
                v => v.VType == VehicleType.Truck && v.ChassisSeries == "DEF" && v.ChassisNumber == 999 && v.Color == "Green"
            )), Times.Once);
        }

        [Fact]
        public void InsertVehicle_ShouldThrowException()
        {
            var vehicle = new Vehicle(VehicleType.Bus, "AAA", 777, "Black");

            _vehicleRepoMock.Setup(r => r.InsertVehicle(It.IsAny<Vehicle>())).Throws(new Exception("Insert fail"));

            var ex = Assert.Throws<Exception>(() => _vehicleService.InsertVehicle(vehicle));

            Assert.Contains("An error occurred while inserting the vehicle", ex.Message);
        }

        [Fact]
        public void UpdateVehicle_ShouldCallRepository()
        {
            var vehicle = new Vehicle(VehicleType.Truck, "GHI", 111, "Blue") { Id = 10 };

            _vehicleService.UpdateVehicle(vehicle);

            _vehicleRepoMock.Verify(r => r.UpdateVehicle(vehicle), Times.Once);
        }

        [Fact]
        public void UpdateVehicle_ShouldThrowException()
        {
            var vehicle = new Vehicle(VehicleType.Truck, "GHI", 111, "Blue") { Id = 10 };

            _vehicleRepoMock.Setup(r => r.UpdateVehicle(vehicle)).Throws(new Exception("Update fail"));

            var ex = Assert.Throws<Exception>(() => _vehicleService.UpdateVehicle(vehicle));

            Assert.Contains("An error occurred while updating the vehicle with id 10", ex.Message);
        }
    }
}