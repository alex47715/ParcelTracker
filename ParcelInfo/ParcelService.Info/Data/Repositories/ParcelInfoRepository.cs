using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoLib;
using ParcelInfoService.AppSettings;
using ParcelInfoService.Domain;
using ParcelInfoService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParcelInfoService.Data.MongoRepository
{
    public interface IParcelInfoRepository
    {
        Task<bool> AddParcel(Parcel parcel);
        Task<bool> DeleteParcel(string id);
        Task<ParcelModel> GetParcelById(string id);
        Task<List<ParcelModel>> GetParcels();
    }
    internal class ParcelInfoRepository : BaseRepository, IParcelInfoRepository
    {
        protected string _connectionString { get; }

        public ParcelInfoRepository(IOptions<ConnectionStrings> options) : base(options.Value.MongoDatabase)
        {
            
        }
        public IMongoCollection<BsonDocument> ParcelsCollection
        {
            get { return this.GetCollection("Parcels"); }
        }
        public async Task<bool> AddParcel(Parcel parcel)
        {
            await ParcelsCollection.InsertOneAsync(parcel.ToBsonDocument());
            return true;
        }

        public async Task<bool> DeleteParcel(string id)
        {
            var result = await ParcelsCollection.DeleteOneAsync(new BsonDocument("ParcelId",id));
            if (result.DeletedCount == 0)
                throw new Exception("Cant delete parcel");
            return true;
        }

        public async Task<ParcelModel> GetParcelById(string id)
        {
            var projection = Builders<BsonDocument>.Projection.Expression(p => new ParcelModel { ParcelId = p.GetValue("ParcelId").ToString(), SenderName = p.GetValue("SenderName").ToString(), RecipientName = p.GetValue("RecipientName").ToString(), Weight = p.GetValue("Weight").ToDouble()});
            var result = await ParcelsCollection.Find(new BsonDocument("ParcelId", id)).Project(projection).FirstOrDefaultAsync();

            if (result == null)
                throw new Exception($"Parcel with id={id} not found");
            if (result == null)
                throw new Exception("Parcel is null for return");
            return result;
        }

        public async Task<List<ParcelModel>> GetParcels()
        {
            var builder = new FilterDefinitionBuilder<ParcelModel>();
            var filter = new BsonDocument();
            var projection = Builders<BsonDocument>.Projection.Expression(p => new ParcelModel { ParcelId = p.GetValue("ParcelId").ToString(), SenderName = p.GetValue("SenderName").ToString() });
            var result = await ParcelsCollection.Find(filter).Project(projection).ToListAsync();
            List<ParcelModel> parcels=new List<ParcelModel>();
            foreach (var item in result)
            {
                parcels.Add(item);
            }
            
            if (parcels == null)
                throw new Exception("Parcels is null for return");
            return parcels;
        }

    }
}
