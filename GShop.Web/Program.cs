using GShop.Web.Services;
using GShop.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ProductApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductApi"]);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";

                })
                .AddCookie("Cookies", c =>
                {
                    c.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    c.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToAccessDenied = (context) =>
                        {
                            context.HttpContext.Response.Redirect(builder.Configuration["ServiceUrls:IdentityServer"] + "/Account/AccessDenied");
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Events.OnRemoteFailure = context =>
                    {
                        context.Response.Redirect("/");
                        context.HandleResponse();

                        return Task.FromResult(0);
                    };

                    options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClientId = "gshop";
                    options.ClientSecret = "my_super_secret";
                    options.ResponseType = "code";
                    options.ClaimActions.MapJsonKey("role", "role", "role");
                    options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.RoleClaimType = "role";
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("gshop");
                    options.SaveTokens = true;
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
