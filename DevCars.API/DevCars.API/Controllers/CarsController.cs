using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Presistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        public CarsController(DevCarsDbContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var cars = _dbContext.Cars;
            var carViewModel = cars
                .Select(c => new CarItemViewModel(c.id, c.Brand, c.Model, c.Price))
                .ToList();
            return Ok(carViewModel);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c=>c.id==id);
            if(car == null)
            {
                return NotFound();
            }
            var carDetailsViewModel = new CarDetailsViewModel(car.id, car.Brand, car.Model, car.VinCode, car.Year, car.Color, car.ProdutionDate);
            return Ok(carDetailsViewModel);
        }
        [HttpPost]
        public IActionResult Post([FromBody] AddCarsInputModel model)
        {
            if (model.Model.Length > 50)
            {
                return BadRequest("Modelo no pode ser mais que 50 caracteres");
            }
            var car = new Car(4,model.Brand,model.Model,model.VinCode,model.Price,model.Year,model.Color,model.ProdutionDate);
            _dbContext.Cars.Add(car);

            return CreatedAtAction(
                nameof(GetById),
                new { id=car.id},
                model);
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UpdateCarsInputModel model,int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.id == id);
            if (car == null)
            {
                return NotFound();
            }
            car.Update(model.Price, model.Color);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.id == id);
            if (car == null)
            {
                return NotFound();
            }
            car.SetAsSuspended();
            return NoContent();
        }
    }
}
