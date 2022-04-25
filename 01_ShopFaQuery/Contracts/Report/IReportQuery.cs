namespace _01_ShopFaQuery.Contracts.Report;

public interface IReportQuery
{
    List<ChartViewModel> GetReports();

    ReportCountViewModel GetReportCounts();
}