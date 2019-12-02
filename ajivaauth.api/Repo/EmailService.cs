using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ajivaauth.api.Models;
using ajivaauth.api.Repo.Interfaces;

namespace ajivaauth.api.Repo
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendContactUsEmail(ContactUsEmailDto contactUsEmailDto, SMPTConfig config)
        {
            using (var client = new SmtpClient(config.Server) { Port = config.Port, EnableSsl = true, UseDefaultCredentials = false, 
                Credentials = new NetworkCredential(config.MainEmail, config.MailPass)})
            {
                try 
                {
                    //Setup message
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(config.InboxEmail);
                    mailMessage.To.Add(config.MainEmail);
                    mailMessage.Subject = contactUsEmailDto.Subject;
                    mailMessage.Body = "The following is a message received from your contact us page(Email: " 
                        + contactUsEmailDto.FromAddress + "  Name: " 
                        + contactUsEmailDto.NameOfRecipient + "):  " 
                        + contactUsEmailDto.Message;
                    
                    //Send
                    await client.SendMailAsync(mailMessage);
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<bool> SendRegistrationEmail(ContactUsEmailDto contactUsEmailDto, SMPTConfig config)
        {
            using (var client = new SmtpClient(config.Server){ Port = config.Port, EnableSsl = true, UseDefaultCredentials = false, Credentials = new NetworkCredential(config.MainEmail, config.MailPass)})
            {
                try 
                {
                    var mailAddress = new MailAddress(config.InboxEmail);
                    var mailMessage = new MailMessage()
                    {
                        From = mailAddress,
                        Subject = contactUsEmailDto.Subject,
                        Body = $"The following email address has requested to register for your current research {contactUsEmailDto.FromAddress}"
                    };
                    mailMessage.To.Add(config.MainEmail);
                    client.Send(mailMessage);

                    var senderMailMessage = new MailMessage()
                    {
                        From = mailAddress,
                        Subject = "Your Registration For Research",
                        Body = $"This email is just confirming that you've registered for a research at GreatBlackMoms.org.  You will be contacted as soon as possible at this email address about moving forward if you've been selected. {Environment.NewLine} Thank you for you registering!"
                    };
                    senderMailMessage.To.Add(contactUsEmailDto.FromAddress);
                    await client.SendMailAsync(senderMailMessage);
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}