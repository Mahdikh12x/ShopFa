namespace AccountManagement.Infrastructure.Configuration
{
    public static class Roles
    {
        public const string Administrator = "10003";
        public const string InventoryManager= "10006";
        public const string ContentManager= "10004";
        public const string User= "10005";

        public static string GetBy(long id)
        {
            return id switch
            {
                10003 => "مدیر سیستم",
                10006 => "انباردار",
                10004 => "محتواگذار",
                _ => ""
            };
        }

    }
}
