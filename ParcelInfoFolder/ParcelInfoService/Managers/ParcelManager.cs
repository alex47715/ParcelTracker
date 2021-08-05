using Microsoft.Extensions.Logging;
using ParcelInfoService.Data.MongoRepository;
using ParcelInfoService.Domain;
using ParcelInfoService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParcelInfoService.Managers
{
    public interface IParcelManager
    {
        Task<bool> CreateParcel(Parcel parcel);
        Task<bool> DeleteParcel(string id);
        Task<ParcelModel> GetParcelById(string id);
        Task<List<ParcelModel>> GetParcels();
    }
    public class ParcelManager : IParcelManager
    {
        private readonly ILogger<ParcelManager> _logger;
        private readonly IParcelInfoRepository _parcelInfoRepository;
        public ParcelManager(ILogger<ParcelManager> logger,IParcelInfoRepository parcelInfoRepository)
        {
            _logger = logger;
            _parcelInfoRepository = parcelInfoRepository;
        }
        public async Task<bool> CreateParcel(Parcel parcel)
        {
            return await _parcelInfoRepository.AddParcel(parcel);
        }

        public async Task<bool> DeleteParcel(string id)
        {
            return await _parcelInfoRepository.DeleteParcel(id);
        }

        public async Task<ParcelModel> GetParcelById(string id)
        {
            return await _parcelInfoRepository.GetParcelById(id);
        }

        public async Task<List<ParcelModel>> GetParcels()
        {
            return await _parcelInfoRepository.GetParcels();
        }
    }
}
