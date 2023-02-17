var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
       .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// This is the HTTP/2 endpoint to register this app as part of the cluster endpoint
var url = builder.Configuration["Tunnel:Url"]!;
// https API response problem https://github.com/microsoft/reverse-proxy/issues/1800

builder.WebHost.UseTunnelTransport(url);

var app = builder.Build();

app.MapReverseProxy();

app.Run();
