using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MultimediaAPI.Contexts;
using MultimediaAPI.Services;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;

namespace MultimediaAPI
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
            services.AddControllers();

            ///
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<MediaBaseService>();
            services.AddSingleton<IFileInfoService, FileInfoService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IRelationService, RelationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddDbContext<MultimediaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MultimediaDbConnection")));
            services.AddControllersWithViews().AddNewtonsoftJson(options
                => options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ///
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Exception");
            }
            ///
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ///
            StaticFileOptions staticFileOptions = new()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Contents")),
                RequestPath = "/Contents"
            };
            app.UseStaticFiles(staticFileOptions);
        }
    }
}
