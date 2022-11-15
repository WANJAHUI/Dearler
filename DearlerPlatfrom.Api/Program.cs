using DearlerPlatform.Service.CustomerApp;
using DearlerPlatfrom.Api.Extensions;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ServiceEnter();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.InitEnter();

app.InitMap();


app.Run();
