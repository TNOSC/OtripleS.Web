using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Tnosc.OtripleS.Client.Application;
using Tnosc.OtripleS.Client.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddFluentUIComponents();
builder.Services.AddApplicationServices();
builder.Services.AddBrokers();
await builder.Build().RunAsync();
