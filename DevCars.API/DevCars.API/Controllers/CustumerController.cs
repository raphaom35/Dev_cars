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
    [Route("api/custumer")]
    public class CustumerController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        public CustumerController(DevCarsDbContext Context)
        {
            _dbContext = Context;
        }  
        [HttpPost]
        public IActionResult Post([FromBody] AddCustumerInputModel model)
        {
            var custumer = new Custumer(4, model.FullName, model.Document, model.BirthDate);
            _dbContext.Custumers.Add(custumer);
            return NoContent();
        }
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id,[FromBody] AddOrderInputModel model)
        {
            var extraItems = model.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();
            var car = _dbContext.Cars.SingleOrDefault(c => c.id == model.IdCar);
            var order = new Order(1, model.IdCar, model.IdCustumer, car.Price, extraItems);
            var custumer = _dbContext.Custumers.SingleOrDefault(c => c.Id == model.IdCustumer);
            custumer.Purchase(order);
            return CreatedAtAction(
                
                nameof(GetOrder),
                new { id=custumer.Id, orderid=order.id },
                model);
        }
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var custumer = _dbContext.Custumers.SingleOrDefault(c => c.Id == id);
            if (custumer == null)
            {
                return NotFound();
            }
            var order = custumer.Orders.SingleOrDefault(o => o.id == orderid);
            var extraItens = order.ExtraItems.Select(e => e.Descrition).ToList();
            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCurtumer, order.TotalCost, extraItens);
            return Ok(orderViewModel);
        }
        
    }
}
