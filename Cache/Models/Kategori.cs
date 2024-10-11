namespace Cache.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Isim { get; set; }

        public ICollection<Urun>? Uruns { get; set; }
    }
}
