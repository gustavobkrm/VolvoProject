using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using VolvoProject.Data;
using VolvoProject.Infrastructure;
using VolvoProject.Models;
using VolvoProject.Services;
using Xunit;
using static VolvoProject.Models.Vehicle;

namespace VolvoProject.Tests
{
    public class VehicleRepositoryTests
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly Mock<ILogger<VehicleRepository>> _loggerMock;
        private readonly ApplicationDbContext _dbContext;

        public VehicleRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_VehicleDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options);

            _loggerMock = new Mock<ILogger<VehicleRepository>>();

            _vehicleRepository = new VehicleRepository(_dbContext, _loggerMock.Object);
        }

        [Fact]
        public void GetAllVehicles_ShouldReturnListOfVehicles()
        {
            // Arrange
            _dbContext.Vehicles.Add(new Vehicle
            {
                VType = VehicleType.Truck,
                ChassisSeries = "ABC123",
                ChassisNumber = 123456,
                Color = "Red"
            });
            _dbContext.Vehicles.Add(new Vehicle
            {
                VType = VehicleType.Bus,
                ChassisSeries = "XYZ789",
                ChassisNumber = 654321,
                Color = "Blue"
            });
            _dbContext.SaveChanges();

            var vehicles = _vehicleRepository.GetAllVehicles();
            // Assert
            Assert.Equal(2, vehicles.Count);
        }

        [Fact]
        public void GetVehicleById_ShouldReturnVehicle()
        {
            var vehicle = new Vehicle
            {
                VType = VehicleType.Truck,
                ChassisSeries = "ABC123",
                ChassisNumber = 123456,
                Color = "Red"
            };
            _dbContext.Vehicles.Add(vehicle);
            _dbContext.SaveChanges();

            // Act
            var result = _vehicleRepository.GetVehicleById(vehicle.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicle.Id, result.Id);
            Assert.Equal(vehicle.ChassisSeries, result.ChassisSeries);
        }

        [Fact]
        public void GetByChassis_ShouldReturnVehicle()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                VType = VehicleType.Truck,
                ChassisSeries = "ABC123",
                ChassisNumber = 123456,
                Color = "Red"
            };
            _dbContext.Vehicles.Add(vehicle);
            _dbContext.SaveChanges();

            // Act
            var result = _vehicleRepository.GetByChassis(vehicle.ChassisSeries, vehicle.ChassisNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicle.ChassisSeries, result.ChassisSeries);
            Assert.Equal(vehicle.ChassisNumber, result.ChassisNumber);
        }

        [Fact]
        public void InsertVehicle_ShouldAddNewVehicle()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                VType = VehicleType.Bus,
                ChassisSeries = "XYZ123",
                ChassisNumber = 789012,
                Color = "Blue"
            };

            // Act
            _vehicleRepository.InsertVehicle(vehicle);

            // Assert
            var insertedVehicle = _dbContext.Vehicles.FirstOrDefault(v => v.ChassisSeries == vehicle.ChassisSeries);
            Assert.NotNull(insertedVehicle);
            Assert.Equal(vehicle.ChassisSeries, insertedVehicle.ChassisSeries);
            Assert.Equal(vehicle.ChassisNumber, insertedVehicle.ChassisNumber);
        }
    }
}
