using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Infrastructure.Repositories
{
    public class DistributedCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public DistributedCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T GetData<T>(string key)
        {
            string cachedMember = _distributedCache.GetString(key);
            if (cachedMember is null)
                return default;
          return  JsonConvert.DeserializeObject<T>(cachedMember!);


        }

        public object RemoveData(string key)
        {
            string value = _distributedCache.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                 _distributedCache.Remove(key);
            }
            return true;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
             _distributedCache.SetString(key,JsonConvert.SerializeObject(value));
            return true;
        }
    }
}
