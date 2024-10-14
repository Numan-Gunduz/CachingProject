//using Microsoft.EntityFrameworkCore;

//namespace Cache.Models
//{
//    public class Context : DbContext
//    {

//        public Context(DbContextOptions<Context> options)
//        : base(options)
//        {
//        }
//        protected Context(DbContextOptions options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<Urun> Uruns { get; set; }
//        public virtual DbSet<Kategori> Kategoris { get; set; }



//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            /*  CREATE VIEW ActiveUruns AS
//                SELECT *
//                FROM Uruns
//                WHERE IsActive = 1;
//            */
//            modelBuilder.Entity<Urun>(entity =>
//            {
//                entity.ToView("ActiveUruns"); // Görünümün adı
//            });

//            modelBuilder.Entity<Kategori>().HasData(GenerateKategoriData());
//            modelBuilder.Entity<Urun>().ToTable("Uruns");
//            modelBuilder.Entity<Urun>().HasData(GenerateUrunData());

//            modelBuilder.Entity<Urun>()
//               .HasOne(a => a.Kategori)
//               .WithMany(d => d.Uruns)
//               .HasForeignKey(a => a.KategoriId)
//               .OnDelete(DeleteBehavior.Cascade);

//            modelBuilder.Entity<Urun>(entry =>
//            {
//                entry.ToTable("Uruns", tb => tb.HasTrigger("UrunsTableChangeTrigger"));
//            });

//            base.OnModelCreating(modelBuilder);

//        }
//        private List<Kategori> GenerateKategoriData()
//        {
//            var kategoriler = new List<Kategori>();
//            for (int i = 1; i <= 10; i++)
//            {
//                kategoriler.Add(new Kategori
//                {
//                    Id = i,
//                    Isim = $"Kategori-{i}"
//                });
//            }
//            return kategoriler;
//        }


        
//        private List<Urun> GenerateUrunData()
//        {
//            var urunler = new List<Urun>();
//            for (int i = 1; i <= 10; i++)
//            {
//                urunler.Add(new Urun
//                {
//                    Id = i,
//                    Isim = $"Urun-{i}",
//                    IsActive = true,
//                    KategoriId = i,
//                });
//            }
//            return urunler;
//        }
//    }
//}
