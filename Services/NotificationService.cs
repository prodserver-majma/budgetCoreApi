using System.Net.Mail;



namespace mahadalzahrawebapi.Services
{
    public interface INotificationService
    {
        void SendAlert(Exception ex);

        bool SendEmail(string subject, string body, List<string> tos, string attachment = null);
        bool SendStandardHTMLEmail(string subject, string body, string tos, string from, string attachment = null);
        bool SendSingleEmail(string subject, string body, string to, string attachment = null);
        bool SendSingleEmailFrom(string subject, string body, string from, string to, string attachment = null);
        bool SendSingleEmailFromWithCC(string subject, string body, string from, string to, string cc, string attachment = null);
        bool SendSingleInfoEmail(string subject, string body, string to, string attachment = null);
        bool SendInfoEmail(string subject, string body, List<string> tos, string attachment = null);

        bool SendEmail_Bulk(string subject, string body, List<string> tos, string attachment = null);
        bool SendSingleEmail_Bulk(string subject, string body, List<string> tos, string attachment = null);
        bool SendSingleEmailFrom_Bulk(string subject, string body, string from, List<string> tos, string attachment = null);
        bool SendSingleEmailFromWithCC_Bulk(string subject, string body, string from, List<string> tos, string cc, string attachment = null);
        bool SendSingleInfoEmail_Bulk(string subject, string body, List<string> tos, string attachment = null);
        bool SendInfoEmail_Bulk(string subject, string body, List<string> tos, string attachment = null);

    }

    public class NotificationService
    {
        private readonly int SMTP_PORT = 587;
        private readonly String SMTP_SERVER = "email-smtp.ap-south-1.amazonaws.com";
        private readonly String SMTP_USER = "AKIAQC4CXA4U7C7LRGN3";
        private readonly String SMTP_PASSWORD = "BK4bd/zMzPOLMwaN0Sx7X3o2h91iWS1gJ5Hgpnx1WdwM";
        private readonly String FROM_SURAT_INFO = "info.surat@mahadalzahra.com";
        private readonly String FROM_SURAT_ACCOUNTS = "accounts.surat@mahadalzahra.com";
        private readonly String FROM_NOREPLY = "noreply@mahadalzahra.com";
        private readonly String FROM_NOREPLY_ATTENDENCE = "noreply.attendance@mahadalzahra.com";
        private readonly String FROM_TRAINING = "training@mahadalzahra.com";
        private readonly List<String> TO_DEVELOPER_ALERT = new List<string> { "mzdeveloper53@gmail.com", "hatimn219@gmail.com" };
        private readonly String FROM_LEAVE = "noreply.leave@mahadalzahra.com";
        private readonly String FROM_ERROR = "noreply.errors@mahadalzahra.com";

        //public void SendAlert(Exception ex)
        //{
        //    string subject = "Exception: " + ex.Message;

        //    string body = string.Empty;
        //    if (Http_context.Current != null)
        //    {
        //        body += "Identity: " + Http_context.Request.Query["Name"];
        //        body += Environment.NewLine + Environment.NewLine;
        //    }
        //    //body += ex.StackTrace();

        //    SendEmail(subject, body, TO_DEVELOPER_ALERT);
        //}

        public bool SendEmail(string subject, string body, List<string> tos, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FROM_SURAT_INFO);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;


            foreach (var i in tos)
            {
                message.To.Add(i);
            }

            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new System.Net.Mail.Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, tos={2}", subject, body, tos.GroupJoin(","));
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, tos, ex.Message);
                return false;
            }
        }

        public bool SendEmail(string subject, string body, string to, string from, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(to);

            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new System.Net.Mail.Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, tos={2}", subject, body, tos.GroupJoin(","));
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, tos, ex.Message);
                return false;
            }
        }

        public bool SendStandardHTMLEmail(string subject, string body, string tos, string from, string attachment = null, string cc = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            switch (from)
            {
                case "info":
                    message.From = new MailAddress(FROM_SURAT_INFO);
                    break;
                case "accounts":
                    message.From = new MailAddress(FROM_SURAT_ACCOUNTS);
                    break;
                case "training":
                    message.From = new MailAddress(FROM_TRAINING);
                    break;
                case "no-reply":
                    message.From = new MailAddress(FROM_NOREPLY);
                    break;
                case "attendance":
                    message.From = new MailAddress(FROM_NOREPLY_ATTENDENCE);
                    break;
                case "leave":
                    message.From = new MailAddress(FROM_LEAVE);
                    break;
                case "error":
                    message.From = new MailAddress(FROM_ERROR);
                    break;
                default:
                    message.From = new MailAddress(from);
                    break;

            }
            message.Subject = subject;
            message.IsBodyHtml = true;
            //message.Body = body;
            message.Body = @"<html>
                <head>
                    <meta name = ""viewport"" content = ""width=device-width, initial-scale=1; charset=utf-8"" />
   
                    <style>
                        body {
                                font - family: Georgia;
                            }
                    </style>
                </head>

                <body>
                        <div style=""max-width: 600px; margin: 20px auto; padding: 20px; background-color: #f8f8f8; border: 1px solid #ddd; border-radius: 10px;"">
 
                         <div
                            style = ""min-height: 60px;border-top: 1px solid #334d4d;padding: 5px 0px;text-align: center;color:firebrick;background-color: #1576a8;"">
                            <a href = ""#"">
 
                                 <div>
 
                                     <img style=""float:left; margin - left:30px; "" src=""https://mahadalzahra.org/img/MahadEnglishLogo.png"" width=""200"" height=""95""
                                        alt=""Mz-Management"" />
                                    <img src = ""https://mahadalzahra.org/img/QismLogoMain.png"" width=""80"" height=""80"" alt=""Mz-Management""
                                        style=""text-align:center;  margin-left:30px;"" />
                                    <!--<img style=""float:right; margin - left:30px;"" class=""arabicLogo"" src=""https://mahadalzahra.org/img/MahadArabicLogo.png""
                                        width=""200"" height=""95"" alt=""Mz-Management"" />-->
                                    <!--<span style = ""font-family:Brush Script MT;font-size:20pt;color: firebrick;float:right;margin-right:30px;padding-top:10px;""> Mahad al zahra</span>-->
                                </div>
                            </a>
                        </div>
                        <div class=""row flex mt-3 mb-3 ml-3"">
                            <div class=""col-lg-12 col-sm-12"">" + body + @"
                            </div>
                        </div>
                        <div
                            style = ""min-height: 20px;border-bottom: 1px solid #334d4d;padding: 10px 0px;text-align: center;color:firebrick;background-color: #1576a8;"">
                            <div><small></ small></ div>
                        </ div>
                    </ div>
                </ body>

                </ html>";
            string str = "\r\n";
            //message.Body = message.Body.Replace(str, " ");


            message.To.Add(tos);
            if (cc != null)
            {
                message.CC.Add(cc);
            }


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new System.Net.Mail.Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, tos={2}", subject, body, tos);
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, tos, ex.Message);
                return false;
            }
        }


        public bool SendEmail_Bulk(string subject, string body, List<string> tos, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FROM_SURAT_INFO);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            foreach (var i in tos)
            {
                message.Bcc.Add(new MailAddress(i));
            }


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, tos={2}", subject, body, tos.Join(","));
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, tos, ex.Message);
                return false;
            }
        }

        public bool SendInfoEmail(string subject, string body, List<string> tos, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FROM_SURAT_INFO);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(string.Join(",", tos));


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, tos={2}", subject, body, string.Join(",", tos));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendInfoEmail_Bulk(string subject, string body, List<string> tos, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FROM_SURAT_INFO);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            foreach (var i in tos)
            {
                message.Bcc.Add(new MailAddress(i));
            }


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, tos={2}", subject, body, tos.Join(","));
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, tos, ex.Message);
                return false;
            }
        }

        public bool SendSingleEmail(string subject, string body, string to, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FROM_NOREPLY);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(FROM_SURAT_INFO);
            message.To.Add(new MailAddress(to));

            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, to={2}", subject, body, to);
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, to, ex.Message);
                return false;
            }
        }

        public bool SendSingleEmailFrom(string subject, string body, string from, string to, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(new MailAddress(to));

            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, to={2}", subject, body, to);
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, to, ex.Message);
                return false;
            }
        }

        public bool SendSingleEmailFromWithCC(string subject, string body, string from, string to, string cc, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(new MailAddress(to));
            if (cc != null)

            {
                message.CC.Add(new MailAddress(cc));
            }



            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, to={2}", subject, body, to);
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, to, ex.Message);
                return false;
            }
        }

        public bool SendSingleEmailFromWithCC_Bulk(string subject, string body, string from, List<string> tos, string cc, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            foreach (var i in tos)
            {
                message.Bcc.Add(new MailAddress(i));
            }


            if (!cc.Equals("") || cc != null)
            {
                message.CC.Add(new MailAddress(cc));
            }



            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                return false;
            }
        }

        public bool SendSingleEmailFrom_Bulk(string subject, string body, string from, List<string> tos, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            foreach (var i in tos)
            {
                message.Bcc.Add(new MailAddress(i));
            }


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                return false;
            }
        }

        public bool SendSingleEmail_Bulk(string subject, string body, List<string> tos, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FROM_NOREPLY);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            foreach (var i in tos)
            {
                message.Bcc.Add(new MailAddress(i));
            }


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                return false;
            }
        }

        public bool SendSingleInfoEmail(string subject, string body, string to, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();


            message.From = new MailAddress(FROM_SURAT_INFO);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(new MailAddress(to));


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {

                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, to={2}", subject, body, to);
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, to, ex.Message);
                throw new Exception(ex.Message);

                //return false;
            }
        }

        public bool SendSingleInfoEmail_Bulk(string subject, string body, List<string> tos, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FROM_SURAT_INFO);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            foreach (var i in tos)
            {
                message.Bcc.Add(new MailAddress(i));
            }


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                return false;
            }
        }

        public bool SendSingleEmailFrom_Test(string subject, string body, string from, string to, string attachment = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = SMTP_PORT;
            client.Host = SMTP_SERVER;
            client.EnableSsl = true;
            client.Timeout = 1000 * 10; //10sec
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(new MailAddress(to));


            if (!String.IsNullOrWhiteSpace(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            try
            {
                client.Send(message);
                message.Dispose();
                //log.Debug("Email sent successfully");
                //log.InfoFormat("Email sent: subject={0}, body={1}, to={2}", subject, body, to);
                return true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //log.Debug("Email not sent : reason - " + ex.Message);
                //log.InfoFormat("Email not sent: subject={0}, body={1}, tos={2}, errorMessage={3}", subject, body, to, ex.Message);
                return false;
            }
        }

    }

}
