using Dapper;
using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Presistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    /// <summary>
    /// Cadastrar um carros
    /// </summary>
    /// <remarks>
    /// Requisição de exemplo
    /// {
    ///     "brand":"Toyota",
    ///     "model":"Civic",
    ///     "vinCode":"abc123",
    ///     "year":2021,
    ///     "color":"Ciza",
    ///     "productionDate":"2021-05-04"
    /// }
    /// </remarks>
    /// <param>Dados do carro </param>
    /// <return>Objeto recém-criado</return>
    /// <response code="201" >Objeto criado com sucesso</response>
    /// <response code="404" >Dados invalidos</response>
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connetionsString;
        public CarsController(DevCarsDbContext context,IConfiguration configuration)
        {
            _connetionsString = configuration.GetConnectionString("DevCarsCs");
            _dbContext = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            //var cars = _dbContext.Cars;
            //var carViewModel = cars
            //    .Select(c => new CarItemViewModel(c.id, c.Brand, c.Model, c.Price))
            //    .ToList();
            using (var sqlConnection =new SqlConnection(_connetionsString))
            {
                var query = "select Id,Brand,ModelPrice from Cars where Status=0;";
                var carViewModel = sqlConnection.Query<CarItemViewModel>(query);
                return Ok(carViewModel);
            }
            
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
            var car = new Car(model.Brand,model.Model,model.VinCode,model.Price,model.Year,model.Color,model.ProdutionDate);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
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
            using (var sqlConnection = new SqlConnection(_connetionsString))
            {
                var query = "Update Cars set Color=@color,Price=Price where Id=@id";
                 sqlConnection.Execute(query,new { color = model.Color, price = model.Price, car.id });
            }
                // _dbContext.SaveChanges();
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
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
