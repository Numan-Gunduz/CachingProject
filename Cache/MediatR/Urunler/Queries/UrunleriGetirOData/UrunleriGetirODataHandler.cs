using Cache.MediatR.Kategoriler;
using Cache.Servisler.Caching;
using MediatR;

namespace Cache.MediatR.Urunler.Queries.UrunleriGetirOData
{
    public class UrunleriGetirODataHandler(CacheContext cacheContext) : IRequestHandler<UrunleriGetirODataRequest, List<UrunleriGetirODataResponse>>
    {
        public async Task<List<UrunleriGetirODataResponse>> Handle(UrunleriGetirODataRequest request, CancellationToken cancellationToken)
        {
            return cacheContext.Uruns
                 .Skip((request.SayfaNumarasi - 1) * request.SayfaBuyuklugu)
                 .Take(request.SayfaBuyuklugu)
                 .Select(urun => new UrunleriGetirODataResponse
                 {
                     Id = urun.Id,
                     Isim = urun.Isim,
                     KategoriId=urun.KategoriId,
                     
                 }).ToList();
           

        }
    }
}
