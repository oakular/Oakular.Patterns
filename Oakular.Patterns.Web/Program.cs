using Azure.Storage.Blobs;
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
builder.Services.AddAuthentication()
                .AddJwtBearer(o => 
                {

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
