using Abp.MimeTypes;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Humanizer;
using ICSharpCode.SharpZipLib.Zip;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Data.Entity;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace mahadalzahrawebapi.Services
{
    public class HelperService
    {
        private readonly mzdbContext _context;

        private readonly string accessKey = "";
        private readonly string secretKey = "";
        private string bucketName = "";
        public RegionEndpoint region = RegionEndpoint.APSouth1; // Replace with your desired region

        public globalConstants _globalConstants;

        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
        );

        public HelperService(mzdbContext context)
        {
            _context = context;
            _globalConstants = new globalConstants();
        }

        public HelperService()
        {
            _context = null;
            _globalConstants = new globalConstants();
        }

        public DateTime currentDate;
        private readonly string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        private readonly string[] months =
        {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };
        public List<WeekModel> calendar;

        public int GetFinancialYear(DateTime date)
        {
            int month = date.Month;
            int dateYear = date.Year;

            if (month == 1 || month == 2 || month == 3)
            {
                int year = dateYear - 1;
                return year;
            }
            else
            {
                return dateYear;
            }
        }

        public string ChangeToWords(string numb)
        {
            try
            {
                // Convert the number to a decimal
                decimal number = decimal.Parse(numb);

                // Split the number into whole number and fractional parts
                int wholeNumber = (int)number;
                int fractionalPart = (int)((number - wholeNumber) * 100);

                // Convert the whole number part to words
                string words = wholeNumber.ToWords();

                if (fractionalPart > 0)
                {
                    // Convert the fractional part to words if it exists
                    string fractionalWords = fractionalPart.ToWords();
                    words += $" point {fractionalWords}";
                }

                return words + " Only";
            }
            catch (Exception ex)
            {
                // Handle potential parsing errors
                return $"Error: {ex.Message}";
            }
        }

        public List<WeekModel> GenerateCalendar(CalenderModel firstDay, CalenderModel lastDay)
        {
            DateTime today = DateTime.Today;
            int year = currentDate.Year;
            int month = currentDate.Month;
            List<hijri_calender> hijriCal = _context.hijri_calender.ToList();
            List<hijri_months> hijriMOnths = _context.hijri_months.ToList();
            List<holiday_hijri_miqaat> miqaats = _context
                .holiday_hijri_miqaat.Where(x =>
                    x.date_id >= firstDay.hijDay
                    && x.date_id <= lastDay.hijDay
                    && x.month_id >= firstDay.hijMonth
                    && x.month_id <= lastDay.hijMonth
                )
                .ToList();

            calendar = new List<WeekModel>
            {
                new WeekModel
                {
                    weekNumber = GetWeekNumber(firstDay.engDate),
                    days = new List<DayModel>()
                }
            };

            int idCounter = 1;
            int startDayOfWeek = (int)firstDay.engDate.DayOfWeek;

            for (int i = startDayOfWeek; i > 0; i--)
            {
                DateTime tempDate = firstDay.engDate.AddDays(0 - i);
                hijri_calender h = hijriCal
                    .Where(x =>
                        x.english_day == tempDate.Day
                        && x.english_month == tempDate.Month
                        && x.english_year == tempDate.Year
                    )
                    .FirstOrDefault();
                hijri_months hmn = _context
                    .hijri_months.Where(x => x.id == h.hijri_month)
                    .FirstOrDefault();

                calendar[0]
                    .days.Add(
                        new DayModel
                        {
                            id = idCounter++,
                            gregDay = firstDay.engDate.AddDays(-i).Day,
                            dayOfWeek = startDayOfWeek - i,
                            isCurrentMonth = false,
                            hijriDay = h.hijri_day ?? 0,
                            hijriMonth = h.hijri_month ?? 0,
                            hijriMonthName = hmn.hijriMonthName,
                            hijriYear = h.hijri_year ?? 0,
                            engdate = tempDate.ToString("o")
                        }
                    );
            }

            for (DateTime date = firstDay.engDate; date <= lastDay.engDate; date = date.AddDays(1))
            {
                int weekNumber = GetWeekNumber(date);
                WeekModel week = calendar.FirstOrDefault(w => w.weekNumber == weekNumber);
                if (weekNumber == 1)
                {
                    WeekModel lastweek = calendar[calendar.Count() - 1];
                    if (lastweek.days.Count() != 7)
                    {
                        week = lastweek;
                    }
                }
                hijri_calender h = _context
                    .hijri_calender.Where(x =>
                        x.english_day == date.Day
                        && x.english_month == date.Month
                        && x.english_year == date.Year
                    )
                    .FirstOrDefault();
                hijri_months hmn = _context
                    .hijri_months.Where(x => x.id == h.hijri_month)
                    .FirstOrDefault();

                int srn = 1;

                List<eventsModel> events = new List<eventsModel>();
                miqaats
                    .Where(x => x.date_id == h.hijri_day && x.month_id == h.hijri_month)
                    .ToList()
                    .ForEach(x =>
                    {
                        events.Add(
                            new eventsModel
                            {
                                id = x.id,
                                eventName = x.miqaats_title,
                                details = x.miqaats_description,
                                eventType = "Miqaat",
                                srn = srn
                            }
                        );
                        srn++;
                    });

                if (week != null)
                {
                    week.days.Add(
                        new DayModel
                        {
                            id = idCounter++,
                            gregDay = date.Day,
                            dayOfWeek = (int)date.DayOfWeek,
                            isCurrentMonth = true,
                            isPast = date < firstDay.engDate,
                            hijriDay = h.hijri_day ?? 0,
                            hijriMonth = h.hijri_month ?? 0,
                            hijriMonthName = hmn.hijriMonthName,
                            hijriYear = h.hijri_year ?? 0,
                            events = events,
                            gregMonth = date.Month,
                            gregMonthName =
                                h.hijri_day == 1 || date.Day == 1 ? date.ToString("MMM") : "",
                            isToday = date == today,
                            engdate = date.ToString("o")
                        }
                    );
                }
                else
                {
                    calendar.Add(
                        new WeekModel
                        {
                            weekNumber = weekNumber,
                            days = new List<DayModel>
                            {
                                new DayModel
                                {
                                    id = idCounter++,
                                    gregDay = date.Day,
                                    dayOfWeek = (int)date.DayOfWeek,
                                    isCurrentMonth = true,
                                    isPast = date < firstDay.engDate,
                                    hijriDay = h.hijri_day ?? 0,
                                    hijriMonth = h.hijri_month ?? 0,
                                    hijriMonthName = hmn.hijriMonthName,
                                    hijriYear = h.hijri_year ?? 0,
                                    events = events,
                                    gregMonth = date.Month,
                                    gregMonthName =
                                        h.hijri_day == 1 || date.Day == 1
                                            ? date.ToString("MMM")
                                            : "",
                                    isToday = date == today,
                                    engdate = date.ToString("o")
                                }
                            }
                        }
                    );
                }
            }

            for (int i = ((int)lastDay.engDate.DayOfWeek + 1); i <= 6; i++)
            {
                DateTime tempDate = lastDay.engDate.AddDays(i - ((int)lastDay.engDate.DayOfWeek));
                hijri_calender h = hijriCal
                    .Where(x =>
                        x.english_day == tempDate.Day
                        && x.english_month == tempDate.Month
                        && x.english_year == tempDate.Year
                    )
                    .FirstOrDefault();
                hijri_months hmn = _context
                    .hijri_months.Where(x => x.id == h.hijri_month)
                    .FirstOrDefault();

                DateTime dateToAdd = firstDay.engDate.AddDays(-i);

                calendar[calendar.Count() - 1]
                    .days.Add(
                        new DayModel
                        {
                            id = idCounter++,
                            gregDay = dateToAdd.Day,
                            dayOfWeek = startDayOfWeek - i,
                            isCurrentMonth = false,
                            hijriDay = h.hijri_day ?? 0,
                            hijriMonth = h.hijri_month ?? 0,
                            hijriMonthName = hmn.hijriMonthName,
                            hijriYear = h.hijri_year ?? 0,
                            gregMonth = dateToAdd.Month,
                            gregMonthName =
                                h.hijri_day == 1 || dateToAdd.Day == 1
                                    ? dateToAdd.ToString("MMM")
                                    : "",
                            isToday = dateToAdd == today,
                        }
                    );
            }

            return calendar;
        }

        public int GetWeekNumber(DateTime date)
        {
            DateTime jan1 = new DateTime(date.Year, 1, 1);
            int daysOffset =
                (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstDayOfWeek = jan1.AddDays(daysOffset);
            int weekNum = (int)Math.Floor((double)(date.Subtract(firstDayOfWeek).Days / 7) + 1);

            return weekNum;
        }

        public List<int> parseItsId(string itsIdCSV)
        {
            List<int> itsIds = new List<int>();

            if (string.IsNullOrWhiteSpace(itsIdCSV))
            {
                return itsIds;
            }

            string[] tokens = itsIdCSV.Split(',');
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                int itsId;
                if (token.Length == 8 && Int32.TryParse(token, out itsId))
                {
                    itsIds.Add(itsId);
                }
                else
                    throw new Exception(string.Format("invalid itsId {0}", token));
            }
            return itsIds;
        }

        public List<int> parseIds(string itsIdCSV)
        {
            List<int> itsIds = new List<int>();

            if (string.IsNullOrWhiteSpace(itsIdCSV))
            {
                return itsIds;
            }

            string[] tokens = itsIdCSV.Split(',');
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                int itsId;
                if (Int32.TryParse(token, out itsId))
                {
                    itsIds.Add(itsId);
                }
                else
                    throw new Exception(string.Format("invalid itsId {0}", token));
            }
            return itsIds;
        }

        public List<string> parseStrings(string itsStringCSV)
        {
            List<string> itsIds = new List<string>();

            if (string.IsNullOrWhiteSpace(itsStringCSV))
            {
                return itsIds;
            }

            string[] tokens = itsStringCSV.Split(',');
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                itsIds.Add(token);
            }
            return itsIds;
        }

        public List<int> allowedIds(int hour, bool after30)
        {
            DateTime currentTimeUTC = DateTime.UtcNow; // current time in UTC

            List<venue> venues = _context
                .venue.Where(x => x.ActiveStatus == true && x.qismId > 0)
                .AsNoTracking()
                .ToList();

            venues = venues.Where(x => !string.IsNullOrEmpty(x.CampId)).ToList();
            List<int> allowedvid = new List<int>();
            List<khidmat_guzaar> kgs = new List<khidmat_guzaar>();

            foreach (var m in venues)
            {
                TimeZoneInfo venueTimeZone = TimeZoneInfo.FindSystemTimeZoneById(m.CampId);
                DateTime venueLocalTime = TimeZoneInfo.ConvertTimeFromUtc(
                    currentTimeUTC,
                    venueTimeZone
                );

                if (
                    venueLocalTime.Hour == hour
                    && venueLocalTime.Minute >= (after30 ? 30 : 0)
                    && venueLocalTime.Minute < (after30 ? 59 : 30)
                )
                {
                    allowedvid.Add(m.Id);
                }
            }

            return allowedvid;
        }

        public DateTime? GetFromDate(mzlm_leave_application src)
        {
            var application = src;
            if (application == null)
                return null; // or some other default value

            try
            {
                return new DateTime(
                    application.fromYear,
                    application.fromMonthId,
                    application.fromDayId
                );
            }
            catch
            {
                return null; // handle invalid date components
            }
        }

        public DateTime? GetToDate(mzlm_leave_application src)
        {
            var application = src;
            if (application == null)
                return null; // or some other default value

            try
            {
                return new DateTime(application.toYear, application.toMonthId, application.toDayId);
            }
            catch
            {
                return null; // handle invalid date components
            }
        }

        public DateTime? GetFromDate(mzlm_leave_package src)
        {
            var application = src.mzlm_leave_application.FirstOrDefault();
            if (application == null)
                return null; // or some other default value

            try
            {
                return new DateTime(
                    application.fromYear,
                    application.fromMonthId,
                    application.fromDayId
                );
            }
            catch
            {
                return null; // handle invalid date components
            }
        }

        public DateTime? GetToDate(mzlm_leave_package src)
        {
            var application = src.mzlm_leave_application.FirstOrDefault();
            if (application == null)
                return null; // or some other default value

            try
            {
                return new DateTime(application.toYear, application.toMonthId, application.toDayId);
            }
            catch
            {
                return null; // handle invalid date components
            }
        }

        public int remaingDays(DateTime src)
        {
            TimeSpan difference = src - DateTime.UtcNow;
            return difference.Days;
        }

        public DateTime getEngDate(CalenderModel HijriDate)
        {
            if (_context == null)
            {
                throw new Exception("Context not defined and required by the helper service");
            }
            hijri_calender h = _context
                .hijri_calender.Where(x =>
                    x.hijri_day == HijriDate.hijDay
                    && x.hijri_month == HijriDate.hijMonth
                    && x.hijri_year == HijriDate.hijYear
                )
                .AsNoTracking()
                .FirstOrDefault();

            DateTime d = new DateTime();
            if (h != null)
            {
                d = new DateTime(h.english_year ?? 0, h.english_month ?? 0, h.english_day ?? 0);
            }
            return d;
        }

        public string retrieveBase64FromBucket(string objectKey)
        {
            string bucketName = "mz-mahadalzahra-org";

            using (var s3Client = new AmazonS3Client(accessKey, secretKey, region))
            {
                // Create a GetObjectRequest
                var request = new GetObjectRequest { BucketName = bucketName, Key = objectKey };

                // Download the object from the bucket
                using (var response = s3Client.GetObjectAsync(request).Result)
                {
                    using (var responseStream = response.ResponseStream)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            // Copy the response stream to a memory stream
                            responseStream.CopyTo(memoryStream);
                            memoryStream.Position = 0;

                            // Convert the file content to base64
                            byte[] fileBytes = memoryStream.ToArray();
                            string base64 = Convert.ToBase64String(fileBytes);

                            return base64;
                        }
                    }
                }
            }
        }

        public string stringToColorCode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.");
            }

            // Use MD5 hash to get a 128-bit hash of the string
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Adjust each byte to ensure the color is not too dark or dull
                for (int i = 0; i < 3; i++)
                {
                    // Ensure each color component is at least 80 (128 for more brightness) on a scale of 0-255
                    hashBytes[i] = (byte)(hashBytes[i] % 128 + 80);
                }

                // Use the first 3 bytes of the hash to create a color code
                string color = $"{hashBytes[0]:X2}{hashBytes[1]:X2}{hashBytes[2]:X2}";

                return color;
            }
        }

        public string stringToColorCode2(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.");
            }

            // Use MD5 hash to get a 128-bit hash of the string
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Use hash bytes to seed a pseudo-random number generator
                var random = new Random(BitConverter.ToInt32(hashBytes, 0));

                // Generate hue between 0.3 to 0.4 (convert to degrees)
                double hue = 0.3 + (0.4 - 0.3) * random.NextDouble(); // Scale to 0-360 if needed
                double saturation = 0.5 + 0.5 * random.NextDouble(); // Assuming saturation range is 0-1
                double lightness = 0.5 + 0.5 * random.NextDouble(); // Assuming lightness range is 0-1

                // Convert HSL to RGB
                var color = HslToRgb(hue, saturation, lightness);

                // Convert RGB to Hex
                string colorCode = $"{color.r:X2}{color.g:X2}{color.b:X2}";

                return colorCode;
            }
        }

        public struct rgbStruct
        {
            public double r { get; set; }
            public double g { get; set; }
            public double b { get; set; }
        }

        private rgbStruct HslToRgb(double h, double s, double l)
        {
            double r = 0,
                g = 0,
                b = 0;

            if (s == 0)
            {
                r = g = b = l; // Achromatic color (gray scale)
            }
            else
            {
                Func<double, double, double, double> hueToRgb = (p, q, t) =>
                {
                    if (t < 0)
                        t += 1;
                    if (t > 1)
                        t -= 1;
                    if (t < 1.0 / 6)
                        return p + (q - p) * 6 * t;
                    if (t < 1.0 / 2)
                        return q;
                    if (t < 2.0 / 3)
                        return p + (q - p) * (2.0 / 3 - t) * 6;
                    return p;
                };

                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;
                r = hueToRgb(p, q, h + 1.0 / 3);
                g = hueToRgb(p, q, h);
                b = hueToRgb(p, q, h - 1.0 / 3);
            }

            // Convert RGB values from 0-1 range to 0-255 range
            return new rgbStruct
            {
                r = (int)(r * 255),
                g = (int)(g * 255),
                b = (int)(b * 255)
            };
        }

        public CalenderModel getAcedemicYear(DateTime engDate)
        {
            if (_context == null)
            {
                throw new Exception("Context not defined and required by the helper service");
            }

            List<AcedemicYearDataModel> MailModel = new List<AcedemicYearDataModel>();

            CalenderModel hdate = getHijriDate(engDate);

            //List<acedemicyear_data> a = _context.acedemicyear_data.Where(x => (x.acedemicYear == hdate.hijYear && x.frommonth_hijri <= hdate.hijMonth || x.acedemicYear == hdate.hijYear + 1 && x.tomonth_hijri >= hdate.hijMonth) ).AsNoTracking().ToList();
            //foreach (var i in a)
            //{
            //    DateTime from = getEngDate(new CalenderModel { hijDay = i.fromday_hijri ?? 0, hijMonth = i.frommonth_hijri ?? 0, hijYear = i.fromyear_hijri ?? 0 });
            //    DateTime to = getEngDate(new CalenderModel { hijDay = i.today_hijri ?? 0, hijMonth = i.tomonth_hijri ?? 0, hijYear = i.toyear_hijri ?? 0 });
            //    AcedemicYearDataModel Amodel = new AcedemicYearDataModel { fromDate = from, toDate = to, acedemicYear = i.acedemicYear, acedemicName = i.acedemicName };
            //    MailModel.Add(Amodel);
            //}


            //AcedemicYearDataModel m = MailModel.Where(x => x.fromDate <= engDate && x.toDate >= engDate).FirstOrDefault();

            //model.acedemicYear = m?.acedemicYear ?? 1444;
            //model.acedemicYearName = m?.acedemicName;

            if (hdate.hijMonth <= 7)
            {
                hdate.acedemicYear = hdate.hijYear;
            }
            else if (hdate.hijMonth >= 8)
            {
                hdate.acedemicYear = hdate.hijYear + 1;
            }

            return hdate;
        }

        public string getHijriMonthName(int monthId)
        {
            return _context
                    .hijri_months.Where(x => x.id == monthId)
                    .FirstOrDefault()
                    ?.hijriMonthName ?? "";
        }

        public async Task<string> UploadFileToS3(
            Stream file,
            string fileName,
            string folderPath = "Misc",
            string bName = null
        )
        {
            if (file == null || file.Length == 0)
                return "No file provided.";
            if (bName != null)
            {
                bucketName = bName;
            }
            var keyName = $"{folderPath}/{Guid.NewGuid()}_{fileName}";
            var client = new AmazonS3Client(accessKey, secretKey, region);

            string mimeType = new MimeTypeMap().GetMimeType(Path.GetExtension(fileName));
            mimeType = string.IsNullOrEmpty(mimeType) ? "application/unknown" : mimeType;

            var uploadRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                InputStream = file,
                ContentType = mimeType
            };

            var response = await client.PutObjectAsync(uploadRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return $"https://{bucketName}.s3.amazonaws.com/{keyName}";
            }
            else
            {
                return "Error uploading file.";
            }
        }

        public async Task<string> retriveAndZipFromBucket(List<string> objectKeys, string fileName)
        {
            string bucketName = "mz-mahadalzahra-org";
            string path = @"C:\inetpub\wwwroot\uploads\ZipFiles\" + fileName + ".zip";

            string returnpath = "https://mahadalzahra.org/uploads/ZipFiles/" + fileName + ".zip";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = File.Create(path))
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(fs))
                {
                    using (
                        AmazonS3Client s3Client = new AmazonS3Client(accessKey, secretKey, region)
                    )
                    {
                        foreach (string objectKey in objectKeys)
                        {
                            try
                            {
                                // log.Info($"Attempting to fetch object: {objectKey} from bucket: {bucketName}");

                                var request = new GetObjectRequest
                                {
                                    BucketName = bucketName,
                                    Key = objectKey
                                };

                                using (
                                    GetObjectResponse response = await s3Client.GetObjectAsync(
                                        request
                                    )
                                )
                                {
                                    using (Stream responseStream = response.ResponseStream)
                                    {
                                        using (MemoryStream memoryStream = new MemoryStream())
                                        {
                                            // Copy the response stream to a memory stream
                                            responseStream.CopyTo(memoryStream);

                                            byte[] fileBytes = memoryStream.ToArray();
                                            ZipEntry fileEntry = new ZipEntry(objectKey)
                                            {
                                                Size = fileBytes.Length
                                            };

                                            zipStream.PutNextEntry(fileEntry);
                                            zipStream.Write(fileBytes, 0, fileBytes.Length);
                                            // log.Info($"Successfully added object: {objectKey} to ZIP");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // log.Error($"Error processing object: {objectKey}. Exception: {ex.Message}");
                            }
                        }
                    }
                    zipStream.Flush();
                    zipStream.Close();
                    // log.Info("ZIP creation completed.");
                }
            }
            return returnpath;
        }

        public async Task<string> saveBase64ToBucket(string objectKey, string base64String)
        {
            try
            {
                string bucketName = "mz-mahadalzahra-org";
                AmazonS3Client s3Client = new AmazonS3Client(accessKey, secretKey, region);

                // Decode the Base64 string to byte array
                byte[] fileBytes = Convert.FromBase64String(base64String);

                // Create a memory stream from the byte array
                using (MemoryStream memoryStream = new MemoryStream(fileBytes))
                {
                    // Create the PutObject request
                    PutObjectRequest request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey,
                        InputStream = memoryStream
                    };

                    // Upload the file to S3 bucket
                    PutObjectResponse response = await s3Client.PutObjectAsync(request);

                    // Check if the file was successfully uploaded
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("File uploaded successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to upload the file.");
                    }
                }
                return $"https://{bucketName}.s3.amazonaws.com/{objectKey}";
            }
            catch (Exception e)
            {
                return e.ToString() + " : " + objectKey + " : " + base64String;
            }
        }

        public async Task<string> ImageToS3(string path, string bucketName)
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

                using (System.Drawing.Image image = System.Drawing.Image.FromFile(fullPath))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        image.Save(stream, image.RawFormat);
                        stream.Position = 0; // Reset the position of MemoryStream to the beginning after saving.

                        string fileName = Path.GetFileName(fullPath);
                        string uploadedUrl = await UploadFileToS3(
                            stream,
                            fileName,
                            "Misc",
                            bucketName
                        );

                        return uploadedUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, S3 upload error)
                // You might want to log this exception
                return $"Error: {ex.Message}";
            }
        }

        public CalenderModel getHijriDate(DateOnly engDate)
        {
            return getHijriDate(engDate.ToDateTime(new TimeOnly()));
        }

        public CalenderModel getHijriDate(DateTime engDate)
        {
            CalenderModel d = new CalenderModel();

            if (engDate == null)
            {
                return null;
            }
            hijri_calender h = _context
                .hijri_calender.Where(x =>
                    x.english_day == engDate.Day
                    && x.english_month == engDate.Month
                    && x.english_year == engDate.Year
                )
                .AsNoTracking()
                .FirstOrDefault();
            hijri_months hmn = _context
                .hijri_months.Where(x => x.id == h.hijri_month)
                .AsNoTracking()
                .FirstOrDefault();
            if (h == null)
            {
                throw new Exception("Hijri Date Not Found");
            }
            else
            {
                d.hijDay = h.hijri_day ?? 0;
                d.hijMonth = h.hijri_month ?? 0;
                d.hijYear = h.hijri_year ?? 0;
                d.hijMonthName = hmn?.hijriMonthName;
            }

            return d;
        }

        public async Task<List<edit_table_column_logs>> LogChanges<T>(
            List<T> oldDataList,
            List<T> newDataList,
            int editedBy,
            string tablePrimaryKeyValue
        )
        {
            var changes = new List<edit_table_column_logs>();
            DateTime now = DateTime.UtcNow;

            int minCount = Math.Min(oldDataList.Count, newDataList.Count);
            int maxLength = 65535;

            for (int i = 0; i < minCount; i++)
            {
                T oldData = oldDataList[i];
                T newData = newDataList[i];

                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    if (property.GetGetMethod().IsVirtual)
                    {
                        continue;
                    }

                    object oldValue = oldData != null ? property.GetValue(oldData) : null;
                    object newValue = newData != null ? property.GetValue(newData) : null;

                    string oldStringValue = oldValue?.ToString();
                    string newStringValue = newValue?.ToString();

                    // Truncate if exceeds maxLength
                    if (oldStringValue?.Length > maxLength)
                    {
                        oldStringValue = oldStringValue.Substring(0, maxLength);
                    }
                    if (newStringValue?.Length > maxLength)
                    {
                        newStringValue = newStringValue.Substring(0, maxLength);
                    }

                    if (
                        (oldValue != null && !oldValue.Equals(newValue))
                        || (newValue != null && !newValue.Equals(oldValue))
                    )
                    {
                        edit_table_column_logs change = new edit_table_column_logs
                        {
                            old_value = oldValue?.ToString(),
                            new_value = newValue?.ToString(),
                            edited_by = editedBy,
                            table_name = typeof(T).Name,
                            column_name = property.Name,
                            table_primary_key_value = tablePrimaryKeyValue,
                            edit_date_time = now
                        };

                        if (_context != null)
                        {
                            await _context.edit_table_column_logs.AddAsync(change);
                        }

                        changes.Add(change);
                    }
                }
            }

            if (_context != null)
            {
                await _context.SaveChangesAsync();
            }

            return changes;
        }

        public async Task<List<edit_table_column_logs>> LogChanges<T>(
            T oldData,
            T newData,
            int editedBy,
            string tablePrimaryKeyValue
        )
        {
            var changes = new List<edit_table_column_logs>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            DateTime now = DateTime.UtcNow;
            int maxLength = 65535;

            try
            {
                foreach (var property in properties)
                {
                    if (property.GetGetMethod().IsVirtual)
                    {
                        continue;
                    }

                    if (property.Name.Contains("Base64"))
                    {
                        continue;
                    }

                    object oldValue = oldData != null ? property.GetValue(oldData) : null;
                    object newValue = newData != null ? property.GetValue(newData) : null;

                    string oldStringValue = oldValue?.ToString();
                    string newStringValue = newValue?.ToString();


                    // Truncate if exceeds maxLength
                    if (oldStringValue?.Length > maxLength)
                    {
                        oldStringValue = oldStringValue.Substring(0, maxLength);
                    }
                    if (newStringValue?.Length > maxLength)
                    {
                        newStringValue = newStringValue.Substring(0, maxLength);
                    }

                    if (
                        (oldValue != null && !oldValue.Equals(newValue))
                        || (newValue != null && !newValue.Equals(oldValue))
                    )
                    {
                        edit_table_column_logs change = new edit_table_column_logs
                        {
                            old_value = oldValue?.ToString(),
                            new_value = newValue?.ToString(),
                            edited_by = editedBy,
                            table_name = typeof(T).Name,
                            column_name = property.Name,
                            table_primary_key_value = tablePrimaryKeyValue,
                            edit_date_time = now
                        };
                        if (_context != null)
                        {
                            await _context.edit_table_column_logs.AddAsync(change);
                        }

                        changes.Add(change);
                    }
                }
                if (_context != null && changes.Count > 0)
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                //console log the errorMessage
            }

            return changes;
        }

        public int getWafdCurrentClass(int? fariqYear, int? farigDarajah)
        {
            int fyear = fariqYear ?? 0;

            int farigdarajah = 0;
            if (farigDarajah != null)
            {
                farigdarajah = farigDarajah ?? 0;
            }

            int count = 0;
            if (fyear != 0)
            {
                count = (_globalConstants.currentAcademicYear - fyear) + farigdarajah;
            }
            return count;
        }

        public CalenderModel getTodayHijriDate()
        {
            CalenderModel d = new CalenderModel();

            DateTime engDate = indianTime;
            if (engDate == null)
            {
                throw new Exception("English Date is Empty");
            }

            hijri_calender h = _context
                .hijri_calender.Where(x =>
                    x.english_day == engDate.Day
                    && x.english_month == engDate.Month
                    && x.english_year == engDate.Year
                )
                .FirstOrDefault();

            if (h == null)
            {
                throw new Exception("Hijri Date Not Found");
            }
            else
            {
                d.hijDay = h.hijri_day ?? 0;
                d.hijMonth = h.hijri_month ?? 0;
                d.hijYear = h.hijri_year ?? 0;
            }

            return d;
        }

        public int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        public dynamic TransformModelForExport(object model, HashSet<string> propertiesToExclude)
        {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var prop in model.GetType().GetProperties())
            {
                if (!propertiesToExclude.Contains(prop.Name))
                {
                    var value = prop.GetValue(model);
                    result.Add(prop.Name, value);
                }
            }

            return result;
        }

        public List<dynamic> ConvertToDynamicList<T>(List<T> models)
        {
            var dynamicList = new List<dynamic>();

            foreach (var model in models)
            {
                IDictionary<string, object> expando = new ExpandoObject();

                foreach (var property in model.GetType().GetProperties())
                {
                    expando.Add(property.Name, property.GetValue(model));
                }

                dynamicList.Add(expando as ExpandoObject);
            }

            return dynamicList;
        }

        public int getMaxWaiveAmount(int allotmentId)
        {
            List<mz_student_fee_transaction> transactions = _context
                .mz_student_fee_transaction.Where(x => x.allotmentId == allotmentId)
                .ToList();

            int? D_withoutR = transactions
                .Where(x => x.paymentMode != "Reverse")
                .ToList()
                .Sum(x => x.debit);
            int? waived = transactions
                .Where(x => x.paymentMode == "Waive")
                .ToList()
                .Sum(x => x.credit);

            int? C_withoutW = transactions
                .Where(x => x.paymentMode != "Waive")
                .ToList()
                .Sum(x => x.credit);
            int? reversed = transactions
                .Where(x => x.paymentMode == "Reverse")
                .ToList()
                .Sum(x => x.debit);

            int amount =
                ((D_withoutR ?? 0) - (waived ?? 0)) - ((C_withoutW ?? 0) - (reversed ?? 0));
            return amount;
        }

        public List<string> parseMobNo(string MobNoCSV)
        {
            List<string> mobNo = new List<string>();

            string[] tokens = MobNoCSV.Split(',');
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];

                mobNo.Add(token);
            }
            return mobNo;
        }

        public async Task<string> GetBase64ImageFromUrl(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                // Download the image data from the URL
                byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                // Convert the byte array to a Base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                // Return the Base64 string prefixed with the data type and encoding
                return $"data:image/jpeg;base64,{base64String}";
            }
        }

        public async Task<string> SaveITSImage(byte[] photoByteArray, int ItsId)
        {
            if (photoByteArray != null)
            {
                try
                {
                    string bucketName = "mz-mahadalzahra-org";
                    string objectKey = $"Its_Photos/{ItsId}.jpeg";
                    AmazonS3Client s3Client = new AmazonS3Client(accessKey, secretKey, region);

                    // Create a memory stream from the byte array
                    using (MemoryStream memoryStream = new MemoryStream(photoByteArray))
                    {
                        // Create the PutObject request
                        PutObjectRequest request = new PutObjectRequest
                        {
                            BucketName = bucketName,
                            Key = objectKey,
                            InputStream = memoryStream,
                            ContentType = "image/jpeg" // Set the content type for the image
                        };

                        // Upload the file to S3 bucket
                        PutObjectResponse response = await s3Client.PutObjectAsync(request);

                        // Check if the file was successfully uploaded
                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Console.WriteLine("File uploaded successfully!");
                            // Return the file URL
                            return $"https://{bucketName}.s3.amazonaws.com/{objectKey}";
                        }
                        else
                        {
                            Console.WriteLine("Failed to upload the file.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            return null;
        }

        public int getMaxReverseAmount(int allotmentId)
        {
            List<mz_student_fee_transaction> transactions = _context
                .mz_student_fee_transaction.Where(x => x.allotmentId == allotmentId)
                .ToList();

            int? C_withoutW = transactions
                .Where(x => x.paymentMode != "Waive")
                .ToList()
                .Sum(x => x.credit);
            int? reversed = transactions
                .Where(x => x.paymentMode == "Reverse")
                .ToList()
                .Sum(x => x.debit);

            int amount = (C_withoutW ?? 0 - reversed ?? 0);
            return amount;
        }

        public MemoryStream CombinePdfStreams(Stream stream1, Stream stream2)
        {
            PdfDocument outputDocument = new PdfDocument();

            // Add stream1 to the output PDF
            using (PdfDocument inputDocument = PdfReader.Open(stream1, PdfDocumentOpenMode.Import))
            {
                foreach (PdfPage page in inputDocument.Pages)
                {
                    outputDocument.AddPage(page);
                }
            }

            // Add stream2 to the output PDF
            using (PdfDocument inputDocument = PdfReader.Open(stream2, PdfDocumentOpenMode.Import))
            {
                foreach (PdfPage page in inputDocument.Pages)
                {
                    outputDocument.AddPage(page);
                }
            }

            MemoryStream outputPdfStream = new MemoryStream();
            outputDocument.Save(outputPdfStream, false);
            outputPdfStream.Position = 0; // Reset stream position to the start
            return outputPdfStream;
        }
    }

    public class AcedemicYearDataModel
    {
        public int id { get; set; }
        public int? fromday_hijri { get; set; }
        public int? frommonth_hijri { get; set; }
        public int? fromyear_hijri { get; set; }
        public int? today_hijri { get; set; }
        public int? tomonth_hijri { get; set; }
        public int? toyear_hijri { get; set; }
        public int? acedemicYear { get; set; }
        public string acedemicName { get; set; }

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }

    public class CurrencyAmount
    {
        //ones
        private readonly string[] _ones = new string[]
        {
            "",
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten",
            "Eleven",
            "Twelve",
            "Thirteen",
            "Fourteen",
            "Fifteen",
            "Sixteen",
            "Seventeen",
            "Eighteen",
            "Nineteen"
        };

        //tens
        private readonly string[] _tens = new string[]
        {
            "",
            "",
            "Twenty",
            "Thirty",
            "Forty",
            "Fifty",
            "Sixty",
            "Seventy",
            "Eighty",
            "Ninety"
        };

        //thousand, million, billion
        private readonly string[] _billions = new string[]
        {
            "Hundred",
            "Thousand",
            "Lakhs",
            "Crore"
        };

        public string ConvertNumbertoWords(long number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKHS ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[]
                {
                    "ZERO",
                    "ONE",
                    "TWO",
                    "THREE",
                    "FOUR",
                    "FIVE",
                    "SIX",
                    "SEVEN",
                    "EIGHT",
                    "NINE",
                    "TEN",
                    "ELEVEN",
                    "TWELVE",
                    "THIRTEEN",
                    "FOURTEEN",
                    "FIFTEEN",
                    "SIXTEEN",
                    "SEVENTEEN",
                    "EIGHTEEN",
                    "NINETEEN"
                };
                var tensMap = new[]
                {
                    "ZERO",
                    "TEN",
                    "TWENTY",
                    "THIRTY",
                    "FORTY",
                    "FIFTY",
                    "SIXTY",
                    "SEVENTY",
                    "EIGHTY",
                    "NINETY"
                };
                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
    }
}
