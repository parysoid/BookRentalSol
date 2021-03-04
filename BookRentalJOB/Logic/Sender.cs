using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using FluentEmail.Core;
using BookRentalJOB.Models;
using FluentEmail.Razor;

namespace BookRentalJOB.Models
{
    public class Sender
    {        
        public async Task ProcessAsync(List<RecordsModel> data )
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
                        {
                            EnableSsl = false,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Port = 25
                            //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                            //PickupDirectoryLocation = @"C:\Users\Kuba\Desktop\emails"
                        });

            Email.DefaultSender = sender;
            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            foreach (var record in data)
            {
                StringBuilder template = new StringBuilder();
                template.AppendLine("Vážený zákazníku,");
                template.AppendLine("<p>Děkujeme za vrácení knihy <strong>@Model.Title</strong> od autora <strong>@Model.Author</strong> a za využití našich služeb.<p>");
                template.AppendLine("<p>Zde naleznete vyučtování za půjčení:<p>");
                template.AppendLine("<p><strong>Název knihy:</strong> @Model.Title<br>" +
                                       "<strong>Datum zapůjčení:</strong> @Model.RentStart<br>" +
                                       "<strong>Datum vrácení:</strong> @Model.RentEnd<br>" +
                                       "<strong>Celková cena za půjčení:</strong> @Model.Price Kč");
               
                var email = await Email
                .From("objednavky@bookrental.cz")
                .To("yolo.swaggins@example.com")
                .Subject("BookRental a.s. - zakázka č: " + record.RecordNumber)
                .UsingTemplate(template.ToString(), record)                               
                .SendAsync();
            }
        }
    }
}
