using MediatR;

namespace Cache.MediatR.Urunler.Commands.UrunGuncelle
{
    public class UrunGuncelleRequest : IRequest
    {
        public int UrunId { get; set; }
        public string Isim { get; set; }
        public int KategoriId { get; set; }
    }
}
