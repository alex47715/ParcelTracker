using Microsoft.Extensions.Logging;
using ParcelStatusService.Data.RedisRepository;
using ParcelStatusService.Data.SqlRepository;
using RabbitMQ.Client;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParcelStatusService.Manager
{
    public interface IParcelStatusManager
    {
        Task SetStatus(StatusENUM parcelStatus, int id);
        Task<string> GetStatus(string id);
        Task UpdateStatus(StatusENUM parcelStatus, int id);
        Task RemoveStatus(string id);
        Task StartSending(int id);
    }
    internal class ParcelStatusManager : IParcelStatusManager
    {
        private readonly ILogger<ParcelStatusManager> _logger;
        private readonly IParcelStatusRepositoryRedis _parcelStatusRepositoryRedis;
        private readonly IParcelStatusRepositorySql _parcelStatusRepositorySQL;
        public ParcelStatusManager(ILogger<ParcelStatusManager> logger,
            IParcelStatusRepositoryRedis parcelStatusRepositoryRedis,
            IParcelStatusRepositorySql parcelStatusRepositorySQL)
        {
            _logger = logger;
            _parcelStatusRepositoryRedis = parcelStatusRepositoryRedis;
            _parcelStatusRepositorySQL = parcelStatusRepositorySQL;
        }

        public async Task<string> GetStatus(string id)
        {
            var result = await _parcelStatusRepositoryRedis.GetStatus(id);
            if (result == null)
            {
                result = await _parcelStatusRepositorySQL.GetStatus(id);
                if (result == null)
                    throw new Exception("Cant get status from Redis and SQL");
                return result;
            }
            return result;
        }

        public async Task RemoveStatus(string id)
        {
            await _parcelStatusRepositoryRedis.RemoveStatus(id);
            await _parcelStatusRepositorySQL.RemoveStatus(id);
        }

        public async Task SetStatus(StatusENUM parcelStatus, int id)
        {
            await _parcelStatusRepositoryRedis.SetStatus(parcelStatus, id);
            await _parcelStatusRepositorySQL.SetStatus(parcelStatus, id.ToString());
        }

        public async Task UpdateStatus(StatusENUM parcelStatus, int id)
        {
            await _parcelStatusRepositoryRedis.SetStatus(parcelStatus, id);
            await _parcelStatusRepositorySQL.UpdateStatus(parcelStatus, id.ToString());
        }
        public async Task StartSending(int id)
        {
            var client = new HttpClient();
            string resultJson = await client.GetStringAsync($"http://localhost:13676/api/v1/ParcelInfo/parcels/{id}");
            var factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "messege-queue1",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                var body = Encoding.UTF8.GetBytes(resultJson);

                channel.BasicPublish(exchange: "",
                                     routingKey: "messege-queue1",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
