using System.ComponentModel.DataAnnotations;

namespace Cache.Models
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }
        public string Isim { get; set; }

        public ICollection<Urun> Uruns { get; set; }
    }
}
