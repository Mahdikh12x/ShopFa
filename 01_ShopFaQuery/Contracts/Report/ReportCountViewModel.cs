namespace _01_ShopFaQuery.Contracts.Report
{
    public class ReportCountViewModel
    {
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
        public int ProductCount{ get; set; }
        public string OrderPayAmounts { get; set; }

        public ReportCountViewModel(int orderCount, int userCount, int productCount,string payAmount)
        {
            OrderCount = orderCount;
            UserCount = userCount;
            ProductCount = productCount;
            OrderPayAmounts=payAmount;
        }
    }
}
