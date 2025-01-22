using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Homebroker.API
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> InitTransaction(InitTransactionDTO dto)
        {
            var order = await _service.InitTransaction(dto);

            return Ok(order);
        }
    }
}
