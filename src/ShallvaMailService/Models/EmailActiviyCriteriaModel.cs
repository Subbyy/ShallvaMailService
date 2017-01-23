using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallvaMailService.Models
{
    public class EmailActiviyCriteriaModel
    {
        public EmailActivityStatus? Status { get; set; }
        public bool? LostEmails { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Size { get; set; }
    }
}
