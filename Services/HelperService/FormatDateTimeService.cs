using CinemaVillage.Services.HelperService.Interface;
using System.Globalization;

namespace CinemaVillage.Services.HelperService
{
    public class FormatDateTimeService : IFormatDateTimeService
    {
        public string GetFormattedDate(string date)
        {
            string[] formats = {
                "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy", "dd MM yyyy",
                "MM-dd-yyyy", "MM/dd/yyyy", "MM.dd.yyyy", "MM dd yyyy",
                "yyyy-MM-dd", "yyyy/MM/dd", "yyyy.MM.dd", "yyyy MM dd",
                "yyyyMMdd", "ddMMyyyy", "MMddyyyy",
                "dd-MMM-yyyy", "MMM dd, yyyy",
                "dd MMMM yyyy", "MMMM dd, yyyy",
                "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd'T'HH:mm:ss",
                "M/d/yyyy"
            };

            DateTime parsedDate;

            if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                string formattedDate = parsedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                return formattedDate;
            }
            else
            {
                return null;
            }
        }

        public string GetFormattedHour(string hour)
        {
            string[] formats = {
            "HH:mm", "hh:mm tt", "HH:mm:ss", "hh:mm:ss tt",
            "HH:mm:ss.fff", "hh:mm:ss.fff tt", "HH:mm:ssZ", "HH:mm:sszzz",
            "h:mm tt", "H:mm"
            };

            DateTime parsedTime;

            if (DateTime.TryParseExact(hour, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTime))
            {
                string formattedTime = parsedTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
                return formattedTime;
            }
            else
            {
                return null;
            }
        }

        public (int, int, int) GetSplittedDate(string date)
        {
            var splittedDate = date.Split('/');
            var day = Int32.Parse(splittedDate[0]);
            var month = Int32.Parse(splittedDate[1]);
            var year = Int32.Parse(splittedDate[2]);
            return (day, month, year);
        }

        public (int, int, int) GetSplittedHour(string hourParam)
        {
            var splittedHour = hourParam.Split(":");
            var hour = Int32.Parse(splittedHour[0]);
            var minute = Int32.Parse(splittedHour[1]);
            var seconds = Int32.Parse(splittedHour[2]);
            return (hour, minute, seconds);
        }
    }
}
