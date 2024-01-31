using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailReportGenerator.Models
{
    public class EmailModel
    {
        public string Uuid {  get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        //public MimeEntity Attachment { get; set; }

        public EmailModel() { }

        public EmailModel(string uuid, string subject, DateTime date, string from, string to)
        {
            Uuid = uuid;
            Subject = subject;
            Date = date;
            From = from;
            To = to;
            //Attachment = attachment;
        }
    }
}
