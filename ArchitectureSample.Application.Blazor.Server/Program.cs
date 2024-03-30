using ArchitectureSample.Application.Blazor.Server.Components;

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
	});

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseWebAssemblyDebugging();
else
	app.UseExceptionHandler("/Error", createScopeForErrors: true).
		UseHsts();

app.UseHttpsRedirection()
	.UseStaticFiles()
	.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ArchitectureSample.Application.Blazor.Client._Imports).Assembly);

app.Run();
