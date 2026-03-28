using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using System.Data;

namespace mahadalzahrawebapi.Services
{

    public interface IHijriCalenderService
    {

        CalenderModel getHijriDate(DateTime engDate);

        DateTime getEngDate(CalenderModel HijriDate);

    }

    public class HijriCalenderService : IHijriCalenderService
    {

        public globalConstants _globalConstants;
        public mzdbContext _context;

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        public HijriCalenderService(mzdbContext context)
        {
            _context = context;
            _globalConstants = new globalConstants();
        }

        public HijriCalenderService()
        {
            _context = null;
            _globalConstants = new globalConstants();
        }

        public DateTime getEngDate(CalenderModel HijriDate)
        {


            hijri_calender h = _context.hijri_calender.Where(x => x.hijri_day == HijriDate.hijDay && x.hijri_month == HijriDate.hijMonth && x.hijri_year == HijriDate.hijYear).FirstOrDefault();


            DateTime d = new DateTime();
            if (h != null)
            {
                d = new DateTime(h.english_year ?? 0, h.english_month ?? 0, h.english_day ?? 0);
            }


            return d;




        }

        public CalenderModel getHijriDate(DateTime engDate)
        {
            CalenderModel d = new CalenderModel();

            if (engDate == null)
            {
                return null;
            }

            hijri_calender h = _context.hijri_calender.Where(x => x.english_day == engDate.Day && x.english_month == engDate.Month && x.english_year == engDate.Year).FirstOrDefault();
            hijri_months hmn = _context.hijri_months.Where(x => x.id == h.hijri_month).FirstOrDefault();
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

        public CalenderModel getTodayHijriDate()
        {
            CalenderModel d = new CalenderModel();

            DateTime engDate = indianTime;
            if (engDate == null)
            {
                throw new Exception("English Date is Empty");
            }

            hijri_calender h = _context.hijri_calender.Where(x => x.english_day == engDate.Day && x.english_month == engDate.Month && x.english_year == engDate.Year).FirstOrDefault();

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

        public CalenderModel MinusHijriMonth(CalenderModel date, int diff)
        {
            int month1 = date.hijMonth - diff;
            int year = date.hijYear;

            while (month1 <= 0)
            {
                month1 = 12 + month1;
                year = year - 1;
            }

            var data = new CalenderModel { hijMonth = month1, hijYear = year };


            return data;

        }

        public List<CalenderModel> getHijriDateRange(CalenderModel FromHijriDate, CalenderModel ToHijriDate)
        {
            CalenderModel d = new CalenderModel();

            DateTime engDate = indianTime;
            if (engDate == null)
            {
                throw new Exception("English Date is Empty");
            }

            hijri_calender from = _context.hijri_calender.Where(x => x.hijri_day == FromHijriDate.hijDay && x.hijri_month == FromHijriDate.hijMonth && x.hijri_year == FromHijriDate.hijYear).FirstOrDefault();
            hijri_calender to = _context.hijri_calender.Where(x => x.hijri_day == ToHijriDate.hijDay && x.hijri_month == ToHijriDate.hijMonth && x.hijri_year == ToHijriDate.hijYear).FirstOrDefault();

            List<hijri_calender> dates = _context.hijri_calender.Where(x => x.id >= from.id && x.id <= to.id).ToList();
            List<CalenderModel> dateModels = new List<CalenderModel>();
            foreach (var i in dates)
            {
                dateModels.Add(new CalenderModel { engDay = i.english_day ?? 0, engMonth = i.english_month ?? 0, engYear = i.english_year ?? 0, hijDay = i.hijri_day ?? 0, hijMonth = i.hijri_month ?? 0, hijYear = i.hijri_year ?? 0 });
            }


            return dateModels;




        }



        public CalenderModel gregorianDateForStartOfCurrentHijriMonth(DateTime todayDate)
        {
            string api = "api/utility/gregoriandateForcurrenthijrimonth";
            //Add_ApiLogs(api);

            CalenderModel dateModel = getHijriDate(todayDate);
            dateModel.hijDay = 1;
            dateModel.engDate = getEngDate(dateModel);
            return dateModel;
        }

        public CalenderModel gregorianDateForEndOfCurrentHijriMonth(DateTime todayDate)
        {
            string api = "api/utility/gregoriandateForcurrenthijrimonth";
            //Add_ApiLogs(api);

            CalenderModel dateModel = getHijriDate(todayDate);
            dateModel.hijDay = 30;
            dateModel.engDate = getEngDate(dateModel);
            if (dateModel.engDate != DateTime.MinValue)
            {
                return dateModel;
            }
            dateModel.hijDay = 29;
            dateModel.engDate = getEngDate(dateModel);
            return dateModel;
        }

        public CalenderModel getAcedemicYear(DateTime engDate)
        {
            CalenderModel model = new CalenderModel();
            List<AcedemicYearDataModel> MailModel = new List<AcedemicYearDataModel>();

            List<acedemicyear_data> a = _context.acedemicyear_data.ToList();
            foreach (var i in a)
            {
                DateTime from = getEngDate(new CalenderModel { hijDay = i.fromday_hijri ?? 0, hijMonth = i.frommonth_hijri ?? 0, hijYear = i.fromyear_hijri ?? 0 });
                DateTime to = getEngDate(new CalenderModel { hijDay = i.today_hijri ?? 0, hijMonth = i.tomonth_hijri ?? 0, hijYear = i.toyear_hijri ?? 0 });
                AcedemicYearDataModel Amodel = new AcedemicYearDataModel { fromDate = from, toDate = to, acedemicYear = i.acedemicYear, acedemicName = i.acedemicName };
                MailModel.Add(Amodel);
            }


            AcedemicYearDataModel m = MailModel.Where(x => x.fromDate <= engDate && x.toDate >= engDate).FirstOrDefault();

            model.acedemicYear = m?.acedemicYear ?? 1444;
            model.acedemicYearName = m?.acedemicName;

            return model;
        }

    }
}
