
using Cache.MediatR.Urunler.Commands.UrunEkle;
using Cache.MediatR.Urunler.Commands.UrunGuncelle;
using Cache.MediatR.Urunler.Commands.UrunSil;
using Cache.MediatR.Urunler.Queries.UrunGetir;
using Cache.MediatR.Urunler.Queries.UrunleriGetir;
using Cache.MediatR.Urunler.Queries.UrunleriGetirOData;
using Cache.Models;
using Cache.Servisler.Caching;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace Cache.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CacheContext _context;

        public UrunController(IMediator mediator, CacheContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> UrunleriGetirOData(CancellationToken cancellationToken, [FromHeader] int sayfaNumarasi = 1, [FromHeader] int sayfaBuyuklugu = 200)
        {
            // Sadece veri tabanında ürün yoksa veri ekleme işlemi yapılacak bu kontrolü yapmamun sebebi veri tabanına sürekli olarak veri pushlamasını engelleyerek sadece veri yoksa veri göndermesini sağlamaktı
            if (!await _context.Uruns.AnyAsync(cancellationToken))
            {
                await _context.Uruns.AddRangeAsync(new List<Urun>
        {
            new Urun { IsActive = true, Isim = "DenemeTrue", KategoriId = 3 },
            new Urun { IsActive = false, Isim = "DenemeFalse", KategoriId = 3 }
        });

                await _context.SaveChangesAsync(cancellationToken);
            }

            var response = await _mediator.Send(new UrunleriGetirODataRequest { SayfaNumarasi = sayfaNumarasi, SayfaBuyuklugu = sayfaBuyuklugu }, cancellationToken);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> UrunleriGetir(CancellationToken cancellationToken, [FromQuery] int sayfaNumarasi = 1, [FromQuery] int sayfaBuyuklugu = 10)
        {
            var response = await _mediator.Send(new UrunleriGetirRequest { SayfaNumarasi = sayfaNumarasi, SayfaBuyuklugu = sayfaBuyuklugu }, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> UrunGetir([FromQuery] int urunId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UrunGetirRequest { UrunId = urunId }, cancellationToken);
            if (response == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UrunEkle([FromBody] UrunEkleRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(UrunGetir), new { urunId = request.UrunIsim }, request); // İsim ile döndürün ya da uygun bir değeri kullanın
        }

        [HttpPut]
        public async Task<IActionResult> UrunGuncelle([FromBody] UrunGuncelleRequest request, CancellationToken cancellationToken)
        {
            
            if (request == null)
            {
                return BadRequest("İstek nesnesi boş.");
            }
            if (request.UrunId <= 0)
            {
                return BadRequest("Geçersiz ürün ID'si.");
            }

            var urun = await _mediator.Send(new UrunGetirRequest { UrunId = request.UrunId }, cancellationToken);
            if (urun == null)
            {
                return NotFound("Güncellenecek ürün bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(request.Isim))
            {
                return BadRequest("Ürün adı boş olamaz.");
            }

            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }



        [HttpDelete]
        public async Task<IActionResult> UrunSil([FromQuery] UrunSilRequest request, CancellationToken cancellationToken)
        {
            if (request.UrunId <= 0) // Geçersiz ID kontrolü
            {
                return BadRequest("Geçersiz ürün ID'si.");
            }

            await _mediator.Send(request, cancellationToken);
            return NoContent(); // 204 No Content
        }

    }
}
