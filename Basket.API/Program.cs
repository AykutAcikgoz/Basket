using Basket.Data;
using Basket.Data.Basket;
using Basket.Data.Product;
using Basket.Logger;
using Basket.Middleware;
using Basket.Middleware.BasketException;
using Basket.Middleware.ClientCookie;
using Basket.Service.Basket;
using Basket.Service.Product;
using Basket.Store;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.ReadFrom.Configuration(hostContext.Configuration);
});

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IBasketLogger, BasketLogger>();

builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ProductSeedData>();

builder.Services.Configure<BasketStoreSettings>(
        builder.Configuration.GetSection("RedisConfiguration"));

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IBasketRepository, BasketRepository>();
builder.Services.AddSingleton<IBasketStore, BasketStore>();
builder.Services.AddSingleton<IBasketService, BasketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseClientCookie();
app.UseHttpRequestLogging(builder.Configuration.GetValue<bool>("EnableRequestLogging"));
app.UseBasketException(builder.Configuration.GetValue<bool>("EnableExceptionLogging"));

try
{
    app.Services.GetRequiredService<ProductSeedData>().SeedDataAsync().Wait();
}
catch (Exception) { }


app.Run();

