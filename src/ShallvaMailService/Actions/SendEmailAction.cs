using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using ShallvaMailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallvaMailService.Actions
{
    public class SendEmailAction
    {
        public bool Send(List<EmailActivityModel> items)
        {
            bool isSuccess = true;
            if (items?.Count > 0)
            {
                UpdateEmailActivityAction action = new UpdateEmailActivityAction();
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    SmtpClientResourceModel clientResource = new SmtpClientResourceModel();
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(clientResource.EmailFromName, clientResource.EmailFrom));
                    // TODO: change clientResource.* to item.*
                    var toAddressList = clientResource.ToTest.Split(',').Select(x => new MailboxAddress(x));
                    message.To.AddRange(toAddressList);
                    if (!string.IsNullOrEmpty(item.CC))
                    {
                        var toCCAddressList = clientResource.ToTest.Split(',').Select(x => new MailboxAddress(x));
                        message.Cc.AddRange(toCCAddressList);
                    }
                    if (!string.IsNullOrEmpty(item.BCC))
                    {
                        var toBCCAddressList = clientResource.ToTest.Split(',').Select(x => new MailboxAddress(x));
                        message.Bcc.AddRange(toBCCAddressList);
                    }
                    message.Subject = item.Subject;
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    // attach the logo image
                    var image = bodyBuilder.LinkedResources.Add(Consts.LOGO_PATH);
                    image.ContentId = MimeUtils.GenerateMessageId();
                    bodyBuilder.HtmlBody = string.Format(item.Body.Trim(), image.ContentId);

                    message.Body = bodyBuilder.ToMessageBody();

                    try
                    {
                        using (SmtpClient client = new SmtpClient())
                        {
                            client.Connect(clientResource.Host, clientResource.Port);
                            client.AuthenticationMechanisms.Remove("XOAUTH2");
                            client.Authenticate(clientResource.EmailFrom, clientResource.EmailFromPassword);

                            item.TryNumber++;
                            client.Send(message);
                            isSuccess = true;
                            item.SentOn = DateTime.Now;
                            item.Status = (byte)EmailActivityStatus.Sent;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n| Error while sending email: \n Id:{item.Id} \n Message:{ex.Message}\n");
                        isSuccess = false;

                        // try again up to 3 times
                        if (item.TryNumber < 3)
                        {
                            // NOTE: next try will be on the next schedule task
                            Console.WriteLine($"Try number: {item.TryNumber}");
                        }
                        else
                        {
                            item.Status = (byte)EmailActivityStatus.Faild;
                            Console.WriteLine("| Error: Faild to send mail\n");
                        }
                    }

                    action.Run(Consts.CONNECTION_STRING, item);
                }
            }
            return isSuccess;
        }
    }
}
