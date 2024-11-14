using System.Runtime;
using Basket.API.gRPCServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
opt.Address= new Uri(builder.Configuration.GetValue<string>("gRPCSettings.DiscountgRPCUrl")));
builder.Services.AddScoped<DiscountgRPCService>();

builder.Services.AddControllers();

builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = builder.Configuration.GetConnectionString("BasketDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
