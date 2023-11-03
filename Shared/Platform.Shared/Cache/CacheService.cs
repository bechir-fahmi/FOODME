using Microsoft.Extensions.Options;
using Platform.Shared.MicroservicesURLs;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Platform.Shared.Cache
{
    public class CacheService : ICacheService
    {
        private IDatabase _cacheDb;
        private readonly string _cachePrefix = "cache_";

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect(Microservice.Redis);
            _cacheDb = redis.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            RedisValue value = string.Empty;
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            if (_cacheDb != null)
            {
                value = _cacheDb.StringGet(key);
            }
            if (!string.IsNullOrEmpty(value))
            {
                Console.WriteLine($"Data for key {key} was fetched from cache");
                return JsonSerializer.Deserialize<T>(value, options);
            }
            Console.WriteLine($"Data for key {key} was fetched from database");

            return default;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true 
            };
            if (_cacheDb != null)
            {
                var expiryTime = expirationTime - DateTimeOffset.Now;
                return _cacheDb.StringSet(key, JsonSerializer.Serialize(value, options), expiryTime);
            }
            return false;
            
        }

        public bool RemoveData(string key)
        {
          var cacheRemoveResult = _cacheDb.KeyDelete(key);
           return cacheRemoveResult;

        }
    }
}
