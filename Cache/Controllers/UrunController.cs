
using Cache.MediatR.Urunler.Commands.UrunEkle;
using Cache.MediatR.Urunler.Commands.UrunGuncelle;
using Cache.MediatR.Urunler.Commands.UrunSil;
using Cache.MediatR.Urunler.Queries.UrunGetir;
using Cache.MediatR.Urunler.Queries.UrunleriGetir;
using Cache.Servisler.Caching;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CacheContext _context;
        private readonly IMemoryCache _cache; 

        public UrunController(IMediator mediator, CacheContext context, IMemoryCache cache)
        {
            _mediator = mediator;
            _context = context;
            _cache = cache; 
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> UrunleriGetirOData(CancellationToken cancellationToken, [FromHeader] int sayfaNumarasi = 1, [FromHeader] int sayfaBuyuklugu = 200)
        {

            var urunler = _context.GetUrunler(sayfaNumarasi, sayfaBuyuklugu);
            return Ok(urunler);
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
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UrunEkle([FromBody] UrunEkleRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            _context.ClearCacheForTable("Uruns");
            var urunler = _context.GetUrunler(1, 200);
            _cache.Set("Urunler_1_200", urunler);
            return CreatedAtAction(nameof(UrunGetir), new { urunId = request.UrunIsim }, request); 
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
            // Cache'i temizle
            _context.ClearCacheForTable("Uruns");

            // Güncel ürün listesini al ve cache'e ekle
            var guncellenenUrunler = _context.GetUrunler(1, 200); // Tüm ürünleri al
            _cache.Set("Urunler_1_200", guncellenenUrunler); // Cache'e güncel ürün listesi eklenmesini sağladom.
            return NoContent();
        }



        [HttpDelete]
        public async Task<IActionResult> UrunSil([FromQuery] UrunSilRequest request, CancellationToken cancellationToken)
        {
            if (request.UrunId <= 0) 
            {
                return BadRequest("Geçersiz ürün ID'si.");
            }
            _context.ClearCacheForTable("Uruns");

           
            var silinenUrunler = _context.GetUrunler(1, 200); 
            _cache.Set("Urunler_1_200", silinenUrunler); 

            await _mediator.Send(request, cancellationToken);
            return NoContent(); 
        }

    }
}
