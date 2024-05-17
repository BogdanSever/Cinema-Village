namespace CinemaVillage.Services.HelperService.Interface
{
    public interface IFormatDateTimeService
    {
        string GetFormattedDate(string date);
        string GetFormattedHour(string hour);
        (int, int, int) GetSplittedDate(string date);
        (int, int, int) GetSplittedHour(string date);
    }
}
