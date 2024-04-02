using ArchitectureSample.Application.Blazor.Client.Services;
using ArchitectureSample.Application.Blazor.Server.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents()
	.Services
	.AddScoped(_ =>
	{
		var handler = new HttpClientHandler();
		handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
		return new HttpClient(handler) { BaseAddress = new Uri(builder.Configuration["ApiHost"]!) };
	})
	.AddScoped(_ =>
	{
		var handler = new HttpClientHandler();
		handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
		return new HttpClient(handler) { BaseAddress = new Uri(builder.WebHost.GetSetting("ASPNETCORE_URLS")!.Split(';').Single(x => x.StartsWith("http://"))) };
	})
	.AddControllers()
	.Services
	.AddHealthChecks()
	.Services
	.AddSwaggerGen()
	.AddMudServices();

builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseWebAssemblyDebugging();
else
	app.UseExceptionHandler("/Error", createScopeForErrors: true).
		UseHsts();

app.UseHttpsRedirection()
	.UseStaticFiles()
	.UseAntiforgery()
	.UseSwagger()
	.UseSwaggerUI();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ArchitectureSample.Application.Blazor.Client._Imports).Assembly);

await app.RunAsync();
