using Duende.IdentityServer.Services;
using GShop.IdentityServer.Configuration;
using GShop.IdentityServer.Data;
using GShop.IdentityServer.Initializer;
using GShop.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var stringConn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseMySql(stringConn,
                            ServerVersion.AutoDetect(stringConn)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
var builderIdentityServer = builder.Services.AddIdentityServer(options =>
                                            {
                                                options.Events.RaiseErrorEvents = true;
                                                options.Events.RaiseInformationEvents = true;
                                                options.Events.RaiseFailureEvents = true;
                                                options.Events.RaiseSuccessEvents = true;
                                                options.EmitStaticAudienceClaim = true;

                                            })
                                            .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                                            .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                                            .AddInMemoryClients(IdentityConfiguration.Clients)
                                            .AddAspNetIdentity<ApplicationUser>();

builderIdentityServer.AddDeveloperSigningCredential();

builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddScoped<IProfileService, ProfileAppService>();

var app = builder.Build();

var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDatabaseInitializer>();

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
app.UseIdentityServer();
app.UseAuthorization();

initializer.Initialize();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
