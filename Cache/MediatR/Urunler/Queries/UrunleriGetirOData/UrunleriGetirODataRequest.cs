using MediatR;

namespace Cache.MediatR.Urunler.Queries.UrunleriGetirOData
{
    public class UrunleriGetirODataRequest : IRequest<List<UrunleriGetirODataResponse>>
    {
        public int SayfaBuyuklugu { get; set; }
        public int SayfaNumarasi { get; set; }

    }
}
