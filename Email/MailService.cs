using Email.Dtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Threading.Tasks;

namespace Email
{
    /* đầu tiên : Startup
     *  services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IJwtHandler, JwtHandler>();
     * 
     *   appsettings.json
     *   
     *        "MailSettings": {
              "Host": "smtp.gmail.com",
              "Port": "587",
              "DisplayName": "Trần Hải Nam",
              "Mail": "hainam1507x@gmail.com",
              "Password": "**********"
              }
     *  Quyền truy cập của ứng dụng kém an toàn => on
     *  
     *  Sử dụng với DI :
     *  private readonly IMailService _mailService;
        public HomePageController(IMailService mailService)
        {
            _mailService = mailService;
        }

        Thực thi : 
            HelloRequest request = new HelloRequest();
            request.FilePath = @"F:\Program\my-projet\Weeke.vn\Weeke.API\Utils\Email\Templates\base.html";
            request.ToEmail = "wt436.developer@gmail.com";
            request.UserName = "asdadasdsadas";
            await _mailService.SendHelloEmailAsync(request);

    * Nếu như phải config ở nơi không thể DI thì dùng toán tử new : 
    * 
        private IMailService _mailService;
        private MailSettings mailSettings = new MailSettings();
        public HomePageController()
        {
            var configurationBuilder = new ConfigurationBuilder()
                                           .SetBasePath(Directory.GetCurrentDirectory())
                                           .AddJsonFile("appsettings.json")
                                           .Build();
            configurationBuilder.GetSection("MailSettings")
                                .Bind(mailSettings);
            _mailService = new MailService(Options.Create(mailSettings));
        }
     *
     **/
    public class MailService : IMailService
    {
        public readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendFileEmailAsync(FileMailRequest mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendHtmlEmailAsync(HtmlMailRequest request)
        {
            // string FilePath = Directory.GetCurrentDirectory() + "\\"+request.FilePath;
            string filePath = request.HtmlPath;
            StreamReader str = new StreamReader(filePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[Title]", request.Title).Replace("[ToEmail]", request.ToEmail);
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"{request.Title}";
            var builder = new BodyBuilder
            {
                HtmlBody = MailText
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendTextEmailAsync(TextMailRequest request)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"{request.Title}";
            var builder = new BodyBuilder
            {
                HtmlBody = request.TextString
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendHtmlEmailVerifiCatioAsync(HtmlMailRequest request , VerificationCode verificationCode)
        {
            string filePath = request.HtmlPath;
            StreamReader str = new StreamReader(filePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[ImgSrc]", verificationCode.ImgSrc)
                               .Replace("[ImgAlt]", verificationCode.ImgAlt)
                               .Replace("[Greeting]", verificationCode.Greeting)
                               .Replace("[UserName]", verificationCode.UserName)
                               .Replace("[Instruction]", verificationCode.Instruction)
                               .Replace("[InstructionUrl]", verificationCode.InstructionUrl)
                               .Replace("[Url]", verificationCode.Url)
                               .Replace("[Click]", verificationCode.Click)
                               .Replace("[VerificationCodesText]", verificationCode.VerificationCodesText)
                               .Replace("[VerificationCodes]", verificationCode.VerificationCodes);
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"{request.Title}";
            var builder = new BodyBuilder
            {
                HtmlBody = MailText
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
