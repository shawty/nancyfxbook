using System;
using nancybook.Models;
using Nancy;

namespace nancybook.modules
{
  public class CachingRoutes : NancyModule
  {
    private readonly CacheService _myCache;

     public CachingRoutes(CacheService myCache) : base("/caching")
     {
       _myCache = myCache;

       Get["/"] = x =>
       {
         var cacheData = new CacheDemo() {WhenRequested = DateTime.Now};
         return View["caching/index.html", cacheData];
       };

       Before += ctx =>
       {
         string key = ctx.Request.Path;

         var cacheObject = _myCache.GetItem(key);
         return cacheObject;
       };

       After += ctx =>
       {
         if(ctx.Response.StatusCode != HttpStatusCode.OK)
         {
           return;
         }

         string key = ctx.Request.Path;
         if(_myCache.GetItem(key) == null)
           _myCache.AddItem(key, ctx.Response);
       };

     }

  }
}