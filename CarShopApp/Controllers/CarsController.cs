using CarShopApp.Models.Repos;
using CarShopApp.Models.Services;
using CarShopApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Controllers
{
    public class CarsController : Controller
    {
        ICarsService _carsService;

        public CarsController()
        {
            _carsService = new CarsService(new InMemoryCarsRepo());
        }

        public IActionResult ShowRoom()
        {
            return View(_carsService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCarViewModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateCarViewModel createCar)
        {
            if (ModelState.IsValid)
            {
                _carsService.Create(createCar);

                return RedirectToAction(nameof(ShowRoom));
            }
            return View(createCar);
        }
    }
}
