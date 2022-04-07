using _0_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    internal class ShopPermissionExposure : IPermissionExposure
    {

        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new(ShopPermissions.CreateProduct, "افزودن محصول"),
                        new(ShopPermissions.EditProduct, "ویرایش محصول"),
                        new(ShopPermissions.SearchProducts, "جست و جو در محصولات"),
                        new(ShopPermissions.ShowProducts, "نمایش محصولات")
                    }
                },
                {
                    "ProductCategory",new List<PermissionDto>
                    {
                        new(ShopPermissions.CreateProductCategory, "افزودن گروه محصول"),
                        new(ShopPermissions.EditProductCategory, "ویرایش گروه محصول"),
                        new(ShopPermissions.SearchProductCategories, "جست و جو در گروه محصول"),
                        new(ShopPermissions.ShowProductCategories, "نمایش گروه محصول")
                    }
                }
            };
        }
    }
}
