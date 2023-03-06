using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperMail
    {
        private IConfiguration configuration;
        public HelperMail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private MailMessage ConfigureMailMessage(string para, string asunto, string mensaje)
        {
            MailMessage mailMessage = new MailMessage();
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            mailMessage.From = new MailAddress(user);
            mailMessage.To.Add(new MailAddress(para));
            mailMessage.Subject = asunto;
            mailMessage.Body = mensaje;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            return mailMessage;
        }

        private SmtpClient ConfigureSmtpClient()
        {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string hostName = this.configuration.GetValue<string>("MailSettings:Smtp:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Smtp:Port");
            bool enableSSL = this.configuration.GetValue<bool>("MailSettings:Smtp:EnableSSL");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Smtp:DefaultCredentials");
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = hostName;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSSL;
            smtpClient.UseDefaultCredentials = defaultCredentials;
            NetworkCredential networkCredential = new NetworkCredential(user, password);
            smtpClient.Credentials = networkCredential;
            return smtpClient;
        }

        public async Task SendMailAsync(string para, string asunto, string mensaje)
        {
            MailMessage mailMessage = this.ConfigureMailMessage(para, asunto, mensaje);
            SmtpClient smtpClient = ConfigureSmtpClient();
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendMailAsync(string para, string asunto, string mensaje, string filePath)
        {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            Attachment attachment = new Attachment(filePath);
            mail.Attachments.Add(attachment);
            SmtpClient client = this.ConfigureSmtpClient();
            await client.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string para, string asunto, string mensaje, List<string> filePaths)
        {
            MailMessage mailMessage = this.ConfigureMailMessage(para, asunto, mensaje);
            foreach (var filePath in filePaths)
            {
                Attachment attachment = new Attachment(filePath);
                mailMessage.Attachments.Add(attachment);
            }
            SmtpClient smtpClient = ConfigureSmtpClient();
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
