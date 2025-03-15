using Microsoft.AspNetCore.DataProtection;

namespace SimpleExternalAuth;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new(Path.GetTempPath()))
            .SetApplicationName("SimpleExternalAuth");

        // Add services to the container.
        builder.Services.AddRazorPages();
        
        builder.Services.AddAuthentication("Cookie")
            .AddCookie("Cookie", o =>
            {
                o.Cookie.Name = "SimpleExternalAuthCookie";
                o.LoginPath = "/Account/Login";
                o.LogoutPath = "/Account/Logout";
                
            })
            .AddGoogle("Google", o =>
            {
                o.ClientId = builder.Configuration["GoogleAuth:ClientId"]!;
                o.ClientSecret = builder.Configuration["GoogleAuth:ClientSecret"]!;
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}