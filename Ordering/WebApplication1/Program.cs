using System.Reflection;
using EventBus.Message.Common;
using MassTransit;
using Ordering.API.EventBusConsumers;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

//RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckOutConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:RabbitMQHost"]);
        cfg.ReceiveEndpoint(EventBusList.BasketCheckOutQueue, c => 
        {
            c.ConfigureConsumer<BasketCheckOutConsumer>(ctx);
        }
        );
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
