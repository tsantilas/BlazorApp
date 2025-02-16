using BlazorApp.Client.IService;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpClient<ICustomerClientService, CustomerClientService>(options =>
{
    options.BaseAddress = new Uri("https://localhost:5000");
}
);

await builder.Build().RunAsync();
