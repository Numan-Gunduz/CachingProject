using MediatR;

namespace Cache.MediatR.Urunler.Queries.UrunleriGetir
{
    public class UrunleriGetirRequest : IRequest<IList<UrunleriGetirResponse>?>
    {
        public int SayfaBuyuklugu { get; set; }
        public int SayfaNumarasi { get; set; }
    }
}
