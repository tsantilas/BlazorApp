using BlazorApp.Client.IService;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpClient<ICustomerClientService, CustomerClientService>();

await builder.Build().RunAsync();
