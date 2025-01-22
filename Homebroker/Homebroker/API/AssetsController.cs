using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Homebroker.Controllers
{
    [ApiController]
    [Route("assets")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetsService _service;

        public AssetsController(IAssetsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Asset asset)
        {
            await _service.Create(asset);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }
    }
}
