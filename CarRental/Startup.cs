using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarRental.Data;
using CarRental.Services;
using CarRental.Data.Repositories;
using Microsoft.Extensions.Configuration;
using System;

namespace CarRental
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            // Add DbContext with SQL Server for CarRental
            services.AddDbContext<CarRentalContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();

            services.AddScoped<IRepository, Repository>();

            services.AddDistributedMemoryCache();


            services.AddScoped<CustomerService>();

            services.AddScoped<EmailService>();

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));


            // Session servislerini ekledim
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            // Register repositories
            services.AddScoped(typeof(IRepository), typeof(Repository));

            // Register services
            services.AddScoped<CarService>();
            services.AddScoped<BrandService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<ReservationService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<LocationService>();
            services.AddScoped<EmailService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseCors("AllowAll"); 

            app.UseAuthentication();
            app.UseAuthorization();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CarRentalContext>();
                AdminSeeder.SeedAdmin(context);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

        }

      

    }
}
