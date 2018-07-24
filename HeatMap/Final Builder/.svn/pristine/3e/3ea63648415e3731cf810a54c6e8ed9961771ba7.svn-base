namespace SendEmailFinalBuilderCustomAction
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using VSoft.CustomActionApi;

    public class SendEmailCustomAction : StandardAction
    {
        public override bool Execute()
        {
            string address = "buildautomation@syncfusion.com";
            string addresses = base.ExpandProperty("To");
            string str3 = base.ExpandProperty("Subject");
            string path = base.ExpandProperty("Body");
            string content = "";
            string str6 = base.ExpandProperty("AttachmentFile");
            //string fileName = base.ExpandProperty("LogoImage");
            MailMessage message = new MailMessage();
            message.From = new MailAddress(address);
            message.To.Add(addresses);
            if (System.IO.File.Exists(path))
            {
                content = System.IO.File.ReadAllText(path);
            }
            else
            {
                content = path;
            }
            if ((str6 != null) && (str6.Trim() != ""))
            {
                try
                {
                    foreach (string str8 in str6.Split(new char[] { ',' }))
                    {
                        if (System.IO.File.Exists(str8))
                        {
                            Attachment attachment = new Attachment(str8);
                            message.Attachments.Add(attachment);
                        }
                    }
                }
                catch (FileNotFoundException exception)
                {
                    base.SendMessage("Mail Sent Failed : " + exception.StackTrace, MessageType.Error);
                    return false;
                }
            }
            message.Subject = str3;
            AlternateView item = AlternateView.CreateAlternateViewFromString(content, null, "text/html");
            //LinkedResource resource = new LinkedResource(fileName);
            //resource.ContentId = "companylogo";
            //item.LinkedResources.Add(resource);
            message.AlternateViews.Add(item);
            try
            {
                message.IsBodyHtml = true;
                message.Body = content;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(address, "Coolcomp299");
                client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
                client.Host = "smtp.office365.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Send(message);
                base.SendMessage("Mail sent successfully !!!", MessageType.Success);
            }
            catch (Exception exception2)
            {
                base.SendMessage("Mail Sent Failed : " + exception2.StackTrace, MessageType.Error);
                return false;
            }
            return true;
        }

        public override void Validate()
        {
            base.ValidateNonEmptyProperty("To");
            base.ValidateNonEmptyProperty("Subject");
            base.ValidateNonEmptyProperty("Body");
        }
    }
}

