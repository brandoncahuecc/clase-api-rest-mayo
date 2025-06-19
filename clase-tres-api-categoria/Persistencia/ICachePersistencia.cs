using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace clase_tres_api_categoria.Persistencia
{
    public interface ICachePersistencia
    {
        Task<T> GetCache<T>(string key);
        Task SetCache<T>(string key, T valor);
    }

    public class CachePersistencia : ICachePersistencia
    {
        private readonly IDistributedCache _cache;

        public CachePersistencia(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetCache<T>(string key)
        {
            try
            {
                string stringCache = await _cache.GetStringAsync(key) ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(stringCache))
                    return JsonConvert.DeserializeObject<T>(stringCache);
                return default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public async Task SetCache<T>(string key, T valor)
        {
            try
            {
                if(valor is not null)
                {
                    TimeSpan time = TimeSpan.FromSeconds(20);
                    DistributedCacheEntryOptions options = new();
                    options.SetAbsoluteExpiration(time);

                    await _cache.SetStringAsync(key, JsonConvert.SerializeObject(valor), options);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
