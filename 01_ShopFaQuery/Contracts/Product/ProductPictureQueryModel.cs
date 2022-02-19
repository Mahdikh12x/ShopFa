namespace _01_ShopFaQuery.Contracts.Product;

public class ProductPictureQueryModel
{
    public long ProductId { get; set; }
    public string Picture { get; set; }
    public string PictureAlt { get; set; }
    public string PictureTitle { get; set; }
}