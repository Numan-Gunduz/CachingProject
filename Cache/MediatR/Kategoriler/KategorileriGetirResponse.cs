using Cache.MediatR.Urunler.Queries.UrunGetir;

namespace Cache.MediatR.Kategoriler
{
    public class KategorileriGetirResponse
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public ICollection<UrunGetirResponse>? Uruns { get; set; }
    }
}
