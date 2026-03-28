using System.Data;
using System.Xml.Linq;

namespace mahadalzahrawebapi.Services
{

    public class JHSAcademicData
    {
        public int itsId { get; set; }

        public string jameaDegree { get; set; }

        public int? farighYear { get; set; }
        public int? farighDarajah { get; set; }

    }

    /// <summary>
    /// This service communicates with ejamaat web service and acts as proxy
    /// </summary>
    public interface IJHSService
    {
        /// <summary>
        /// Returns details of itsId as per Its database, else returns null if not found
        /// </summary>
        Task<JHSAcademicData> GetJHSAcademicData(int itsId);
    }

    internal class IJHSServiceLocal : IJHSService
    {
        // log4net.ILog log = // log4net.LogManager.GetLogger(typeof(ItsServiceLocal));
        public async Task<JHSAcademicData> GetJHSAcademicData(int itsId)
        {
            return new JHSAcademicData()
            {
                itsId = itsId,
                farighDarajah = 0,
                farighYear = 999,
                jameaDegree = "N/A"
            };
        }
    }

    internal class IJHSServiceRemote : IJHSService
    {
        //readonly BasicHttpBinding binding = new BasicHttpBinding();
        //readonly EndpointAddress endpoint = new EndpointAddress("http://jameasaifiyah.org/");

        public async Task<JHSAcademicData> GetJHSAcademicData(int itsId)
        {
            string url = $"http://jameasaifiyah.org/JHSService.asmx/MahadJameaDegree?Token=Degree45874847&ITSID={itsId}";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(url);
                    var xmlDocument = XDocument.Parse(response);
                    var ns = xmlDocument.Root.GetDefaultNamespace();
                    var dataTable = new DataTable();

                    // Loop through each element and add to DataTable
                    foreach (var element in xmlDocument.Descendants("Degree"))
                    {
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (var child in element.Elements())
                            {
                                dataTable.Columns.Add(child.Name.LocalName, typeof(string));
                            }
                        }

                        var row = dataTable.NewRow();
                        foreach (var child in element.Elements())
                        {
                            row[child.Name.LocalName] = child.Value;
                        }
                        dataTable.Rows.Add(row);
                    }

                    // Assuming the first row contains the desired data
                    if (dataTable.Rows.Count > 0)
                    {
                        var row = dataTable.Rows[0];
                        var user = new JHSAcademicData
                        {
                            itsId = itsId,
                            jameaDegree = row["JameaDegree"].ToString(),
                            farighYear = int.TryParse(row["FarighYear"].ToString(), out var fy) ? fy : (int?)null,
                            farighDarajah = int.TryParse(row["FarighDarajah"].ToString(), out var fd) ? fd : (int?)null
                        };
                        return user;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    // Log error, handle exception as needed
                    return null;
                }
            }
        }
    }
}
