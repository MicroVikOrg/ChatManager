using ChatManager.WebApi.Interfaces;
using ChatManager.WebApi.Models.Db;
using ChatManager.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Confluent.Kafka;
using ChatManager.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region kafka configuration
var kafkaConfig = new KafkaProducerConfig
{
    BootstrapServers = builder.Configuration["BootstrapServers"]!
};
builder.Services.AddSingleton(kafkaConfig);
var producerConfig = new ProducerConfig
{
    BootstrapServers = kafkaConfig.BootstrapServers
};
builder.Services.AddSingleton(producerConfig);

builder.Services.AddSingleton<IProducer<Null, string>>(_ => new ProducerBuilder<Null, string>(producerConfig).Build());
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
#endregion
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
