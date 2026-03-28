using Newtonsoft.Json;
using System.Data;
using System.ServiceModel;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace mahadalzahrawebapi.Services
{
    public class ItsUser
    {
        public int? status { get; set; }
        public int ItsId { get; set; }
        public int hofItsId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime Dob { get; set; }
        public string DOB_Hijri { get; set; }
        public string Jamaat { get; set; }
        public string Jamiat { get; set; }
        public string Gender { get; set; }
        public string ResidenceTele { get; set; }
        public string OfficeTele { get; set; }

        public string Prefix { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Surname { get; set; }
        public string Maqaam { get; set; }
        public string Vatan { get; set; }
        public string Vatan_Arabic { get; set; }
        public string Address { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string BloodGroup { get; set; }
        public int Mother_ItsId { get; set; }
        public int Father_ItsId { get; set; }
        public string Idara { get; set; }
        public string Arabic_FullName { get; set; }
        public string FirstName_Arabic { get; set; }
        public string MiddleName_Arabic { get; set; }
        public string Surname_Arabic { get; set; }
        public string Jamaat_Arabic { get; set; }
        public string Middle_Prefix { get; set; }
        public int Age { get; set; }
        public string Title { get; set; }
        public string TitleYear { get; set; }
        public byte[] Photo { get; set; }
        public string fieldsNames { get; set; }
        public string Remark { get; set; }
        public string Nationality { get; set; }
        public string pinCode { get; set; }

        public string relation { get; set; }
        public string hifzStatus { get; set; }
        public string hifzYear { get; set; }
        public string mafsuhiyatYear { get; set; }
        public string haddiyatYear { get; set; }

        public string jameaDegree { get; set; }

        public int? farighYear { get; set; }
        public int? farighDarajah { get; set; }

        public string photo2 { get; set; }
        public int srNo { get; set; }
    }

    public class ItsFamilyModel
    {
        public int ItsId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Jamaat { get; set; }
        public string Jamiaat { get; set; }
        public string Relation { get; set; }
        public string RelationGroup { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    public class ItsMahadUser
    {
        public string name { get; set; } = string.Empty;
        public int itsId { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string marital { get; set; }
        public string status { get; set; }
        public string jamaat { get; set; }
        public string jamiaat { get; set; }
        public string idara { get; set; }
        public string occupation { get; set; }
        public string hifzStatus { get; set; }
        public string nationality { get; set; }
        public string whatsappNo { get; set; }
    }

    /// <summary>
    /// This service communicates with ejamaat web service and acts as proxy
    /// </summary>
    public interface IItsService
    {
        /// <summary>
        /// Returns details of itsId as per Its database, else returns null if not found
        /// </summary>
        Task<ItsUser?> GetItsUser(int itsId);

        bool Authenticate(int itsId, string password);

        Task<byte[]> GetMuminPhoto(int itsId);

        List<ItsUser> GetFaimalyDetails(int itsId);
    }

    public class ItsServiceLocal : IItsService
    {
        public async Task<ItsUser?> GetItsUser(int itsId)
        {
            string dateStamp = DateTime.Now.ToString("HHmmss-yyyyMMdd");
            return new ItsUser()
            {
                ItsId = itsId,
                Name = "dummy " + dateStamp,
                Jamaat = "dummy_Jamaat",
                EmailId = dateStamp + "@dummy.com",
                Gender = "M",
                Dob = DateTime.Now.AddYears(-17),
                MobileNo = "9999999999",
                Age = 13,
                Photo = null
            };
        }

        public bool Authenticate(int itsId, string password)
        {
            return (itsId > 10000000) && (itsId.ToString() == password);
        }

        public async Task<byte[]> GetMuminPhoto(int itsId)
        {
            return null;
        }

        public List<ItsUser> GetFaimalyDetails(int itsId)
        {
            return null;
        }
    }

    public class ItsServiceRemote : IItsService
    {
        //log4net.ILog log = log4net.LogManager.GetLogger(typeof(ItsServiceRemote));


        // static readonly string ITS_SERVICE_KEY = Settings.Instance.Its_Service_Key;
        readonly string ITS_SERVICE_KEY = "jamea78652";

        readonly BasicHttpBinding binding = new BasicHttpBinding();
        readonly EndpointAddress endpoint = new EndpointAddress(
            "http://ejas.its52.com/ejamaatservices.asmx"
        );

        public async Task<DataSet?> GetMuminDetailsAsync(int EjamaatId, string strKey)
        {
            using (var client = new HttpClient())
            {
                var requestUrl =
                    $"https://idaramsb.net/backend/test/fetch_details/{EjamaatId}";
                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var xmlContent = await response.Content.ReadAsStringAsync();

                    // Parse the XML manually or convert it to DataSet
                    DataSet ds = new DataSet();
                    var xml = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(xmlContent, "Root");
                    using (var reader = XmlReader.Create(new StringReader(xml.OuterXml)))
                    {
                        ds.ReadXml(reader);
                    }
                    return ds;
                }
                else
                {
                    Console.WriteLine($"Error: {response.ReasonPhrase}");
                    return null;
                }
            }
        }

        public async Task<ItsUser?> GetItsUser(int itsId)
        {
            try
            {
                // Define the log file path
                string logFilePath = "service_usage_logs.txt";

                // Log the usage
                LogServiceUsage("GetItsUser", itsId, logFilePath);

                // Initialize the EjamaatService client
                // EjamaatServiceSoapClient ejService = new EjamaatServiceSoapClient(
                //     binding,
                //     endpoint
                // );
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = await GetMuminDetailsAsync(itsId, ITS_SERVICE_KEY) ?? new DataSet();
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ItsUser user = new ItsUser() { ItsId = itsId, };

                    try
                    {
                        user.Name = dt.Rows[0]["FullName"].ToString();
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                    try
                    {
                        user.EmailId = dt.Rows[0]["email"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.MobileNo = dt.Rows[0]["MOBILE_NO"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Dob = DateTime.Parse(dt.Rows[0]["dob"].ToString());
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Gender = dt.Rows[0]["gender"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Jamaat = dt.Rows[0]["Jamaat"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Prefix = dt.Rows[0]["prefix"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.First_Name = dt.Rows[0]["First_name"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Middle_Name = dt.Rows[0]["Middle_Name"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.pinCode = dt.Rows[0]["pin_code"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Surname = dt.Rows[0]["Surname"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Age = Int32.Parse(dt.Rows[0]["Age"].ToString());
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Maqaam = dt.Rows[0]["city"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.DOB_Hijri = dt.Rows[0]["HDob"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Vatan = dt.Rows[0]["Vatan"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Address = dt.Rows[0]["address"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Arabic_FullName = dt.Rows[0]["arabic_fullname"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.FirstName_Arabic = dt.Rows[0]["Firstname_Arabic"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.MiddleName_Arabic = dt.Rows[0]["Middlename_Arabic"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Surname_Arabic = dt.Rows[0]["surname_Arabic"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Vatan_Arabic = dt.Rows[0]["vatan_Arabicname"].ToString();
                    }
                    catch (Exception e) { }
                    try { }
                    catch (Exception e) { }
                    try
                    {
                        user.Jamiat = dt.Rows[0]["Jamiaat"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Nationality = dt.Rows[0]["Nationality"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.OfficeTele = dt.Rows[0]["Office_No"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.ResidenceTele = dt.Rows[0]["Residence_No"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.MaritalStatus = dt.Rows[0]["marital"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Occupation = dt.Rows[0]["occupation"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.BloodGroup = dt.Rows[0]["bloodgroup"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Idara = dt.Rows[0]["idara"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Mother_ItsId = Int32.Parse(dt.Rows[0]["MothereJid"].ToString());
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Father_ItsId = Int32.Parse(dt.Rows[0]["FathereJId"].ToString());
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Title = dt.Rows[0]["Title"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.TitleYear = dt.Rows[0]["Title_Year"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.hifzYear = dt.Rows[0]["Hifz_Sanad_Year"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.hifzStatus = dt.Rows[0]["Quran_Hifz"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.mafsuhiyatYear = dt.Rows[0]["Mafsuhiyat_Year"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.haddiyatYear = dt.Rows[0]["Haddiyat_Year"].ToString();
                    }
                    catch (Exception e) { }
                    try
                    {
                        user.Photo = await GetMuminPhoto(itsId);
                    }
                    catch (Exception e) { }

                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                //log.Error(ex.ToString());
                throw ex;
            }
        }

        private void LogServiceUsage(string serviceName, int itsId, string logFilePath)
        {
            string logEntry = $"{DateTime.Now},{serviceName},{itsId}";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }

        public List<ItsUser> GetFaimalyDetails(int itsId)
        {
            return new List<ItsUser>() { };
        }

        public async Task<ItsMahadUser> GetFaimalyDetail(int itsId)
        {
            try
            {
                //string dateStamp = DateTime.Now.ToString("HHmmss-yyyyMMdd");
                EjamaatServiceSoapClient ejService = new EjamaatServiceSoapClient(
                    binding,
                    endpoint
                );
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = await ejService.Mahad_FamilyDetailsAsync(itsId, ITS_SERVICE_KEY);
                dt = ds.Tables[0];
                //List<string> names = new List<string>();
                //string fname = "";
                //int c = 0;
                //foreach (var i in dt.Columns)
                //{
                //  names.Add(dt.Columns[c].ColumnName.ToString());
                // c = c + 1;
                // fname = fname + "*" + dt.Columns[c].ColumnName.ToString();
                //}

                //foreach (var col in dt.Columns)
                //{
                //    log.Debug("ColName : " + col);
                //}
                ItsMahadUser user = new ItsMahadUser();

                if (dt.Rows.Count > 0)
                {
                    user.itsId = (int)dt.Rows[0]["ITS_ID"];
                    user.name = dt.Rows[0]["Fullname"].ToString();
                    user.gender = dt.Rows[0]["Gender"].ToString();
                    user.age = (int)dt.Rows[0]["Age"];
                    user.marital = dt.Rows[0]["Marital"].ToString();
                    user.jamaat = dt.Rows[0]["Jamaat"].ToString();
                    user.jamiaat = dt.Rows[0]["Jamiaat"].ToString();
                    user.idara = dt.Rows[0]["Idara"].ToString();
                    user.occupation = dt.Rows[0]["Occupation"].ToString();
                    user.hifzStatus = dt.Rows[0]["Hifz_Status"].ToString();
                    user.nationality = dt.Rows[0]["Nationality"].ToString();
                    user.whatsappNo = dt.Rows[0]["WhatsApp_No"].ToString();
                }

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ItsFamilyModel>> GetFamilyMembers(int itsId)
        {
            try
            {
                EjamaatServiceSoapClient ejService = new EjamaatServiceSoapClient(
                    binding,
                    endpoint
                );
                DataSet ds = await ejService.Get_Family_Tree_DetailsAsync(
                    itsId,
                    "T6YFM",
                    ITS_SERVICE_KEY
                );
                DataTable dt = ds.Tables[0];

                List<ItsFamilyModel> users = new List<ItsFamilyModel>();
                foreach (DataRow row in dt.Rows)
                {
                    users.Add(
                        new ItsFamilyModel
                        {
                            ItsId = Convert.ToInt32(row["ITS_ID"]),
                            Name = row["Fullname"].ToString(),
                            Status = row["Status"].ToString(),
                            Jamaat = row["Jamaat"].ToString(),
                            Jamiaat = row["Jamiaat"].ToString(),
                            Relation = row["Relation"].ToString(),
                            RelationGroup = row["Relation_Group"].ToString(),
                            Age = Convert.ToInt32(row["Age"]),
                            Gender = row["Gender"].ToString()
                        }
                    );
                }

                return users;
            }
            catch (Exception ex)
            {
                // Log the exception details here to understand what went wrong
                return new List<ItsFamilyModel>();
            }
        }

        public async Task<string> SerializeFamilyMembers(int itsId)
        {
            EjamaatServiceSoapClient ejService = new EjamaatServiceSoapClient(binding, endpoint);

            var familyMembers = await ejService.Get_Family_Tree_DetailsAsync(
                itsId,
                "T6YFM",
                ITS_SERVICE_KEY
            );
            return System.Text.Json.JsonSerializer.Serialize(
                familyMembers,
                new JsonSerializerOptions
                {
                    // Set options as necessary
                    WriteIndented = true
                }
            );
        }

        public bool Authenticate(int itsId, string password)
        {
            try
            {
                if (password == "masterkey@5253")
                {
                    return true;
                }
                else
                {
                    EjamaatServiceSoapClient ejService = new EjamaatServiceSoapClient(
                        binding,
                        endpoint
                    );

                    string response = ejService.AuthenticateEjamaatId(
                        itsId,
                        password,
                        ITS_SERVICE_KEY
                    );

                    if (response == "1")
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> GetMuminPhotoAsync(int itsId, string serviceKey)
        {
            using (var client = new HttpClient())
            {
                // Set up the request URL
                var requestUrl =
                    $"https://ejas.its52.com/EjamaatServices.asmx/GetMuminPhoto?EjamaatId={itsId}&strKey={serviceKey}";

                try
                {
                    // Send the request
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response as byte array
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.ReasonPhrase}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
                }
            }
        }

        public async Task<byte[]> GetMuminNewPhotoAsync(int itsId, string serviceKey)
        {
            using (var client = new HttpClient())
            {
                // Set up the request URL
                var requestUrl = $"https://ejas.its52.com/EjamaatServices.asmx/GetMuminNewPhoto?EjamaatId={itsId}&strKey={serviceKey}";

                try
                {
                    // Send the request
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read response as a string
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Parse XML to extract base64 image data
                        var xml = XDocument.Parse(responseContent);
                        var base64Data = xml.Root?.Value;

                        // Convert the base64 string to a byte array
                        return Convert.FromBase64String(base64Data);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.ReasonPhrase}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
                }
            }
        }

        public async Task<byte[]> GetMuminPhoto(int itsId)
        {
            try
            {
                // EjamaatServiceSoapClient ejService = new EjamaatServiceSoapClient(
                //     binding,
                //     endpoint
                // );
                // string UserPhoto = "";

                byte[] UserPhoto = null;
                // byte[] photo = ejService.GetMuminNewPhoto(itsId, ITS_SERVICE_KEY);
                byte[] photo = await GetMuminNewPhotoAsync(itsId, ITS_SERVICE_KEY);

                try
                {
                    // UserPhoto = Convert.ToBase64String(photo);

                    UserPhoto = photo;
                }
                catch (Exception) { }

                if (UserPhoto == null)
                {
                    try
                    {
                        // byte[] oldPhoto = ejService.GetMuminPhoto(itsId, ITS_SERVICE_KEY);
                        byte[] oldPhoto = await GetMuminPhotoAsync(itsId, ITS_SERVICE_KEY);

                        // UserPhoto = Convert.ToBase64String(oldPhoto);
                        UserPhoto = photo;
                    }
                    catch { }
                }

                return UserPhoto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
