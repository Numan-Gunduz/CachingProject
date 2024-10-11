
using Cache.MediatR.Kategoriler;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Threading.Tasks;

namespace Cache.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KategoriController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KategoriController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> KategorileriGetir()
        {
            var response = await _mediator.Send(new KategorileriGetirRequest());
            return Ok(response);
        }
    }
}

