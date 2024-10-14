//using Cache.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.Extensions.Caching.Memory;
//using System.Collections;
//using System.Linq.Expressions;

//namespace Cache.Servisler.Caching
//{
//    public class CacheContext : Context
//    {
//        private readonly IMemoryCache _cache;

//        public CacheContext(DbContextOptions<CacheContext> options, IMemoryCache cache) : base(options)
//        {
//            _cache = cache;
//        }

//        public override DbSet<Kategori> Kategoris
//        {
//            get
//            {
//                return new CachedDbSet<Kategori>(base.Kategoris, _cache, this);
//            }
//            set
//            {
//                base.Kategoris = value;
//            }
//        }

//        public override DbSet<Urun> Uruns
//        {
//            get
//            {
//                return new CachedDbSet<Urun>(base.Uruns, _cache, this);
//            }
//            set
//            {
//                base.Uruns = value;
//            }
//        }

//        // DisposeAsync method implementation
//        public override async ValueTask DisposeAsync()
//        {
//            await base.DisposeAsync();
//        }

//        public override void Dispose()
//        {
//            base.Dispose();
//        }
//    }

//}
using Cache.Models;
using EntityFrameworkCore.Triggered; // Triggered paketi
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Servisler.Caching
{
    public class CacheContext : DbContextWithTriggers // DbContextWithTriggers kullanarak tetikleyici işlevselliğini etkinleştiriyoruz
    {
        private readonly IMemoryCache _cache;

        public CacheContext(DbContextOptions<CacheContext> options, IMemoryCache cache) : base(options)
        {
            _cache = cache;
        }

        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Urun> Uruns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kategori tetikleyici tanımlamaları
            Triggers<Kategori>.Inserting += entry =>
            {
                ClearCacheForTable("Kategoris");
            };

            Triggers<Kategori>.Updating += entry =>
            {
                ClearCacheForTable("Kategoris");
            };

            Triggers<Kategori>.Deleting += entry =>
            {
                ClearCacheForTable("Kategoris");
            };

            // Urun tetikleyici tanımlamaları
            Triggers<Urun>.Inserting += entry =>
            {
                ClearCacheForTable("Uruns");
            };

            Triggers<Urun>.Updating += entry =>
            {
                ClearCacheForTable("Uruns");
            };

            Triggers<Urun>.Deleting += entry =>
            {
                ClearCacheForTable("Uruns");
            };

            // Model yaratımı
            //modelBuilder.Entity<Urun>(entity =>
            //{
            //    entity.ToView("ActiveUruns"); // Görünümün adı
            //    entity.HasOne(a => a.Kategori)
            //          .WithMany(d => d.Uruns)
            //          .HasForeignKey(a => a.KategoriId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            modelBuilder.Entity<Kategori>().HasData(GenerateKategoriData());
            modelBuilder.Entity<Urun>().HasData(GenerateUrunData());
        }

        private void ClearCacheForTable(string tableName)
        {
            _cache.Remove(tableName); // İlgili tablo için cache'i temizliyoruz
        }

        private List<Kategori> GenerateKategoriData()
        {
            var kategoriler = new List<Kategori>();
            for (int i = 1; i <= 10; i++)
            {
                kategoriler.Add(new Kategori
                {
                    Id = i,
                    Isim = $"Kategori-{i}"
                });
            }
            return kategoriler;
        }

        private List<Urun> GenerateUrunData()
        {
            var urunler = new List<Urun>();
            for (int i = 1; i <= 10; i++)
            {
                urunler.Add(new Urun
                {
                    Id = i,
                    Isim = $"Urun-{i}",
                    IsActive = true,
                    KategoriId = i,
                });
            }
            return urunler;
        }
    }
}
