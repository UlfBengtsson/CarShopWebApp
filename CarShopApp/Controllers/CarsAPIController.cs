using CarShopApp.Models;
using CarShopApp.Models.Services;
using CarShopApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarShopApp.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsAPIController : ControllerBase
    {
        private readonly ICarsService _carsService;

        public CarsAPIController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        // GET: api/<CarsAPIController>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            IEnumerable<Car> list = _carsService.GetAll();

            foreach (var item in list)
            {
                item.Brand.Cars = null;
            }

            return list;
        }

        // GET api/<CarsAPIController>/5
        [HttpGet("{id}")]
        public Car Get(int id)
        {
            Car car = _carsService.FindById(id);
            car.Brand.Cars = null;
            foreach (var item in car.Insurances)
            {
                item.Car = null;
                if (item.Insurance != null)
                {
                    item.Insurance.CarsCoverd = null;
                }
            }
            return car;
        }

        // POST api/<CarsAPIController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public void Post([FromBody] CreateCarViewModel createCar)
        {
            Car car = _carsService.Create(createCar);
            if (car != null)
            {
                Response.StatusCode = 201;//Created
            }
            else
            {
                Response.StatusCode = 400;//Bad request
            }
        }

        // PUT api/<CarsAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CreateCarViewModel editCar)
        {
            _carsService.Edit(id, editCar);
        }

        // DELETE api/<CarsAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _carsService.Remove(id);
        }
    }
}
