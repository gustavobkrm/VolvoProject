using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VolvoProject.Models;
using VolvoProject.Services.Interfaces;

namespace VolvoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;


        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        public IActionResult Index(string? chassisId)
        {
            try
            {
                var allVehicles = _vehicleService.GetAllVehicles();

                if (!string.IsNullOrEmpty(chassisId))
                {
                    allVehicles = allVehicles
                        .Where(v => (v.ChassisSeries + v.ChassisNumber.ToString()) == chassisId)
                        .ToList();
                }

                return View(allVehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar veículos");
                return View(new List<Vehicle>());
            }
        }


        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                if (!_vehicleService.InsertVehicle(vehicle))
                {
                    ModelState.AddModelError(string.Empty, "A vehicle with this chassis Id already exists.");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(vehicle);
        }

        public IActionResult Update(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, string color)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
                return NotFound();

            vehicle.Color = color;
            _vehicleService.UpdateVehicle(vehicle);

            return RedirectToAction("Index");
        }

    }
}
