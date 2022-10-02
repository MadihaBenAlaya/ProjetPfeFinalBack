using AppRecrutement.Configuration;
using AppRecrutement.Models;
using AppRecrutement.Persistence;
using AppRecrutement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;
using static AppRecrutement.Services.IEmailService;

namespace AppRecrutement
{
    public class Startup
    {
        private readonly string _policyName = "Recrutement.Api";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            //Inject AppSettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppRecrutement", Version = "v1" });
            });

            //services.AddEntityFrameworkNpgsql().AddDbContext<AuthenticationContext>(opt =>
            //    opt.UseNpgsql(GetConnectionInfo.("IdentityConnection")));



            services.AddDbContext<AuthenticationContext>(options => options.UseNpgsql(GetConnectionInfo(Configuration).ToString()));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }
            );


            services.AddCors(options => options.AddPolicy(_policyName, builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                };
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppRecrutement v1"));
            }

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()

            );

           // app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }





        /// <summary>
        /// Gets the connection information.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static DbConnectionInfo GetConnectionInfo(IConfiguration configuration)
        {

            DbConnectionInfo dbConnectionInfo;

            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                dbConnectionInfo = new DbConnectionInfo
                {
                    Host = Environment.GetEnvironmentVariable("PG_HOST"),
                    Database = Environment.GetEnvironmentVariable("PG_DATABASE"),
                    Username = Environment.GetEnvironmentVariable("PG_USERNAME"),
                    Password = Environment.GetEnvironmentVariable("PG_PASSWORD")
                };
            }
            else
            {
                dbConnectionInfo = new DbConnectionInfo
                {
                    Host = configuration.GetValue<string>("DataConnection:PG_HOST"),
                    Database = configuration.GetValue<string>("DataConnection:PG_DATABASE"),
                    Username = configuration.GetValue<string>("DataConnection:PG_USERNAME"),
                    Password = configuration.GetValue<string>("DataConnection:PG_PASSWORD")
                };
            }
            return dbConnectionInfo;
        }
    }
}
