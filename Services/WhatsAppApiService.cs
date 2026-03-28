using mahadalzahrawebapi.Models;
using Newtonsoft.Json;
using System.Net;

namespace mahadalzahrawebapi.Services
{
    public class StarResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<dynamic> result { get; set; }
    }
    public class WhatsAppApiService
    {
        private string msgUrl = "https://graph.facebook.com/v14.0/106854035534702/messages";

        private readonly mzdbContext _context;

        public WhatsAppApiService(mzdbContext context)
        {
            _context = context;
        }

        //public string sendPayslip(SalaryAllocation sa)
        //{

        //    var httpRequest = (HttpWebRequest)WebRequest.Create(msgUrl);
        //    httpRequest.Method = "POST";

        //    httpRequest.Headers["Authorization"] = authKey;
        //    httpRequest.ContentType = "application/json";

        //    string data = "";
        //    var result = "";

        //    using (var context = new mzmanageEntities())
        //    {
        //        int hMonth = sa.month;
        //        hijri_months months = _context.hijri_months.Where(x => x.id == hMonth).FirstOrDefault();
        //        string monthName = sa.isHijri ? months.hijriMonthName : new DateTime(2022, hMonth, 01).ToString("MMMM");

        //        khidmat_guzaar kh = _context.khidmat_guzaar.Where(x => x.itsId == sa.itsId).FirstOrDefault();
        //        if (kh == null)
        //        {
        //            return "Khidmat guzaar not found";
        //        }
        //        else
        //        {
        //            if (kh.c_codeWhatsapp == null || kh.whatsappNo == null)
        //            {
        //                return "Khidmat guzaar detial empty";
        //            }
        //        }
        //        //string num = kh.c_codeWhatsapp + kh.whatsappNo;
        //        string num = "919082062503";
        //        data = @"{ 
        //                    ""messaging_product"": ""whatsapp"",
        //                    ""to"": """ + num + @""",
        //                    ""type"": ""template"",
        //                    ""template"": {
        //                        ""name"": ""pay_slip"",
        //                        ""language"": { 
        //                            ""code"": ""en"" 
        //                        },
        //                        ""components"":[{
        //                            ""type"":""header"",
        //                            ""parameters"":[{
        //                                ""type"":""document"",
        //                                ""document"":{
        //                                    ""link"":""https://www.mahadalzahra.org/uploads/ReportPdf/Payslips/" + kh.itsId + @".pdf"",
        //                                    ""filename"":""Payslip_" + monthName + "_" + sa.year + @".pdf""
        //                                }
        //                            }]
        //                        },{
        //                            ""type"":""body"",
        //                            ""parameters"":[{
        //                                ""type"":""text"",
        //                                ""text"":""" + kh.fullName + @"""
        //                            },{
        //                                ""type"":""text"",
        //                                ""text"":""" + monthName + "_" + sa.year + @"""
        //                            }]
        //                        }]
        //                    }
        //                }";
        //        string str = "\r\n";
        //        data = data.Replace(str, "");
        //        data = data.Replace("  ", "");
        //        whatsapp_msg_log wlog = new whatsapp_msg_log()
        //        {
        //            msgType = "payslip",
        //            sentby = "System",
        //            sentvia = "sendPayslip (" + monthName + ")",
        //            timestamp = DateTime.Now,
        //            status = "Initaited",
        //            sentto = kh.fullName + " (" + kh.itsId + ")"
        //        };
        //        try
        //        {
        //            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        //            {
        //                streamWriter.Write(data);
        //            }
        //            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                result += streamReader.ReadToEnd();
        //            }
        //            //wlog.status = "Message Sent";
        //            //_context.whatsapp_msg_log.Add(wlog);
        //            _context.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            wlog.status = "Error Ocured";
        //            //_context.whatsapp_msg_log.Add(wlog);
        //            _context.SaveChanges();
        //            result += data + e.ToString();
        //        }


        //    }

        //    return result;
        //}

        //public string sendStarMarketingWhatsappPayslip(SalaryAllocation sa)
        //{

        //    var url = "http://www.wpadmin.star52app.com/api-panel/api/message.php";
        //    var httpRequest = WebRequest.CreateHttp(url);
        //    httpRequest.Method = "POST";
        //    httpRequest.Timeout = 60000;
        //    httpRequest.ContentType = "application/json; charset=utf-8";


        //    using (var context = new mzmanageEntities())
        //    {
        //        int hMonth = sa.month;
        //        hijri_months months = _context.hijri_months.Where(x => x.id == hMonth).FirstOrDefault();
        //        string monthName = sa.isHijri ? months.hijriMonthName : new DateTime(2022, hMonth, 01).ToString("MMMM");
        //        string errorState = "Step";
        //        khidmat_guzaar kh = _context.khidmat_guzaar.Where(x => x.itsId == sa.itsId).FirstOrDefault();

        //        if (kh == null)
        //        {
        //            return "Khidmat guzaar not found";
        //        }
        //        else
        //        {
        //            if (kh.c_codeWhatsapp == null || kh.whatsappNo == null)
        //            {
        //                return "Khidmat guzaar detial empty";
        //            }
        //        }
        //        string message = "Salam jameel,\n*" + kh.fullName + "*\n\nYour Payslip for month of *" + monthName + "_" + sa.year + "* is attached herewith.\n\nRequest to download and save it at your convenience as you may not be able to access it next month.\n\nShukran,\nWassalam ";
        //        //string message = "Salam jameel,\n is attached herewith.\n\nRequest to download and save it at your convenience as you may not be able to access it next month.\n\nShukran,\nWassalam ";
        //        //string num = kh.c_codeWhatsapp + kh.whatsappNo;
        //        string fileName = "Payslip_" + monthName + "_" + sa.year + ".pdf";
        //        List<string> num = new List<string>();
        //        num.Add(kh.c_codeWhatsapp + kh.whatsappNo);
        //        var data = new
        //        {
        //            token = "649e830bd0ce2",
        //            numbers = num,
        //            message = message,
        //            mediatype = "url",
        //            filename = fileName,
        //            media = "https://www.mahadalzahra.org/uploads/ReportPdf/Payslips/" + kh.itsId + ".pdf"
        //        };
        //        try
        //        {

        //            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        //            {
        //                streamWriter.Write(JsonConvert.SerializeObject(data));
        //                streamWriter.Flush();
        //            }

        //            errorState = "step - 1";

        //            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

        //            errorState = "step - 2";

        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                var result = streamReader.ReadToEnd();
        //                if (result != "")
        //                {
        //                    StarResponse resp = JsonConvert.DeserializeObject<StarResponse>(result);

        //                    errorState += " a " + resp.message;
        //                    return errorState;
        //                }
        //                else
        //                {
        //                    return "Error";
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            return errorState + e.ToString();
        //        }
        //    }
        //}

        public string sendGeneralMessage(int its, string msg, string methodName, string msgType = "System", string sentBy = "System")
        {

            var httpRequest = (HttpWebRequest)WebRequest.Create(msgUrl);
            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = "";
            httpRequest.ContentType = "application/json";

            DateTime t = DateTime.Now;

            //whatsapp_msg_log wlog = new whatsapp_msg_log()
            //{
            //    msgType = msgType,
            //    sentby = sentBy,
            //    sentvia = methodName,
            //    timestamp = t
            //};

            string data = "";
            var result = "";

            khidmat_guzaar kh = _context.khidmat_guzaar.Where(x => x.itsId == its).FirstOrDefault();
            if (kh == null)
            {
                throw new Exception("Khidmat guzaar not found");
            }
            else
            {
                //wlog.sentto = kh.fullName + " (" + kh.itsId + ")";
                //wlog.status = "Initiated";
                if (kh.c_codeWhatsapp == null || kh.whatsappNo == null)
                {
                    throw new Exception("Khidmat guzaar detail empty");
                }
            }
            string num = kh.c_codeWhatsapp + kh.whatsappNo;
            //string num = "919082062503";
            data = @"{ 
                            ""messaging_product"": ""whatsapp"",
                            ""to"": """ + num + @""",
                            ""type"": ""template"",
                            ""template"": {
                                ""name"": ""general"",
                                ""language"": { 
                                    ""code"": ""en"" 
                                },
                                ""components"":[{
                                    ""type"":""body"",
                                    ""parameters"":[{
                                        ""type"":""text"",
                                        ""text"":""" + msg + @"""
                                    }]
                                }]
                            }
                        }";
            string str = "\r\n";
            data = data.Replace(str, " ");
            try
            {
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result += streamReader.ReadToEnd();
                }
                //wlog.status = "Message Sent";
                //_context.whatsapp_msg_log.Add(wlog);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                //wlog.status = "Error Ocured";
                //_context.whatsapp_msg_log.Add(wlog);
                _context.SaveChanges();
                throw new Exception(result += data + e.Message);
            }

            return result;
        }

        public string sendGeneralMessageToSpecific(string whatsappNum, string msg, string methodName, string msgType = "System", string sentBy = "System")
        {

            var httpRequest = (HttpWebRequest)WebRequest.Create(msgUrl);
            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = "";
            httpRequest.ContentType = "application/json";

            DateTime t = DateTime.Now;
            //whatsapp_msg_log wlog = new whatsapp_msg_log()
            //{
            //    msgType = msgType,
            //    sentby = sentBy,
            //    sentvia = methodName,
            //    timestamp = t
            //};

            string data = "";
            var result = "";

            string num = whatsappNum;
            //string num = "919082062503";
            //data = data = @"{ ""messaging_product"": ""whatsapp"",
            //            ""to"": """ + num + @""",
            //            ""type"": ""template"",
            //            ""template"": {
            //                ""name"": ""general"",
            //                ""language"": { 
            //                    ""code"": ""en"" 
            //                },
            //                ""components"":[{
            //                    ""type"":""body"",
            //                    ""parameters"":[{
            //                        ""type"":""text"",
            //                        ""text"":""" + msg + @"""
            //                    }]
            //                }]
            //            }
            //        }";
            //data = "{ \"messaging_product\": \"whatsapp\",\"to\": \"" + num + "\",\"type\": \"template\",\"template\": {\"name\": \"general\",\"language\": {\"code\": \"en\" },\"components\":[{\"type\":\"body\",\"parameters\":[{\"type\":\"text\",\"text\":\"" + msg + "\"}]}]}}";
            data = "{ \"messaging_product\": \"whatsapp\", \"to\": \"919033221851\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }";
            string str = "\r\n";
            data = data.Replace(str, "");
            data = data.Replace("  ", "");
            try
            {
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result += streamReader.ReadToEnd();
                }
                //wlog.status = "Message Sent";
                //_context.whatsapp_msg_log.Add(wlog);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                //wlog.status = "Error Ocured";
                //_context.whatsapp_msg_log.Add(wlog);
                _context.SaveChanges();
                throw new Exception(result += data + e.Message);
            }

            return result;

        }

        //public string sendStarMarketingGeneralWhatsapp(List<long> whatsappNum, string msg, string methodName, string msgType = "System", string sentBy = "System")
        //{
        //    List<string> whatsappNum2 = new List<string>();
        //    whatsappNum.ForEach(x =>
        //    {
        //        if (!string.IsNullOrEmpty(x) && long.TryParse(x, out long num))
        //        {
        //            whatsappNum2.Add(num);
        //        }
        //    });
        //    return sendStarMarketingGeneralWhatsapp(whatsappNum2, msg, methodName, msgType, sentBy);
        //}


        public string sendStarMarketingGeneralWhatsapp(List<string> whatsappNum, string msg, string methodName, string msgType = "System", string sentBy = "System")
        {

            var url = "http://www.wpadmin.star52app.com/api-panel/api/message.php";
            var httpRequest = WebRequest.CreateHttp(url);
            httpRequest.Method = "POST";
            httpRequest.Timeout = 60000;
            httpRequest.ContentType = "application/json; charset=utf-8";

            //httpRequest.Headers.Add("api-token", "649e830bd0ce2");

            string errorState = "step";
            string str = "\r\n";
            msg = msg.Replace(str, "");
            msg = msg.Replace("  ", "");

            List<string> whatsappNum2 = new List<string>();
            whatsappNum.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x))
                {
                    whatsappNum2.Add(x);
                }
            });

            var data = new
            {
                token = "649e830bd0ce2",
                message = msg,
                numbers = whatsappNum2,
                //numbers = new List<string> { "919082062503" },
                speed = "s"
            };

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(data));

                streamWriter.Flush();
            }

            errorState = "step - 1";

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            errorState = "step - 2";

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var length = result.Length;
                if (result != "")
                {
                    StarResponse resp = JsonConvert.DeserializeObject<StarResponse>(result);
                    if (resp != null && resp.status == 1)
                    {
                        errorState += " a " + resp.message;
                    }
                    else if (resp != null && (resp.message == "Logged Out" || resp.message == "Invalid token"))
                    {
                        url = "http://www.wpadmin.star52app.com/api-panel/api/delete.php?token=004e5594-fb9c-4986-bad2-aa79958c7214";
                        httpRequest = WebRequest.CreateHttp(url);
                        httpRequest.Method = "GET";
                        httpRequest.Timeout = 60000;
                        httpRequest.ContentType = "application/json; charset=utf-8";
                        httpRequest.Headers.Add("api-token", "649e830bd0ce2");


                        httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        errorState += " b " + resp.message;

                    }
                    else
                    {
                        errorState += " c " + resp.message;
                    }
                    return errorState;
                }
                else
                {
                    return "Error";
                }
            }
        }
    }
}
