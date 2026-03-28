using mahadalzahrawebapi.Models;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.ServiceModel;

namespace mahadalzahrawebapi.Services
{
    public class HifzData
    {
        public int ItsId { get; set; }
        public string Surah { get; set; }
        public string Aayah { get; set; }
        public string Page { get; set; }
        public string Juz { get; set; }
        public string Sanah { get; set; }
        public DateTime Last_Entry_Date { get; set; }
        public string Grade { get; set; }
        public string Sanad { get; set; }
        public string Ikht_Sanah { get; set; }

    }

    public class elq_students
    {
        public string ejid { get; set; }
        public string branch { get; set; }
        public int stage_id { get; set; }
    }

    public class MahadIncome
    {
        public string Mode { get; set; }
        public string Currency_INR { get; set; }
        public string Currency_PKR { get; set; }
        public string Currency_USD { get; set; }
        public string CollectionCenter { get; set; }
        public string Branch { get; set; }
        public string Remark { get; set; }
        public string Currency { get; set; }
        public int amount { get; set; }

    }
    public class MahadIncome_Export
    {

        public string Amount { get; set; }

        public string Branch { get; set; }
        public string date { get; set; }

    }
    public class MahadDues
    {
        public int ItsId { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string Branch { get; set; }
        public string Remark { get; set; }
    }
    public class EQUser
    {
        public int ItsId { get; set; }
        public string ClassType { get; set; }
        public string ClassDays { get; set; }
        public string ClassTime { get; set; }
        public string primaryEmail { get; set; }
        public string secondaryEmail { get; set; }
        public string chatId { get; set; }
        public string mobile { get; set; }
        public string workPhone { get; set; }
        public string residencePhone { get; set; }
        public string eqId { get; set; }
    }
    public interface IElearningService
    {
        EQUser GetElearningId(int itsId);
        HifzData GetCurrentHifzStatus(int itsId);
        List<MahadIncome> GetElearning_Income_InDateRange_BranchWise(string fDate, string tDate, int branchId, string branchName);
        List<MahadIncome> GetElearning_Income_InDateRange_2_BranchWise(string fDate, string tDate, int branchId, string branchName);
        List<MahadIncome> GetElearning_Ewallet_Refill_InDateRange(string fDate, string tDate);
        List<MahadDues> GetElearning_Dues_InDateRange_BranchWise(int branchId, string branchName);
        List<MahadIncome> GetElearning_Ewallet_Used_InDateRange_BranchWise(string fDate, string tDate, int branchId, string branchName);
        MahadDues GetStudent_Dues(int its);
        string GetBranchId(int itsid);
        List<mz_student> GetStudents_FromGroup(int itsid);

        List<mz_student> Test_GetStudents_fromGroup(int itsId);
    }
    internal class ElearningService : IElearningService
    {
        // log4net.ILog log = // log4net.LogManager.GetLogger(typeof(ElearningService));

        private readonly string ELEARNING_SERVICE_KEY = "hifz#5253#_Camps@78652110";
        private readonly string ELEARNING_SERVICE_SECRET = "ErH9E8Jd0MWn8DKxSM2nvYAlDYv3DRGX";
        private readonly string ELEARNING_SERVICE_CLIENT = "MZ";

        private readonly string ELEARNING_INCOME_SERVICE_KEY = "Receipts#786110#";
        private readonly string ELEARNING_STUDENT_SERVICE_KEY = "mz_camp_4652@student";
        private readonly string ELEARNING_GROUP_SERVICE_KEY = "mz_camp_4652@group";

        private readonly string ELEARNING_DUES_SERVICE_KEY = "DUES#786110#";
        private readonly string ELEARNING_STUDENT_DUES_SERVICE_KEY = "DUES#43201322#";
        private readonly string ELEARNING_EWALLET_REFILL_KEY = "ewallet_refill#786110#";
        private readonly string ELEARNING_EWALLET_USED_KEY = "ewallet_used#786110#";
        private readonly string OFFICE_BEARER_ITSID = "20433287";


        readonly BasicHttpBinding binding = new BasicHttpBinding();
        readonly EndpointAddress endpoint = new EndpointAddress("http://ejas.its52.com/ejamaatservices.asmx");

        public EQUser GetElearningId(int itsId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                //ElearningWS.Mahadws ws = new ElearningWS.Mahadws();

                //ds = ws.GetEQID(itsId, "hifz#5253#_Camps@78652110");

            }
            catch (Exception ex)
            {
                // log.Error("GetElearningId Failed: itsId=" + itsId, ex);
                return null;
            }

            dt = ds.Tables[0];



            if (itsId != 0)
            {
                EQUser student = new EQUser()
                {
                    ItsId = itsId,
                    primaryEmail = dt.Rows[0]["primary_email"].ToString(),
                    secondaryEmail = dt.Rows[0]["secondary_email"].ToString(),
                    chatId = dt.Rows[0]["Chat_ID"].ToString(),
                    mobile = dt.Rows[0]["mobile"].ToString(),
                    workPhone = dt.Rows[0]["work_phone"].ToString(),
                    residencePhone = dt.Rows[0]["residence_phone"].ToString(),
                    eqId = dt.Rows[0]["eqid"].ToString(),
                };
                return student;
            }
            else
            {
                return null;
            }
        }

        public HifzData GetCurrentHifzStatus(int itsId)
        {
            try
            {
                //ElearningWS.Mahadws ws = new ElearningWS.Mahadws();
                // log.Debug("Mahad Webservice called:");
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //ds = ws.GetCurrentHifz_Camps(itsId, ELEARNING_SERVICE_KEY);
                // if (ds != null) { log.Debug("Got Mahad Webservice Hifz DataSet:" + ds); }
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    // log.Debug("foreach HifzObject :" + "Surah" + "==" + dt.Rows[0]["Surah"].ToString());
                    // log.Debug("foreach HifzObject :" + "Aayah" + "==" + dt.Rows[0]["Aayah"].ToString());
                    // log.Debug("foreach HifzObject :" + "Page" + "==" + dt.Rows[0]["Page"].ToString());
                    // log.Debug("foreach HifzObject :" + "Juz" + "==" + dt.Rows[0]["Juz"].ToString());
                    // log.Debug("foreach HifzObject :" + "Sanah" + "==" + dt.Rows[0]["Sanah"].ToString());
                    // log.Debug("foreach HifzObject :" + "Grade" + "==" + dt.Rows[0]["Grade"].ToString());
                    // log.Debug("foreach HifzObject :" + "Sanad" + "==" + dt.Rows[0]["Sanad"].ToString());
                    // log.Debug("foreach HifzObject :" + "Last_Entry_Date" + "==" + dt.Rows[0]["Last_Entry_Date"].ToString());
                    DateTime Last_EntryDate;
                    DateTime.TryParse(dt.Rows[0]["Last_Entry_Date"].ToString(), out Last_EntryDate);
                    HifzData Hifz = new HifzData()
                    {
                        ItsId = itsId,
                        Surah = dt.Rows[0]["Surah"].ToString(),
                        Aayah = dt.Rows[0]["Aayah"].ToString(),
                        Page = dt.Rows[0]["Page"].ToString(),
                        Juz = dt.Rows[0]["Juz"].ToString(),
                        Sanah = dt.Rows[0]["Sanah"].ToString(),
                        Last_Entry_Date = Last_EntryDate,
                        Grade = dt.Rows[0]["Grade"].ToString(),
                        Sanad = dt.Rows[0]["Sanad"].ToString(),
                        Ikht_Sanah = dt.Rows[0]["Ikht_Sanah"].ToString(),

                    };

                    return Hifz;

                }
                else return null;
            }
            catch (Exception ex)
            {
                // log.Error("GetCurrentHifzData Failed: itsId=" + itsId, ex);
                return null;
            }
        }

        // ********************* ELEARNING STUDENTS  *****************************

        public string GetBranchId(int itsId)
        {
            try
            {

                var url = "https://api.elearningquran.com/general/GetEQID?ejid=" + itsId;
                var httpRequest = WebRequest.CreateHttp(url);
                httpRequest.Method = "GET";
                httpRequest.Timeout = 60000;
                httpRequest.ContentType = "application/json; charset=utf-8";

                httpRequest.Headers.Add("key", ELEARNING_SERVICE_SECRET);
                httpRequest.Headers.Add("clientid", ELEARNING_SERVICE_CLIENT);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                List<mz_student> students = new List<mz_student>();
                //List<dynamic> stu = JsonConvert.DeserializeObject<List<dynamic>>(httpResponse);

                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string jsonResponse = reader.ReadToEnd();

                    // Deserialize the JSON response into a List of YourObjectType.
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    // Now 'resultList' contains the parsed objects from the response.
                    if (result["groupID"])
                    {
                        return result["groupID"];
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                // log.Error(" Failed: itsId=" + itsId, ex);
                return null;
            }
        }

        public List<mz_student> Test_GetStudents_fromGroup(int itsId)
        {
            List<mz_student> students = new List<mz_student>();

            List<elq_students> resultList = new List<elq_students>
          {
    new elq_students(){
        ejid = "30320994",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "78652198",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30304035",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50465734",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30377251",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30302518",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30434597",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30431017",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30307959",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30329782",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30314723",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30330936",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "60446819",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30304552",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30314867",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30303941",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50453465",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30464621",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30326847",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30329259",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40900998",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50462650",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30601315",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "60400012",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30368985",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30488689",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30366771",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30706123",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30326979",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40903573",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30909042",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30310486",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40907666",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "78652531",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30338172",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30915069",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30904878",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40902910",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30921951",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30902956",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30315123",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40800602",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30337052",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30611070",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40910394",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30803278",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30140482",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40914409",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30921965",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40913414",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40800789",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "78652532",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40915497",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40495098",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30306781",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40909752",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40911616",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30906493",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30910415",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40911412",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30904480",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40911618",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30315124",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40903058",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117710",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50171479",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "78652546",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30919753",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30919752",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30141653",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30802506",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30338935",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30914399",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40916852",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40156129",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40908685",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40916150",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30114727",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117618",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117619",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50184092",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30326142",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40916350",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "60413653",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117441",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40917662",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40913703",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40912485",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30935252",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40900231",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30151118",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40919872",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30118150",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40150899",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30116866",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30318283",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117386",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40918585",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30154845",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40151910",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117099",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50467218",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30116003",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40914797",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "60481833",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40154148",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40915836",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40918837",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30418228",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30152382",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30922040",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50172687",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50463150",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40150302",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40920834",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30315845",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40173162",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40718424",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30925903",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30912005",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40154370",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30152710",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40175352",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40170450",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117107",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40918832",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40171806",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30382626",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40912563",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30154258",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40912420",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40920277",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40910956",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40152988",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40920872",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40914699",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40151923",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117440",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40918855",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40153401",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40170969",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40911425",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50171861",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50171862",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50457296",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30327943",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30306696",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "78652578",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30312996",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40170976",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30152575",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40183894",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30322371",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50161981",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40155572",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40161553",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40173167",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40161501",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40154012",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40161201",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30610777",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40163700",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40914700",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40160860",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50183999",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40915155",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30515253",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40156202",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40918151",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40171951",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40180135",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30152453",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40183887",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40154546",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30153923",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40459150",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50171280",
        branch = "Tahfeez_Mumbai",
        stage_id = -1
    },
    new elq_students(){
        ejid = "40204581",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40173719",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30914393",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30905813",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50180179",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40171813",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40173951",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40154118",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40182323",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30142253",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30153947",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40172094",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40161389",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50183583",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30117240",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40172053",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40170982",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "50180707",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40173161",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40154109",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30118131",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "30160065",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    },
    new elq_students(){
        ejid = "40156196",
        branch = "Tahfeez_Mumbai",
        stage_id = 2
    }
          };

            // Now 'resultList' contains the parsed objects from the response.
            for (int i = 0; i < resultList.Count; i++)
            {
                mz_student s = new mz_student()
                {
                    itsID = Convert.ToInt32(resultList[i].ejid),
                    elq_BranchName = resultList[i].branch.ToString(),
                    elq_GroupId = Convert.ToInt32(resultList[i].stage_id),

                };
                students.Add(s);
            }
            return students;

        }

        public List<mz_student> GetStudents_FromGroup(int groupId)
        {
            // try
            // {

            var url = "https://api.elearningquran.com/general/GetCampStudents_FromGroup?groupid=" + groupId;
            var httpRequest = WebRequest.CreateHttp(url);
            httpRequest.Method = "GET";
            httpRequest.Timeout = 60000;
            httpRequest.ContentType = "application/json; charset=utf-8";

            httpRequest.Headers.Add("key", ELEARNING_SERVICE_SECRET);
            httpRequest.Headers.Add("clientid", ELEARNING_SERVICE_CLIENT);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            List<mz_student> students = new List<mz_student>();
            //List<dynamic> stu = JsonConvert.DeserializeObject<List<dynamic>>(httpResponse);

            using (var responseStream = httpResponse.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                string jsonResponse = reader.ReadToEnd();


                // Deserialize the JSON response into a List of YourObjectType.
                List<elq_students> resultList = JsonConvert.DeserializeObject<List<elq_students>>(jsonResponse);

                // Now 'resultList' contains the parsed objects from the response.
                for (int i = 0; i < resultList.Count; i++)
                {
                    mz_student s = new mz_student()
                    {
                        itsID = Convert.ToInt32(resultList[i].ejid),
                        elq_BranchName = resultList[i].branch.ToString(),
                        elq_GroupId = Convert.ToInt32(resultList[i].stage_id),

                    };
                    students.Add(s);
                }
            }



            return students;

            // }
            // catch (Exception ex)
            // {
            //    // log.Error(" Failed: GroupId=" + groupId, ex);
            //     return null;
            // }
        }


        // ********************* ELEARNING INCOME  *****************************
        public List<MahadIncome> GetElearning_Income_InDateRange_BranchWise(string fDate, string tDate, int branchId, string branchName)
        {
            try
            {
                //ElearningWS.Mahadws ws = new ElearningWS.Mahadws();
                // log.Debug("Mahad Webservice called:");
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //ds = ws.GetBranch_Receipts(ELEARNING_INCOME_SERVICE_KEY, fDate, tDate, branchId, OFFICE_BEARER_ITSID);
                // if (ds != null) { log.Debug("Got Mahad Webservice Total Income on ELearning Branch Wise in a DataSet: " + ds); }
                dt = ds.Tables[0];

                List<MahadIncome> IncomeList = new List<MahadIncome>();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MahadIncome Income = new MahadIncome();

                        Income.Currency_INR = dt.Rows[i]["INR_Amount"].ToString();
                        Income.Currency_PKR = dt.Rows[i]["PKR_Amount"].ToString();
                        Income.Currency_USD = dt.Rows[i]["USD_Amount"].ToString();
                        Income.Currency_USD = dt.Rows[i]["USD_Amount"].ToString();
                        Income.Mode = dt.Rows[i]["mode"].ToString();

                        Income.CollectionCenter = dt.Rows[i]["Collection_Center"].ToString();
                        Income.Branch = branchName;

                        IncomeList.Add(Income);
                    }
                    // log.Debug("Income Object : ");

                    return IncomeList;
                }
                else return null;
            }
            catch (Exception ex)
            {
                // log.Error("GetElearning_Income_InDateRange_BranchWise Failed: branchID= " + branchId, ex);
                List<MahadIncome> IncomeList = new List<MahadIncome>();
                MahadIncome Income = new MahadIncome();
                Income.Currency_INR = "0";
                Income.Currency_PKR = "0";
                Income.Currency_USD = "0";
                Income.Mode = null;
                Income.CollectionCenter = null;
                Income.Remark = "No income in selected Date Range";
                Income.Branch = branchName;

                IncomeList.Add(Income);
                return IncomeList;
            }
        }
        public List<MahadIncome> GetElearning_Income_InDateRange_2_BranchWise(string fDate, string tDate, int branchId, string branchName)
        {
            try
            {
                //ElearningWS.Mahadws ws = new ElearningWS.Mahadws();
                // log.Debug("Mahad Webservice called:");
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //ds = ws.GetBranch_Receipts_Currencywise("currencywise#48652#", fDate, tDate, branchId);
                // if (ds != null) { log.Debug("Got Mahad Webservice Total Income on ELearning Branch Wise in a DataSet: " + ds); }
                dt = ds.Tables[0];

                List<MahadIncome> IncomeList = new List<MahadIncome>();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MahadIncome Income = new MahadIncome();


                        Income.amount = Convert.ToInt32(dt.Rows[i]["amount"].ToString());
                        Income.Mode = dt.Rows[i]["mode"].ToString();
                        Income.Currency = dt.Rows[i]["Currency"].ToString();
                        Income.CollectionCenter = dt.Rows[i]["Collection_Center"].ToString();
                        Income.Branch = branchName;

                        IncomeList.Add(Income);
                    }
                    // log.Debug("Income Object : ");

                    return IncomeList;
                }
                else return null;
            }
            catch (Exception ex)
            {
                // log.Error("GetElearning_Income_InDateRange_BranchWise Failed: branchID= " + branchId, ex);
                List<MahadIncome> IncomeList = new List<MahadIncome>();
                MahadIncome Income = new MahadIncome();
                Income.Currency_INR = "0";
                Income.Currency_PKR = "0";
                Income.Currency_USD = "0";
                Income.Mode = null;
                Income.CollectionCenter = null;
                Income.Remark = "No income in selected Date Range";
                Income.Branch = branchName;

                IncomeList.Add(Income);
                return IncomeList;
            }
        }

        public List<MahadIncome> GetElearning_Ewallet_Refill_InDateRange(string fDate, string tDate)
        {
            try
            {

                List<MahadIncome> IncomeList = new List<MahadIncome>();

                var url = "https://api.elearningquran.com/general/GetBranch_Ewallet_Refill?dtfrom" + fDate + "&dtTo=" + tDate;
                var httpRequest = WebRequest.CreateHttp(url);
                httpRequest.Method = "GET";
                httpRequest.Timeout = 60000;
                httpRequest.ContentType = "application/json; charset=utf-8";

                httpRequest.Headers.Add("key", ELEARNING_SERVICE_SECRET);
                httpRequest.Headers.Add("clientid", ELEARNING_SERVICE_CLIENT);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                List<mz_student> students = new List<mz_student>();
                //List<dynamic> stu = JsonConvert.DeserializeObject<List<dynamic>>(httpResponse);

                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string jsonResponse = reader.ReadToEnd();

                    // Deserialize the JSON response into a List of YourObjectType.
                    List<dynamic> resultList = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse);

                    // Now 'resultList' contains the parsed objects from the response.
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        MahadIncome Income = new MahadIncome();

                        Income.Currency_INR = resultList[i]["INR_Amount"].ToString();
                        Income.Currency_PKR = resultList[i]["PKR_Amount"].ToString();
                        Income.Currency_USD = resultList[i]["USD_Amount"].ToString();
                        Income.Mode = resultList[i]["Mode"].ToString();
                        Income.CollectionCenter = resultList[i]["Collection_Center"].ToString();
                        Income.Branch = "E-Wallet Refill";

                        IncomeList.Add(Income);
                    }
                }



                return IncomeList;

            }
            catch (Exception ex)
            {
                // log.Error("GetElearning_Ewaller_Refill_InDateRange Failed", ex);
                return null;
            }
        }

        // ********************* ELEARNING DUES  *****************************
        public List<MahadDues> GetElearning_Dues_InDateRange_BranchWise(int branchId, string branchName)
        {
            try
            {
                //ElearningWS.Mahadws ws = new ElearningWS.Mahadws();
                // log.Debug("Mahad Webservice called:");
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //ds = ws.GetBranch_Dues(ELEARNING_DUES_SERVICE_KEY, branchId, OFFICE_BEARER_ITSID);
                // if (ds != null) { log.Debug("Got Mahad Webservice Total Income on ELearning Branch Wise in a DataSet: " + ds); }
                dt = ds.Tables[0];

                foreach (var col in dt.Columns)
                {
                    // log.Debug("ColName : " + col);
                }

                List<MahadDues> DuesList = new List<MahadDues>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int itsId;
                        int.TryParse(dt.Rows[i]["Acc_JamaatNo"].ToString(), out itsId);
                        MahadDues Due = new MahadDues();

                        Due.ItsId = itsId;
                        Due.Currency = dt.Rows[i]["Currency"].ToString();
                        Due.Amount = dt.Rows[i]["Due_amt"].ToString();
                        Due.Branch = branchName;

                        DuesList.Add(Due);
                    }
                    // log.Debug("Income Object : ");

                    return DuesList;
                }

                return null;
            }
            catch (Exception ex)
            {
                // log.Error("GetElearning_Income_InDateRange_BranchWise Failed: branchID= " + branchId, ex);
                return null;
            }
        }

        public MahadDues GetStudent_Dues(int itsId)
        {

            try
            {
                MahadDues due = new MahadDues();

                List<MahadIncome> IncomeList = new List<MahadIncome>();

                var url = "https://api.elearningquran.com/general/GetStudentDues?ejid=" + itsId;
                var httpRequest = WebRequest.CreateHttp(url);
                httpRequest.Method = "GET";
                httpRequest.Timeout = 60000;
                httpRequest.ContentType = "application/json; charset=utf-8";

                httpRequest.Headers.Add("key", ELEARNING_SERVICE_SECRET);
                httpRequest.Headers.Add("clientid", ELEARNING_SERVICE_CLIENT);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                List<mz_student> students = new List<mz_student>();
                //List<dynamic> stu = JsonConvert.DeserializeObject<List<dynamic>>(httpResponse);

                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string jsonResponse = reader.ReadToEnd();

                    // Deserialize the JSON response into a List of YourObjectType.
                    List<dynamic> resultList = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse);

                    int its;
                    int.TryParse(((IDictionary<string, object>)resultList[0])["ejid"].ToString(), out its);
                    due.ItsId = its;
                    due.Currency = resultList[0]["Currency"].ToString();
                    due.Amount = resultList[0]["Due_amt"].ToString();


                    // log.Debug("Income Object : ");

                    return due;
                }

            }
            catch (Exception ex)
            {
                // log.Error("GetElearning_Income_InDateRange_BranchWise Failed: branchID= ");
                return null;
            }

        }

        public List<MahadIncome> GetElearning_Ewallet_Used_InDateRange_BranchWise(string fDate, string tDate, int branchId, string branchName)
        {
            try
            {

                List<MahadIncome> IncomeList = new List<MahadIncome>();

                var url = "https://api.elearningquran.com/general/GetBranch_Ewallet_Used?dtFrom=" + fDate + "&dtTo=" + tDate + "&Branch_ID=" + branchId + "&Office_bearer_EJID=" + OFFICE_BEARER_ITSID;
                var httpRequest = WebRequest.CreateHttp(url);
                httpRequest.Method = "GET";
                httpRequest.Timeout = 60000;
                httpRequest.ContentType = "application/json; charset=utf-8";

                httpRequest.Headers.Add("key", ELEARNING_SERVICE_SECRET);
                httpRequest.Headers.Add("clientid", ELEARNING_SERVICE_CLIENT);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                List<mz_student> students = new List<mz_student>();
                //List<dynamic> stu = JsonConvert.DeserializeObject<List<dynamic>>(httpResponse);

                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string jsonResponse = reader.ReadToEnd();

                    // Deserialize the JSON response into a List of YourObjectType.
                    List<dynamic> resultList = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse);

                    // Now 'resultList' contains the parsed objects from the response.
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        MahadIncome Income = new MahadIncome();

                        Income.Currency_INR = resultList[i]["INR_Amount"].ToString();
                        Income.Currency_PKR = resultList[i]["PKR_Amount"].ToString();
                        Income.Currency_USD = resultList[i]["USD_Amount"].ToString();
                        Income.Mode = resultList[i]["Mode"].ToString();
                        Income.CollectionCenter = resultList[i]["Collection_Center"].ToString();
                        Income.Branch = branchName;

                        IncomeList.Add(Income);
                    }
                }



                return IncomeList;

            }
            catch (Exception ex)
            {
                // log.Error("GetElearning_Ewaller_Used_InDateRange Failed", ex);
                List<MahadIncome> IncomeList = new List<MahadIncome>();
                MahadIncome Income = new MahadIncome();
                Income.Currency_INR = "0";
                Income.Currency_PKR = "0";
                Income.Currency_USD = "0";
                Income.Mode = null;
                Income.CollectionCenter = null;
                Income.Remark = "No income in selected Date Range";
                Income.Branch = branchName;

                IncomeList.Add(Income);
                return IncomeList;
            }
        }

    }
}
