using EmailReportGenerator.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailReportGenerator.Services
{
    internal class EmailService : IEmailService
    {
     
        public string IMAP_HOST { get; set; }
        public int IMAP_PORT { get; set; }
        public string IMAP_USER { get; set; }
        public string IMAP_PASSWORD { get; set; }
        public ImapClient ImapClient { get; set; }

        public EmailService(string iMAP_HOST, int iMAP_PORT,string iMAP_USER, string iMAP_PASSWORD)
        {
            IMAP_HOST = iMAP_HOST;
            IMAP_PORT = iMAP_PORT;
            IMAP_USER = iMAP_USER;
            IMAP_PASSWORD = iMAP_PASSWORD;
            ImapClient = new ImapClient();
        }

        public async Task Connect()
        {
            if(!ImapClient.IsConnected)
                await ImapClient.ConnectAsync(IMAP_HOST, IMAP_PORT, SecureSocketOptions.SslOnConnect);
            Console.WriteLine("Connected");
            if(!ImapClient.IsAuthenticated)
            {
                await ImapClient.AuthenticateAsync(IMAP_USER, IMAP_PASSWORD);
                await ImapClient.Inbox.OpenAsync(FolderAccess.ReadWrite);
       
            }
            
        }
        public List<EmailModel> GetEmailsBySubjectAndSender(string subject, string sender)
        {
            List<EmailModel> messages = new List<EmailModel>();

            var messagesNotRead = ImapClient.Inbox.Search(SearchQuery.SubjectContains(subject).And(SearchQuery.FromContains(sender)));

            foreach (var uuid in messagesNotRead)
            {
                var message = ImapClient.Inbox.GetMessage(uuid);
                SaveAttachmentFromEmail(message.Attachments);

                EmailModel email = new EmailModel {
                    Uuid = uuid.ToString(),
                    Subject = message.Subject,
                    Date = message.Date.DateTime, 
                    From = message.From.ToString(), 
                    To =message.To.ToString()
                };
                messages.Add(email);

                ImapClient.Inbox.AddFlags(uuid, MessageFlags.Seen, true);
            }

            return messages;
        }
       public void SaveAttachmentFromEmail(IEnumerable<MimeEntity> attachments)
        {
            if(attachments != null)
            {
                foreach (MimePart attachment in attachments)
                {
                    if (attachment.FileName.EndsWith(".xlsx"))
                    {
                        Console.WriteLine(attachment.FileName);
                    }
                }
            }
        }
    }
}
