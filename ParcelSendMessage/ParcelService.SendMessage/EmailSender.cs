using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelService.SendMessage
{
    public interface IEmailSender
    {
        Task SendEmailAsync<T>(string email, string subject,T obj);
    }
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync<T>(string email, string subject, T obj)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Some", "apanasevich-90@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            string values=null;
            Type type = obj.GetType();
            int i = 0;
            foreach (var item in type.GetProperties())
            {
                values+=$"{i}.{item.Name}:{item.GetValue(obj)}<br>";
                i++;
            }
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Hello,your parcel was send!<br>{values}"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync("apanasevich-90@mail.ru", "PASSSSSSSSS");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
