using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Texnokaktus.ProgOlymp.Kernel.Consumers;
using Texnokaktus.ProgOlymp.Kernel.DataAccess;
using Texnokaktus.ProgOlymp.Kernel.Extensions;
using Texnokaktus.ProgOlymp.Kernel.Jobs;
using Texnokaktus.ProgOlymp.Kernel.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<ApplicationConsumer>(consumerConfigurator => consumerConfigurator.Retry(5, 5, 5));

    configurator.UsingRabbitMq((context, factoryConfigurator) =>
    {
        factoryConfigurator.Host(builder.Configuration.GetConnectionString("DefaultRabbitMq"));
        factoryConfigurator.ConfigureEndpoints(context);
    });
});

builder.Services
       .AddQuartz(configurator =>
        {
            var jobSettings = builder.Configuration.GetSection(nameof(JobSettings)).Get<JobSettings>()
                           ?? throw new("Invalid configuration");

            configurator.AddJob<ApplicationTransactionProcessor>(jobConfigurator => jobConfigurator.WithIdentity(nameof(ApplicationTransactionProcessor)));
            configurator.AddTrigger(triggerConfigurator => triggerConfigurator.ForJob(nameof(ApplicationTransactionProcessor))
                                                                              .WithIdentity($"{nameof(ApplicationTransactionProcessor)}-trigger")
                                                                              .WithCronSchedule(jobSettings.ApplicationTransactionProcessor.Schedule));
        })
       .AddQuartzHostedService();

builder.Services.AddDataAccess(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
