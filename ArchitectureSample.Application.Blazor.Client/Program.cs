using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped<ICustomerService, CustomerService>();

await builder.Build().RunAsync();