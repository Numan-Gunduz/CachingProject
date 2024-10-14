using Cache.Models;

namespace Cache.MediatR.Urunler.Queries.UrunleriGetirOData
{
    public class UrunleriGetirODataResponse
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public int? KategoriId { get; set; }

        public Kategori Kategori { get; set; }

    }
}
