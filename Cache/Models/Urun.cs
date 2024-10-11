
namespace Cache.Models
{
    public class Urun
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public int? KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        public bool? IsActive { get; set; }

    }
}
