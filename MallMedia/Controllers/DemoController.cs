using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController(IMasterDataRepository masterDataRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var res = await masterDataRepository.GetAllDevices();
            return Ok(res);
        }
    }
}
