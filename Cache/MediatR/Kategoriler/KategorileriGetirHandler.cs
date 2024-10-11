using Cache.MediatR.Urunler.Queries.UrunGetir;
using Cache.Servisler.Caching;
using MediatR;

namespace Cache.MediatR.Kategoriler
{
    public class KategorileriGetirHandler(CacheContext cacheContext) : IRequestHandler<KategorileriGetirRequest, IList<KategorileriGetirResponse>?>
    {
        public async Task<IList<KategorileriGetirResponse>?> Handle(KategorileriGetirRequest request, CancellationToken cancellationToken)
        {
            return cacheContext.Kategoris
                .Select(kategori => new KategorileriGetirResponse
                {
                    Id = kategori.Id,
                    Isim = kategori.Isim,
                    Uruns = kategori.Uruns.Select(u => new UrunGetirResponse
                    {
                        Id = u.Id,
                        Isim = u.Isim,
                    }).ToList()
                }).ToList();
        }
    }
}
