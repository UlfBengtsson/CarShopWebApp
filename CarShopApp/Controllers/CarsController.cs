using CarShopApp.Models;
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

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCarViewModel createCar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _carsService.Create(createCar);
                }
                catch (ArgumentException exception)
                {
                    ModelState.AddModelError("Model & Brand", exception.Message);
                    return View(createCar);
                }

                return RedirectToAction(nameof(ShowRoom));
            }
            return View(createCar);
        }

        public IActionResult Details(int id)
        {
            Car car = _carsService.FindById(id);

            if (car == null)
            {
                return RedirectToAction(nameof(ShowRoom));
                //return NotFound();//404
            }

            return View(car);
        }

        //------------------------ called by Ajax -----------------------------------------------------

        public IActionResult AjaxCarBrandList(string brand)
        {
            List<Car> cars = _carsService.FindByBrand(brand);

            if (cars != null)
            {
                return PartialView("_ListOfCars", cars);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult AjaxCarDetails(int id)
        {
            Car car = _carsService.FindById(id);

            if (car != null)
            {
                return PartialView("_CarDetails", car);
            }

            return NotFound();
        }

        public IActionResult LastCarAdded()
        {
            Car car = _carsService.LastAdded();

            if (car != null)
            {
                return PartialView("_CarRow", car);
            }

            return NotFound();
        }

        public IActionResult AjaxCarList()
        {
            List<Car> cars = _carsService.GetAll();

            if (cars != null)
            {
                return PartialView("_ListOfCars", cars);
            }

            return NotFound();
        }

        public IActionResult LastCarAddedJSON()
        {
            Car car = _carsService.LastAdded();

            if (car != null)
            {
                return Json(car);
            }

            return NotFound();
        }


    }
}
