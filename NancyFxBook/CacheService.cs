// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 13/05/2015
// // Module           : CacheService.cs
// // Purpose          : Static class to provide an output cache for the cache demo
// //===========================================================================================

using System;
using System.Collections.Specialized;
using System.Runtime.Caching;
using Nancy;

namespace nancybook
{
  public class CacheService
  {
    private static readonly NameValueCollection _config = new NameValueCollection();
    private readonly MemoryCache _cache = new MemoryCache("NancyCache", _config);

    private readonly CacheItemPolicy _standardPolicy = new CacheItemPolicy
    {
      Priority = CacheItemPriority.NotRemovable,
      SlidingExpiration = TimeSpan.FromSeconds(30) // This can be changed to FromMinutes/FromHours etc it's 30 secs for testing purposes
    };

    public void AddItem(string itemKey, Response itemToAdd)
    {
      _cache.Add(new CacheItem(itemKey, itemToAdd), _standardPolicy);
    }

    public Response GetItem(string itemKey)
    {
      return (Response)_cache.Get(itemKey);
    }

  }
}
