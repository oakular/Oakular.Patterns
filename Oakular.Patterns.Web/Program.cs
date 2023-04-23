using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Oakular.Patterns.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IListingRepository, ListingRepository>(_ =>
{
    return new ListingRepository(new BlobServiceClient(builder.Configuration.GetConnectionString("default")));
});

builder.Services.AddTransient<ICompositeRepository, CompositeRepository>(_ =>
{
    return new CompositeRepository(new BlobServiceClient(builder.Configuration.GetConnectionString("default")));
});

builder.Services.AddTransient<CompositeContentRepository>(_ =>
{
    return new CompositeContentRepository(new BlobServiceClient(builder.Configuration.GetConnectionString("default")));
});

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.ExpireTimeSpan = TimeSpan.FromHours(8);
                   o.LoginPath = "/Login";
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
