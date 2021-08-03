using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ParcelStatusService.Data.RedisRepository
{
    public interface IParcelStatusRepositoryRedis
    {
        Task SetStatus(StatusENUM parcelStatus, int id);
        Task<string> GetStatus(string id);
        Task RemoveStatus(string id);
    }
    class ParselStatusRepositoryRedis : IParcelStatusRepositoryRedis
    {
        private readonly IDistributedCache _distributedCache;
        public ParselStatusRepositoryRedis(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<string> GetStatus(string id)
        {
            var answer = await _distributedCache.GetAsync(id);
            if (answer == null)
                throw new Exception($"Status with id={id} not found");
            else
                return Encoding.UTF8.GetString(answer);
                        
        }

        public async Task RemoveStatus(string id)
        {
            await _distributedCache.RemoveAsync(id.ToString());
        }

        public async Task SetStatus(StatusENUM parcelStatus, int id)
        {
            await _distributedCache.SetStringAsync(id.ToString(), parcelStatus.ToString());
        }
    }
}
