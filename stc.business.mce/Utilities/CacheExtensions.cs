using Core.Cache;
using Core.Common;
using Core.Log.Interface;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce
{
    public static class CacheExtensions
    {
        public static async Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, Func<Task<T>> argument)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = await argument.Invoke();
            cacheManager.Set<T>(key, result, ConfigDefaults.CacheTime); ;

            return result;
        }

        public static void ClearCache(this List<string> cacheKeys)
        {
            try
            {
                var conn = Engine.ContainerManager.Resolve<IRedisConnectionWrapper>("CacheRedisConnection");
                Config.IsRedisAvailable = conn.IsAvailable();

                var cacheManager = Engine.ContainerManager.Resolve<ICacheManager>("main_cache");
                var memManager = Engine.ContainerManager.Resolve<ICacheManager>("memorycache");
                cacheManager.Clear();
                memManager.Clear();
                if (cacheKeys != null && cacheKeys.Any())
                {
                    cacheKeys.ToList().ForEach(x => { memManager.Remove(x); });
                }

                var logger = Engine.ContainerManager.Resolve<ILogger>();
                var endPoint = conn.GetEndpoints().First();
                var server = conn.Server(endPoint);
                var subscribers = server.SubscriptionChannels();

                if (subscribers.Count() > 0)
                {
                    var idlog = new Core.Log.LogIdentify()
                    {
                        ProcessID = Guid.NewGuid().ToString()
                    };

                    var srtLog = new StringBuilder();
                    srtLog.Append("Clear cache client: ").Append("\r\n");

                    var sub = conn.GetConnection().GetSubscriber();

                    foreach (var subscriber in subscribers)
                    {
                        cacheKeys.ForEach(x =>
                        {
                            var jsonObject = new JObject
                            {
                                { "SchemaName", x.Contains(".") ? x.Split('.')[0] : "" },
                                { "TableName", x.ToString() }
                            };

                            sub.Publish(subscriber.ToString(), jsonObject.ToString(), CommandFlags.FireAndForget);
                        });
                    }
                    logger.Info(idlog, srtLog.ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}