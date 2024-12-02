using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phoenix.Identity.Entities;
using Phoenix.Identity.Services;
using Phoenix.Identity.Stores;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Infrastructure.Repositories;
using Phoenix.Web.Services;
using System;
using System.Data.SqlClient;
using Phoenix.Web.Hubs;

namespace Phoenix.Web
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
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru-RU");
            //    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
            //});

            services.Configure<RazorViewEngineOptions>(options =>
            {
                //options.AreaPageViewLocationFormats.Add("/Areas/{2}")
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSignalR();

            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            string connectionString = Configuration.GetConnectionString("PhoenixIdentity");
            services.AddTransient<SqlConnection>(e => new SqlConnection(connectionString));
            services.AddTransient<DapperUsersTable>();
            services.AddTransient<DapperRolesTable>();

            services.AddScoped<IIdentityParser<ApplicationUser>, IdentityParser>();

            // data infrastructure
            string dataConnectionString = Configuration.GetConnectionString("PhoenixDB");
            services.AddScoped<IAdminRepository>(sp => new AdminRepository(dataConnectionString));
            services.AddScoped<IElectionRepository>(sp => new ElectionRepository(dataConnectionString));
            //services.AddScoped<IAddressRepository>(sp => new AddressRepository(dataConnectionString));
            services.AddScoped<ITerritoryRepository>(sp => new TerritoryRepository(dataConnectionString));
            services.AddScoped<IPersonRepository>(sp => new PersonRepository(dataConnectionString));
            services.AddScoped<IOrganizationRepository>(sp => new OrganizationRepository(dataConnectionString));
            services.AddScoped<IEventRepository>(sp => new EventRepository(dataConnectionString));
            services.AddScoped<IPartyRepository>(sp => new PartyRepository(dataConnectionString));
            services.AddScoped<IReportRepository>(sp => new ReportRepository(dataConnectionString));
            services.AddScoped<IServiceDeskRepository>(sp => new ServiceDeskRepository(dataConnectionString));
            services.AddScoped<IHubRepository>(sp => new HubRepository(dataConnectionString));

            // for testing
            services.AddSingleton<ITestService>(sp => new TestService());

            services.AddControllersWithViews();

            services.AddMvc();

            services.AddTransient<IEmailSender, MessageServices>();
            services.AddTransient<ISmsSender, MessageServices>();

            services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseRequestLocalization();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ElectionHub>("/signalr/election",
                    options =>
                    {
                        options.LongPolling.PollTimeout = TimeSpan.FromMinutes(1);
                        options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                    });

                endpoints.MapHub<PersonHub>("/hub/person",
                    options =>
                    {
                        options.LongPolling.PollTimeout = TimeSpan.FromMinutes(1);
                        options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                    });
            });
        }
    }
}
