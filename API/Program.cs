using Application.DataProvider;
using gRPC_Service.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using SignalR_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddSignalR();

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddMemoryCache();

builder.WebHost.UseKestrel(opts =>
{
    opts.ListenLocalhost(7030, localOpts =>
    {
        localOpts.Protocols = HttpProtocols.Http2;
    });
    opts.ListenLocalhost(7031, localOpts =>
    {
        localOpts.Protocols = HttpProtocols.Http1;
        localOpts.UseHttps();
    });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<gRPC_ProductionService>();
app.MapHub<SignalR_ProductionHub>("/hub");

app.Run();
