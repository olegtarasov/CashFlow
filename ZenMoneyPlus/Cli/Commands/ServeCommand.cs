using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Spectre.Console.Cli;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Helpers;

namespace ZenMoneyPlus.Cli.Commands;

internal class ServeCommand : AsyncCommand
{
    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Host.UseSerilog();

        builder.Services.Configure<JsonOptions>(options =>
                                                {
                                                    options.JsonSerializerOptions.ConfigureDefaultOptions();
                                                });

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ZenContext>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        var scope = app.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetService<ZenContext>() ??
                  throw new InvalidOperationException("Failed to create context");

        await ctx.Database.MigrateAsync();

        await app.RunAsync();

        return 0;
    }
}