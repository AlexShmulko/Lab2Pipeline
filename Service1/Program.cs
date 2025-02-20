using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/service1", () => "Hello from Service 1!");
app.MapGet("/service1/add", () => "Hello from add!");
app.Run("http://*:80");