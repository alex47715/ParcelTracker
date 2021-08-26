using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer.Parcel
{
    public class Parcel
    {
        public string parcelId { get; set; }
        public string senderName { get; set; }
        public string recipientName { get; set; }
        public double weight { get; set; }
    }
}
