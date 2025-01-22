using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Homebroker.Controllers
{
    [ApiController]
    [Route("wallets")]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _service;

        public WalletsController(IWalletService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Wallet wallet)
        {
            await _service.Create(wallet);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }
    }
}
