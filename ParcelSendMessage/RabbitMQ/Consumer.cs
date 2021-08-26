using MassTransit;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using ParcelService.SendMessage;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer.Parcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class ParcelConsumer : IConsumer<Parcel>
    {
        public async Task Consume(ConsumeContext<Parcel> context)
        {
            var data = context.Message;
            EmailSender emailSender = new EmailSender();
            await emailSender.SendEmailAsync("alex47715@yandex.ru", "SenderNotification", data);
            Console.WriteLine(data.recipientName);
        }
    }
}
