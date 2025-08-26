using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Ikaisoft.Models;
namespace Ikaisoft.Services
{
    

    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendContactMail(ContactFormModel model)
        {
            var smtpSettings = _config.GetSection("SmtpSettings");
            var forwardTo = _config["ForwardTo"];
            var brand = _config.GetSection("Brand");

            var smtpClient = new SmtpClient(smtpSettings["Server"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(
                    smtpSettings["Username"],
                    smtpSettings["Password"]
                ),
                EnableSsl = bool.Parse(smtpSettings["EnableSSL"])
            };

            // HTML Email Body
            string htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color: #f4f6f8;
      padding: 20px;
    }}
    .card {{
      background: #ffffff;
      border-radius: 12px;
      box-shadow: 0 4px 12px rgba(0,0,0,0.1);
      max-width: 600px;
      margin: auto;
      padding: 20px;
      border: 1px solid #e0e0e0;
    }}
    .header {{
      text-align: center;
      padding-bottom: 15px;
      border-bottom: 3px solid {brand["PrimaryColor"]};
    }}
    .header img {{
      max-height: 60px;
      margin-bottom: 10px;
    }}
    .header h2 {{
      color: {brand["PrimaryColor"]};
      margin: 0;
    }}
    .content p {{
      margin: 10px 0;
      font-size: 15px;
      color: #333;
    }}
    .label {{
      font-weight: bold;
      color: #555;
    }}
    .footer {{
      text-align: center;
      margin-top: 20px;
      font-size: 13px;
      color: #777;
    }}
  </style>
</head>
<body>
  <div class='card'>
    <div class='header'>
      <img src='{brand["LogoUrl"]}' alt='{brand["CompanyName"]} Logo'>
      <h2>📩 New Contact Request</h2>
    </div>
    <div class='content'>
      <p><span class='label'>Name:</span> {model.Name}</p>
      <p><span class='label'>Email:</span> {model.Email}</p>
      <p><span class='label'>Subject:</span> {model.Subject}</p>
      <p><span class='label'>Message:</span><br>{model.Message}</p>
    </div>
    <div class='footer'>
      This message was sent via the {brand["CompanyName"]} website.
    </div>
  </div>
</body>
</html>";

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["SenderEmail"], smtpSettings["SenderName"]),
                Subject = $"New Contact Request: {model.Subject}",
                Body = htmlBody,
                IsBodyHtml = true
            };

            // Send to Hiox mailbox
            mailMessage.To.Add(smtpSettings["SenderEmail"]);

            // CC Gmail
            mailMessage.CC.Add(forwardTo);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }

}

