using ShallvaMailService.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallvaMailService.Models
{
    public class SmtpClientResourceModel
    {
        private int port;
        private bool enableSsl;

        public string ToTest
        {
            get
            {
                return Smtp.ResourceManager.GetString("ToTest");
            }
        }

        public string EmailFrom
        {
            get
            {
                return Smtp.ResourceManager.GetString("EmailFrom");
            }
        }
        public string EmailFromName
        {
            get
            {
                return Smtp.ResourceManager.GetString("EmailFromName");
            }
        }
        public string EmailFromPassword
        {
            get
            {
                return Smtp.ResourceManager.GetString("EmailFromPassword");
            }
        }
        public string Host
        {
            get
            {
                return Smtp.ResourceManager.GetString("Host");
            }
        }
        public int Port
        {
            get
            {
                int.TryParse(Smtp.ResourceManager.GetString("Port"), out port);
                return port;
            }
        }
        public bool EnableSsl
        {
            get
            {
                bool.TryParse(Smtp.ResourceManager.GetString("EnableSsl"), out enableSsl);
                return enableSsl;
            }
        }

    }
}
