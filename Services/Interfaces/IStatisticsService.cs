using Services.Viewmodels;

namespace Services.Interfaces
{
    public interface IStatisticsService
    {
        StatisticsVM GetStatistics();

        List<CountryStatisticsViewModel> GetCountryStatistics();

    }
}
