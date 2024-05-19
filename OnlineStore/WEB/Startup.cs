using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using Contracts.DataAccess;
using Contracts.DataAccess.SpecificRepos;
using DataAccess;
using Contracts.Services;
using Services;
using Model;
using EntityMap;
using Utils;
using WEB.Models;

namespace WEB
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
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });
            services.AddDataProtection()
                .SetApplicationName("tech-shop")
                .PersistKeysToFileSystem(new DirectoryInfo(@"../KeyStore/var/"))
                .ProtectKeysWithDpapi(true);
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                var emailSettings = new EmailSettings();
                Configuration.GetSection("SmtpSettings").Bind(emailSettings);
                return emailSettings;
            }).SingleInstance();
            builder.RegisterType<EmailSender>().As<IEmailSender>().SingleInstance();
            builder.RegisterType<ConfirmationSender>().As<IConfirmationSender>().SingleInstance();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<CategoriesService>().As<ICategoriesService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductsService>().As<IProductsService>().InstancePerLifetimeScope();
            builder.RegisterType<ReviewsService>().As<IReviewsService>().InstancePerLifetimeScope();
            builder.RegisterType<ImagesService>().As<IImagesService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoriesRepository>().As<ICategoriesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ReviewsRepository>().As<IReviewsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ImagesRepository>().As<IImagesRepository>().InstancePerLifetimeScope();
            builder.RegisterModule(new AutoMappingModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IWebHostEnvironment env,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller}/{action=Index}/{id?}");
                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            if (!roleManager.RoleExistsAsync(UserRolesConstants.AdminRole).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(UserRolesConstants.AdminRole)).GetAwaiter().GetResult();
            }
            if (userManager.FindByNameAsync(AdminUserConstants.AdminEmail).GetAwaiter().GetResult() == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = AdminUserConstants.AdminEmail,
                    Email = AdminUserConstants.AdminEmail,
                    EmailConfirmed = true
                };
                userManager.CreateAsync(user, AdminUserConstants.AdminPassword).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, UserRolesConstants.AdminRole).GetAwaiter().GetResult();
            }
        }
    }
}
