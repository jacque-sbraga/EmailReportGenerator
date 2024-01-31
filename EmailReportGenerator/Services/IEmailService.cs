using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailReportGenerator.Services
{
    public interface IEmailService
    {
        public string IMAP_HOST { get; set; }
        public int IMAP_PORT { get; set; }
        public string IMAP_USER { get; set; }
        public string IMAP_PASSWORD { get; set; }
        public ImapClient ImapClient { get; set; }

        public Task Connect();
        public List<MimeMessage> GetEmailsBySubjectAndSender(string subject, string sender);
        public void SaveAttachmentFromEmail(MimeMessage email);

    }
}
