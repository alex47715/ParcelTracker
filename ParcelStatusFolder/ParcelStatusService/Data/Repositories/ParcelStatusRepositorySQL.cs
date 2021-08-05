using DapperLibrarie;
using Microsoft.Extensions.Options;
using ParcelStatusService.AppSettings;
using ParcelStatusService.Data.SqlRepository.SQLQueries;
using System.Threading.Tasks;

namespace ParcelStatusService.Data.SqlRepository
{
    public interface IParcelStatusRepositorySql
    {
        Task SetStatus(StatusENUM parcelStatus, string id);
        Task<string> GetStatus(string id);
        Task RemoveStatus(string id);
        Task UpdateStatus(StatusENUM parcelStatus, string id);
    }
    class ParcelStatusRepositorySQL : DapperRepository, IParcelStatusRepositorySql
    {
        public ParcelStatusRepositorySQL(IOptions<ConnectionStrings> options) : base(options.Value.MSSQLDatabase)
        {
        }

        public async Task<string> GetStatus(string id)
        {
            return await this.GetSingleOrDefault(ParcelStatusQueries.GetStatus, new
            {
                idParcel = id.ToString()
            });
        }

        public async Task RemoveStatus(string id)
        {
            await this.ExecuteAsync(ParcelStatusQueries.RemoveStatus, new
            {
                idParcel = id.ToString()
            });
        }

        public async Task SetStatus(StatusENUM parcelStatus, string id)
        {
            await this.ExecuteAsync(ParcelStatusQueries.SetStatus, new
            {
                idParcel = id,
                Status = parcelStatus.ToString(),
            });
        }

        public async Task UpdateStatus(StatusENUM parcelStatus, string id)
        {
            await this.ExecuteAsync(ParcelStatusQueries.UpdateStatus, new
            {
                idParcel = id,
                Status = parcelStatus.ToString(),
            });
        }
    }
}
