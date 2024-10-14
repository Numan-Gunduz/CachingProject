
using System.ComponentModel.DataAnnotations;

namespace Cache.Models
{
    public class Urun
    {
        [Key]
        public int Id { get; set; }
        public string Isim { get; set; }
        public int? KategoriId { get; set; }

        public bool? IsActive { get; set; }
        public Kategori Kategori { get; set; }

    }
}
