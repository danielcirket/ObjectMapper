using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielCirket.ObjectMapper
{
    public interface ICacheHelper
    {
        void SaveToCache(string cacheKey, object savedItem, DateTime expirationTime);

        T GetFromCache<T>(string cacheKey) where T : class;
        
        void RemoveFromCache(string cacheKey);
        
        bool IsInCache(string cacheKey);
    }
}
