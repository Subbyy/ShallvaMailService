using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using ShallvaMailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ShallvaMailService.Actions;

namespace ShallvaMailService
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("-- Start --\n");

            Console.WriteLine("Fetching data...");
            // get active emails
            EmailActiviyCriteriaModel criteria = new EmailActiviyCriteriaModel { Status = EmailActivityStatus.Active };
            GetEmailsByCriteriaAction action = new GetEmailsByCriteriaAction();
            var emails = action.Run(criteria);
            bool result;
            // send emails
            if (emails?.Count > 0)
            {
                Console.WriteLine("Done.\n");
                Console.WriteLine("Start sending emails...");
                SendEmailAction emailAction = new SendEmailAction();
                result = emailAction.Send(emails);
                Console.WriteLine("Finish sending emails...\n");
            }
            else
            {
                Console.WriteLine("No emails found.");
            }

            Console.WriteLine("-- End --");
        }
    }
}
