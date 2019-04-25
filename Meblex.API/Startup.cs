using System.IO;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Meblex.API.Context;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Meblex.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            if(File.Exists(".env"))
                DotNetEnv.Env.Load();
            Configuration = configuration;
            _jwtSettings = new JWTSettings();
        }

        public IConfiguration Configuration { get; }

        private readonly JWTSettings _jwtSettings;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            services.AddTransient<JWTSettings>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJWTService, JWTService>();
            services.AddTransient<IUserService, UserService>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });

            var connectionString = "server="+ System.Environment.GetEnvironmentVariable("DATABASE_HOST") +
                                   ";userid="+ System.Environment.GetEnvironmentVariable("DATABASE_USER")+
                                    ";password="+ System.Environment.GetEnvironmentVariable("DATABASE_PASSWORD")+
                                    ";database="+ System.Environment.GetEnvironmentVariable("DATABASE_NAME");
            services.AddDbContext<MeblexDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseMySql(connectionString);

            });

            

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters =
                        _jwtSettings.GetTokenValidationParameters(_jwtSettings.AccessTokenSecret);
                });


            services.AddAutoMapper(cfg => cfg.ValidateInlineMaps = false);


            ValidatorOptions.PropertyNameResolver = (type, info, arg3) => info.Name.ToLower();
            ValidatorOptions.CascadeMode = CascadeMode.Continue;
            ValidatorOptions.LanguageManager.Enabled = true;
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.LocalizationEnabled = true;

                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;

                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();


                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "Meblex API", Version = "v1" });
                c.AddFluentValidationRules();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MeblexDbContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc().UseSwagger();
            app.UseReDoc(r =>
            {
                r.RoutePrefix = "redoc";
                r.SpecUrl = "/swagger/v1/swagger.json";
                r.DocumentTitle = "Meblex API";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meblex API");
            });


        }
    }
}
