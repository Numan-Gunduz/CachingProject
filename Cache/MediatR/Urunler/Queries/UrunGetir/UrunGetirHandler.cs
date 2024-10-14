using Cache.Models;
using Cache.Servisler.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.MediatR.Urunler.Queries.UrunGetir
{
    public class UrunGetirHandler(CacheContext context, IMemoryCache cache) : IRequestHandler<UrunGetirRequest, UrunGetirResponse>
    {
        private readonly string _cacheUrunListKey = "urun_list";

        public async Task<UrunGetirResponse> Handle(UrunGetirRequest request, CancellationToken cancellationToken)
        {
            var start = DateTime.UtcNow;

            if (cache.TryGetValue(_cacheUrunListKey, out List<Urun>? cachedUrunler))
            {
                if (cachedUrunler != null && cachedUrunler.Count != 0)
                {
                    var cachedUrun = cachedUrunler.FirstOrDefault(u => u.Id == request.UrunId);

                    if (cachedUrun != null)
                    {
                        var endCache = DateTime.UtcNow;
                        var durationCache = (endCache - start).TotalMilliseconds;
                        Console.WriteLine($"Cache'den veri çekme süresi: {durationCache} ms");
                        return new UrunGetirResponse
                        {
                            Id = cachedUrun.Id,
                            Isim = cachedUrun.Isim
                        };
                    }
                }
                return await context.Uruns.Where(u => u.Id == request.UrunId).Select(u => new UrunGetirResponse
                {
                    Id = u.Id,
                    Isim = u.Isim
                }).FirstAsync(cancellationToken);
            }
            var end = DateTime.UtcNow;
            var duration = (end - start).TotalMilliseconds;
            Console.WriteLine($"Veritabanından veri çekme ve cache'e ekleme süresi: {duration} ms");

            return await context.Uruns.Where(u => u.Id == request.UrunId).Select(u => new UrunGetirResponse
            {
                Id = u.Id,
                Isim = u.Isim
            }).FirstAsync(cancellationToken);
        }
    }
}
