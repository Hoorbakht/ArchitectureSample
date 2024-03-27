﻿using ArchitectureSample.Application.Commands;
using ArchitectureSample.Application.Queries;
using ArchitectureSample.Infrastructure.Core.Validators;
using ArchitectureSample.Infrastructure.Data;
using ArchitectureSample.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

namespace ArchitectureSample.Application.Api;

public static class Extensions
{
	public static IServiceCollection AddCoreServices(this IServiceCollection services,
	    IConfiguration config, Type apiType)
	{
		services.AddHttpContextAccessor();
		services.AddCustomMediatR(new[] { typeof(GetCustomer), typeof(CreateCustomer) });
		services.AddCustomValidators(new[] { typeof(GetCustomer), typeof(CreateCustomer) });
		services.AddControllers();
		services.AddSwagger(apiType);

		services.AddSqlServerDbContext<ArchitectureSampleContext>(
		    config.GetConnectionString("SqlServer") ?? "",
		    null,
		    svc => svc.AddRepository(typeof(Repository<>))
		    );

		return services;
	}

	public static IApplicationBuilder UseCoreApplication(this WebApplication app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
			app.UseDeveloperExceptionPage();

		app.UseRouting();
		app.MapControllers();

		return app.UseSwaggerCore();
	}

	public static IServiceCollection AddCustomMediatR(this IServiceCollection services, Type[]? types = null,
		Action<IServiceCollection>? doMoreActions = null)
	{
		services.AddHttpContextAccessor();

		services.AddMediatR(x =>
			{
				foreach (var type in types)
					x.RegisterServicesFromAssemblyContaining(type);
			})
			.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
			.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

		doMoreActions?.Invoke(services);

		return services;
	}

	public static IServiceCollection AddCustomValidators(this IServiceCollection services, Type[] types) =>
		services
			.AddFluentValidationAutoValidation()
			.Scan(scan => scan
				.FromAssembliesOf(types)
				.AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
				.AsImplementedInterfaces()
				.WithTransientLifetime());
}
