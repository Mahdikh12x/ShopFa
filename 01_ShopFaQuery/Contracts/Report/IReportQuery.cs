using System.Collections.ObjectModel;

namespace _01_ShopFaQuery.Contracts.Report;

public interface IReportQuery
{
    Collection<List<ChartViewModel>> GetReports();

    ReportCountViewModel GetReportCounts();
}