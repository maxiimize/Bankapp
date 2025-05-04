using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using Services.Viewmodels;
using Services.Interfaces;

namespace Bankapp.Pages;

public class IndexModel : PageModel
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsVM Statistics { get; set; }

    public IndexModel(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public void OnGet()
    {
        Statistics = _statisticsService.GetStatistics();
    }
}

