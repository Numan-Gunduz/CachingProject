
using Cache.Models;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Servisler.Caching
{
    public class CacheContext : DbContextWithTriggers 
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
            Triggers<Kategori>.Inserting += entry => ClearCacheForTable("Kategoris");
            Triggers<Kategori>.Updating += entry => ClearCacheForTable("Kategoris");
            Triggers<Kategori>.Deleting += entry => ClearCacheForTable("Kategoris");

            // Urun tetikleyici tanımlamaları
            Triggers<Urun>.Inserting += entry => ClearCacheForTable("Uruns");
            Triggers<Urun>.Updating += entry => ClearCacheForTable("Uruns");
            Triggers<Urun>.Deleting += entry => ClearCacheForTable("Uruns");

            modelBuilder.Entity<Kategori>().HasData(GenerateKategoriData());
            modelBuilder.Entity<Urun>().HasData(GenerateUrunData());
        }

        public void ClearCacheForTable(string tableName)
        {
            Console.WriteLine($"Cache temizleniyor: {tableName}");
            _cache.Remove(tableName); // İlgili tablo için cache'i temizlemek için yazdım
            _cache.Remove("Urunler_1_200");
        }
        public List<Urun> GetUrunler(int sayfaNumarasi, int sayfaBuyuklugu)
        {
            var cacheKey = $"Urunler_{sayfaNumarasi}_{sayfaBuyuklugu}";

            return GetCachedData(cacheKey, () =>
            {
                // Veritabanından veri al
                return Uruns.Skip((sayfaNumarasi - 1) * sayfaBuyuklugu)
                            .Take(sayfaBuyuklugu)
                            .ToList();
            });
        }
        private T GetCachedData<T>(string cacheKey, Func<T> retrieveData)
        {
            if (_cache.TryGetValue(cacheKey, out T cachedData))
            {
                Console.WriteLine($"Cache'den veri Alındı: {cacheKey}");
                return cachedData;
            }

            Console.WriteLine($"Cache'den veri alınamadı, veritabanından getiriliyor: {cacheKey}");
            var data = retrieveData();
            _cache.Set(cacheKey, data);
            return data;
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
