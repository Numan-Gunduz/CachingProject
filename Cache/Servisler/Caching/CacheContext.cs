using Cache.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Linq.Expressions;

namespace Cache.Servisler.Caching
{
    public class CacheContext : Context
    {
        private readonly IMemoryCache _cache;

        public CacheContext(DbContextOptions<CacheContext> options, IMemoryCache cache) : base(options)
        {
            _cache = cache;
        }

        public override DbSet<Kategori> Kategoris
        {
            get
            {
                return new CachedDbSet<Kategori>(base.Kategoris, _cache, this);
            }
            set
            {
                base.Kategoris = value;
            }
        }

        public override DbSet<Urun> Uruns
        {
            get
            {
                return new CachedDbSet<Urun>(base.Uruns, _cache, this);
            }
            set
            {
                base.Uruns = value;
            }
        }

        // DisposeAsync method implementation
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

}
