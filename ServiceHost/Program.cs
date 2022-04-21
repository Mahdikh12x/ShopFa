using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPalService;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using ShopManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("ShopfaDB");
ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.AddTransient<IAuthHelper, AuthHelper>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea",
        policyBuilder => policyBuilder.RequireRole(new List<string> { Roles.Administrator, Roles.InventoryManager, Roles.ContentManager, Roles.OrderManager}));
    
    options.AddPolicy("ShopPolicy",
policyBuilder => policyBuilder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("InventoryPolicy", 
        policyBuilder => policyBuilder.RequireRole(new List<string> { Roles.InventoryManager, Roles.Administrator,Roles.OrderManager }));
    
    options.AddPolicy("ContentPolicy", 
        policyBuilder => policyBuilder.RequireRole(new List<string> { Roles.ContentManager, Roles.Administrator }));
});

builder.Services.AddRazorPages().AddMvcOptions(opt=>opt.Filters.Add<PageFilter>()).AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeAreaFolder("administration", "/", "AdminArea");
    options.Conventions.AuthorizeAreaFolder("administration", "/Shop", "ShopPolicy");
    options.Conventions.AuthorizeAreaFolder("administration", "/Inventory", "InventoryPolicy");
    options.Conventions.AuthorizeAreaFolder("administration", "/Comment", "ContentPolicy");
    options.Conventions.AuthorizeAreaFolder("administration", "/Blog", "ContentPolicy");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
