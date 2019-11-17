using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController:Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<Order> _logger;

        public OrdersController(IDutchRepository repository, ILogger<Order> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders : {ex}");
                return BadRequest("Failed to get Information");
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if (order != null) return Ok(order);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders : {ex}");
                return BadRequest("Failed to get Information");
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order()
                    {
                        OrderDate = model.OrderDate,
                        OrderNumber = model.OrderNumber,
                        Id = model.OrderId
                    };
                    if (newOrder.OrderDate ==DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        var vm = new OrderViewModel()
                        {
                            OrderId = newOrder.Id,
                            OrderDate = newOrder.OrderDate,
                            OrderNumber = newOrder.OrderNumber
                        };

                        return Created($"/api/orders/{vm.OrderId}", vm);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex}");
            }
            return BadRequest("Failed to save the order");
        }

    }
}
