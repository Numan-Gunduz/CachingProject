using Cache.Models;
using Cache.Servisler.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.MediatR.Urunler.Queries.UrunleriGetir
{
    public class UrunleriGetirHandler(Context context, CacheContext cacheContext, IMemoryCache cache) : IRequestHandler<UrunleriGetirRequest, IList<UrunleriGetirResponse>?>
    {
        private readonly string _cacheUrunListKey = "urun_list";

        public async Task<IList<UrunleriGetirResponse>?> Handle(UrunleriGetirRequest request, CancellationToken cancellationToken)
        {
            var start = DateTime.UtcNow;

            var cachedUrunList = cache.Get<List<Urun>>(_cacheUrunListKey);

            List<Urun>? urunler;

            if (cachedUrunList == null)
            {
                //Sayfalamada gösterilecek kadar veri cachelenebilir veya bütün veri cachelenir. 
                urunler = await context.Uruns.ToListAsync(cancellationToken);


                cache.Set(_cacheUrunListKey, urunler);

                var pagginatedUrunler = urunler
                    .Skip((request.SayfaNumarasi - 1) * request.SayfaBuyuklugu)
                    .Take(request.SayfaBuyuklugu)
                    .ToList();
                var end = DateTime.UtcNow;
                var duration = (end - start).TotalMilliseconds;
                Console.WriteLine($"Veritabanından veri çekme süresi: {duration} ms");

                return pagginatedUrunler.Select(m => new UrunleriGetirResponse
                {
                    Id = m.Id,
                    Isim = m.Isim,
                }).ToList(); ;
            }

            urunler = cachedUrunList
                .Skip((request.SayfaNumarasi - 1) * request.SayfaBuyuklugu)
                .Take(request.SayfaBuyuklugu)
                .ToList();

            var endCache = DateTime.UtcNow;
            var durationCache = (endCache - start).TotalMilliseconds;
            Console.WriteLine($"Cache'den veri çekme süresi: {durationCache} ms");

            return urunler.Select(m => new UrunleriGetirResponse
            {
                Id = m.Id,
                Isim = m.Isim,
            }).ToList();
        }
    }
}
