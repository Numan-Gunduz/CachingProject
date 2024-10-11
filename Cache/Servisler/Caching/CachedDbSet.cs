using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Collections;

namespace Cache.Servisler.Caching
{

    public class CachedDbSet<TEntity> :
        DbSet<TEntity>,
        IQueryable<TEntity>,
        IAsyncEnumerable<TEntity>,
        IEnumerable<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _innerDbSet;
        private readonly IMemoryCache _cache;
        private readonly CacheContext _context;

        public CachedDbSet(DbSet<TEntity> innerDbSet, IMemoryCache cache, CacheContext context)
        {
            _innerDbSet = innerDbSet;
            _cache = cache;
            _context = context;
            var queryable = AsQueryable();
            Provider = queryable.Provider;
            Expression = queryable.Expression;
        }

        public override IEntityType EntityType => _innerDbSet.EntityType;

        public Type ElementType => typeof(TEntity);

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public override IQueryable<TEntity> AsQueryable()
        {
            var entityTableName = GetTableName().Result;
            if (!_cache.TryGetValue(entityTableName, out IList<TEntity> _cachedEntities))
            {
                _cachedEntities = IliskiliVerileriEkle().Result;
                _cache.Set(entityTableName, _cachedEntities);
            }
            return _cachedEntities.AsQueryable();
        }

        private async Task<IList<TEntity>> IliskiliVerileriEkle()
        {
            var query = _innerDbSet.AsQueryable();

            // Tüm ilişkili verileri dinamik olarak yüklemek için Entity Framework'ün ChangeTracker özelliğini kullanıyoruz
            foreach (var navigation in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
            {
                query = query.Include(navigation.Name);
            }
            return await query.ToListAsync();
        }

        public override async IAsyncEnumerable<TEntity> AsAsyncEnumerable()
        {
            var entityTableName = GetTableName().Result;

            if (!_cache.TryGetValue(entityTableName, out IList<TEntity>? entities))
            {
                entities = await _innerDbSet.ToListAsync();
                _cache.Set(entityTableName, entities);
            }
            foreach (var entity in entities)
            {
                yield return entity;
            }
        }

        public IEnumerator<TEntity> GetEnumerator() => AsQueryable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default) => AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

        private async Task<string> GetTableName()
        {
            return await Task.FromResult(_context.Model.FindEntityType(typeof(TEntity))?.GetTableName()!);
        }
    }
}
