using CarShopApp.Models;
using CarShopApp.Models.Repos;
using CarShopApp.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Controllers
{
    public class DemoController : Controller
    {
        private readonly ICarsService _carsService;

        public DemoController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SpaList()
        {
            return PartialView("_SpaListOfCars", _carsService.GetAll());
        }

        public IActionResult SpaDetails(int id)
        {
            Car car = _carsService.FindById(id);

            if (car == null)
            {
                return NotFound();
            }

            return PartialView("_SpaCarDetails", car);
        }

        public IActionResult SpaCarRow(int id)
        {
            Car car = _carsService.FindById(id);

            if (car == null)
            {
                return NotFound();
            }

            return PartialView("_SpaCarRow", car);
        }

        public IActionResult SpaCarDelete(int id)
        {
            Car car = _carsService.FindById(id);

            if (car == null)
            {
                return NotFound();//404
                //return BadRequest();//400
            }

            _carsService.Remove(id);

            return Ok();
        }
    }
}
