using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Services;

namespace SoftwareEngineeringProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // runs the migration scripts which create the database automatically on setup or when changes are made
            var context = serviceProvider.GetService<ApplicationDbContext>();



            context.Database.Migrate();
            DbInitializer.Initialize(context);

            // seeds the data
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            SeedData(userManager, roleManager).Wait();
            // runs the migration scripts which create the database automatically on setup or when changes are made
            //context.Database.Migrate();
            
            SeedData(userManager, roleManager).Wait();
        }

        // Creates data that can be used by the sites on creation, default users, rooms, etc...
        public async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // creating all of the roles that users can have
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "admin"
                });
            }

            if (!await roleManager.RoleExistsAsync("instructor"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "instructor"
                });
            }

            if (!await roleManager.RoleExistsAsync("security"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "security"
                });
            }

            if (!await roleManager.RoleExistsAsync("locksmith"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "locksmith"
                });
            }

            // applies the role of admin to the user with account
            // successfully applies the role to the created account, doesn't seem to add roles to the previous admin account created above
            // var user = await userManager.FindByEmailAsync("myles52@live.ca");
            // if (!await userManager.IsInRoleAsync(user, "admin"))
            // {
            //    await userManager.AddToRoleAsync(user, "admin");
            // }

        }
    }
}