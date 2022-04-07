using _0_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.Permissions
{
    internal class InventoryPermissionExposure:IPermissionExposure
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new(InventoryPermission.CreateInventory,"ایجاد انبار"),
                        new(InventoryPermission.EditInventory,"ویرایش انبار"),
                        new(InventoryPermission.SearchInventory,"جست و جو در انبار"),
                        new(InventoryPermission.OperationLogsInventory,"نمایش گزارشات انبار"),
                        new(InventoryPermission.IncreaseInventory,"اقزایش در انبار"),
                        new(InventoryPermission.DecreaseInventory,"کاهش در انبار"),
                    }
                }
            };
        }
    }
}
