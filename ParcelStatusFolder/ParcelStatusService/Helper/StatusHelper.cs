using Microsoft.Extensions.Logging;
using ParcelStatusService.Data.RedisRepository;
using System;
using System.Threading.Tasks;

namespace ParcelStatusService.Helper
{
    public interface IStatusHelper
    {
        Task<bool> CheckStatus(int id, StatusENUM statusENUM);
    }
    public class StatusHelper : IStatusHelper
    {
        private readonly ILogger<StatusHelper> _logger;
        private readonly IParcelStatusRepositoryRedis _parcelStatusRepositoryRedis;
        public StatusHelper(ILogger<StatusHelper> logger, IParcelStatusRepositoryRedis parcelStatusRepositoryRedis)
        {
            _logger = logger;
            _parcelStatusRepositoryRedis = parcelStatusRepositoryRedis;
        }
        public async Task<bool> CheckStatus(int id,StatusENUM statusENUM)
        {
            var status = await _parcelStatusRepositoryRedis.GetStatus(id.ToString());

            switch (statusENUM)
            {
                case StatusENUM.WaitingSender:
                    if (status.Equals(StatusENUM.InRecipientOffice.ToString()) ||
                        status.Equals(StatusENUM.InSenderOffice.ToString()) ||
                        status.Equals(StatusENUM.OnTheWay.ToString()) ||
                        status.Equals(StatusENUM.ReadyToShip.ToString()) ||
                        status.Equals(StatusENUM.Received.ToString()) ||
                        status.Equals(StatusENUM.ReceivedBySender.ToString()) ||
                        status.Equals(StatusENUM.WaitingSender.ToString()))
                        return false;
                    else
                        return true;
                case StatusENUM.InSenderOffice:
                    if (status.Equals(StatusENUM.WaitingSender.ToString()) ||
                        status.Equals(StatusENUM.ReadyToShip.ToString()))
                        return true;
                    else
                        return false;
                case StatusENUM.ReadyToShip:
                    if (status.Equals(StatusENUM.InSenderOffice.ToString()) ||
                        status.Equals(StatusENUM.Received.ToString()))
                        return true;
                    else
                        return false;
                case StatusENUM.OnTheWay:
                    if (status.Equals(StatusENUM.ReadyToShip.ToString()))
                        return true;
                    else
                        return false;
                case StatusENUM.InRecipientOffice:
                    if (status.Equals(StatusENUM.OnTheWay.ToString()))
                        return true;
                    else
                        return false;
                case StatusENUM.Received:
                    if (status.Equals(StatusENUM.InRecipientOffice.ToString()))
                        return true;
                    else
                        return false;
                case StatusENUM.ReceivedBySender:
                    if (status.Equals(StatusENUM.InSenderOffice.ToString()))
                        return true;
                    else
                        return false;
                default:
                    throw new Exception("Default start in switch");
            }
        }
       
    }
}
