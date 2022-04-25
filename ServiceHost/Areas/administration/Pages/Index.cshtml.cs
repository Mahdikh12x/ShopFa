using _01_ShopFaQuery.Contracts.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.administration.Pages
{
    public class IndexModel : PageModel
    {
        public List<ChartViewModel> DataList;
        private readonly IReportQuery _reportQuery;
        public List<Chart> List;
        public ReportCountViewModel Counts;
        public IndexModel(IReportQuery reportQuery)
        {
            _reportQuery = reportQuery;
        }

        public void OnGet()
        {
            Counts = _reportQuery.GetReportCounts();
            DataList = _reportQuery.GetReports();
            List = new List<Chart>
            {
                new Chart
                {
                    Label = "مبل",
                    BackgroundColor = new List<string> { "#00FFC6", "#30AADD", "#43919B", "#247881" },
                    BorderColor = new List<string> { "#FFD124", "#00AFC1", "#0093AB", "#006778" },
                    Data = new List<int> { 4, 14, 6, 11, 16 }
                },
                new Chart
                {
                    Label = "یخچال",
                    BackgroundColor = new List<string> { "#FFD124", "#00AFC1", "#0093AB", "#006778" },
                    BorderColor = new List<string> { "#00FFC6", "#30AADD", "#43919B", "#247881" },
                    Data = new List<int> { 5, 15, 7, 17, 10 }
                },
                new Chart
                {
                    Label = "تلویزون",
                    BackgroundColor = new List<string> { "#00FFC6", "#30AADD", "#43919B", "#247881" },
                    BorderColor = new List<string> { "#FFD124", "#00AFC1", "#0093AB", "#006778" },
                    Data = new List<int> { 2, 22, 5, 1, 6 }
                },
            };
        }


    }

    public class Chart
    {
        [JsonProperty(PropertyName = "label")]
        public string? Label { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<int>? Data { get; set; }
        [JsonProperty(PropertyName = "backgroundColor")]
        public List<string>? BackgroundColor { get; set; }
        [JsonProperty(PropertyName = "borderColor")]
        public List<string>? BorderColor { get; set; }
    }
}
