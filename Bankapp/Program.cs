using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Services;

namespace Bankapp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add application services
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            // Configure EF Core
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BankAppDataContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Configure Identity with roles
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BankAppDataContext>();

            // Razor Pages
            builder.Services.AddRazorPages();

            // Data initializer
            builder.Services.AddTransient<DataInitializer>();

            var app = builder.Build();

            // Apply migrations
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BankAppDataContext>();
                if (dbContext.Database.IsRelational())
                {
                    dbContext.Database.Migrate();
                }
            }

            // Seed roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Cashier" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            // Seed application data
            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
                initializer.SeedData();
            }

            // Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            await app.RunAsync();
        }
    }
}
