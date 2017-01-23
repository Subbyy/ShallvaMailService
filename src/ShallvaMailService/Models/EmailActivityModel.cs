using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallvaMailService
{
    public class EmailActivityModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime SentOn { get; set; }
        public byte Status { get; set; }
        public int TryNumber { get; set; }
    }
}
