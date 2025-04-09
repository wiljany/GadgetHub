using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Concrete
{
	public class EmailSettings
	{
		public string MailToAddress = "orders@example.com";
		public string MailFromAddress = "orders@GadgetHub.com";
		public bool UseSsl = true;
		public string Username = "MySmtpUsername";
		public string Password = "MySmtpPassword";
		public string ServerName = "smtp.example.com";
		public int ServerPort = 587;
		public bool WriteAsFile = true;
		public string FileLocation = @"C:\gadgethub_emails";
	}
	public class EmailOrderProcessor : IOrderProcessor
	{
		private EmailSettings emailSettings;

		public EmailOrderProcessor(EmailSettings settings)
		{
			emailSettings = settings;
		}

		public void processOrder(Cart cart, ShippingDetails shippingInfo)
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
					.AppendLine("A new order has been submitted.")
					.AppendLine("---------------------")
					.Append("Items");

				foreach (var line in cart.Lines)
				{
					var subtotal = line.Gadgets.Price * line.Quantity;
					body.AppendFormat("{0} X {1} (subtotal: {2:c}", line.Quantity, line.Gadgets.Name, subtotal);
				}

				body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
					.AppendLine("-------------------")
					.AppendLine("Ship to:")
					.AppendLine(shippingInfo.Name)
					.AppendLine(shippingInfo.Address)
					.AppendLine(shippingInfo.SecondaryAddress ?? "")
					.AppendLine(shippingInfo.ThirdAddress ?? "")
					.AppendLine(shippingInfo.City)
					.AppendLine(shippingInfo.State)
					.AppendLine(shippingInfo.Country)
					.AppendLine(shippingInfo.Zip)
					.AppendLine("-------------------")
					.AppendFormat("Gift wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");

				MailMessage mailMessage = new MailMessage(
					emailSettings.MailFromAddress,
					emailSettings.MailToAddress,
					"New Order Submitted!",
					body.ToString());

				if (emailSettings.WriteAsFile)
				{
					mailMessage.BodyEncoding = Encoding.ASCII;
				}
				smtpClient.Send(mailMessage);
			}
		}
	}
}
