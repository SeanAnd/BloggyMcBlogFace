using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using NetCoreWithReactAndBasicAuth.Helpers;
using NetCoreWithReactAndBasicAuth.Core.Services;
using NetCoreWithReactAndBasicAuth.Data;
using NetCoreWithReactAndBasicAuth.Core.Repositories;
using NetCoreWithReactAndBasicAuth.Data.Repositories;
using Microsoft.Extensions.FileProviders;

namespace NetCoreWithReactAndBasicAuth.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            Environment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "defaultPolicy",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:44440", "http://localhost:44440", "https://localhost:3000",
                            "http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAutoMapper(typeof(Web.MapProfile), typeof(Data.MapProfile));

            services.AddMemoryCache();

            ConfigureCoreServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();

            app.UseHttpsRedirection();
            var test = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/Files"
            });
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseCors("defaultPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }

        private void ConfigureCoreServices(IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
        }
    }
}