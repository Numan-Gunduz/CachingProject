using Cache.Models;

namespace ApiConsumer.Models
{
    public class UrunEkle
    {

        public string Isim { get; set; }

        public int KategoriId { get; set; }

        public bool IsActive { get; set; } = true;
        public Kategori? Kategori { get; set; }

    }
}
