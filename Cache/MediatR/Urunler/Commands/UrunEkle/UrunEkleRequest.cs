using MediatR;

namespace Cache.MediatR.Urunler.Commands.UrunEkle
{
    public class UrunEkleRequest : IRequest
    {
        public string UrunIsim { get; set; }
        public int KategoriId { get; set; }


    }
}
