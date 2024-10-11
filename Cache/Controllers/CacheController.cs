
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public CacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpPut]
        public async Task<IActionResult> ResetCache(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return BadRequest("Tablo boş değil.");
            }

            await Task.Run(() => _memoryCache.Remove(tableName));

 
       
            Console.WriteLine("Cache Temizlendi. Tablo Adı: " + tableName);
        

            return Ok("Cache reset successfully for table: " + tableName);
        }
    }
}
