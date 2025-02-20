using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/service2", () => "Hello from Service 2!");
app.MapGet("/service2/add", () => "Hello from add!");
app.Run("http://*:80");