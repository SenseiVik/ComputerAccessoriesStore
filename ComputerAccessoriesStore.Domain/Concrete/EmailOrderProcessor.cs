using ComputerAccessoriesStore.Domain.Abstract;
using System.Text;
using ComputerAccessoriesStore.Domain.Entities;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace ComputerAccessoriesStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }
        //232 str
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;

                    body.AppendLine($"{line.Quantity} x {line.Product.Name} (subtotal: {subtotal.ToString("c")}");
                }

                //else some info


                MailMessage msg = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "New order submitted",
                    body.ToString()
                );

                if (emailSettings.WriteAsFile)
                {
                    msg.BodyEncoding = Encoding.ASCII;
                    File.WriteAllText(emailSettings.FileLocation, msg.ToString());
                }
                else
                {
                    smtpClient.Send(msg);
                }
            }


        }
    }
}
