// Author: Kaan Çembertaş 
// No: 200001684

using System;
using System.Text;
using AutoMapper;
using ImHere.API.Helpers;
using ImHere.API.Mappings;
using ImHere.API.Middlewares;
using ImHere.Business.Abstract;
using ImHere.Business.Concrete;
using ImHere.DataAccess.Abstract;
using ImHere.DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ImHere.API
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
            services.AddSwaggerDocument(settings => settings.Title = "I'm Here API");
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JWT authentication Ayarlaması
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            // Singletons
            ConfigureSingletons(services);
            services.AddSingleton(mapper);
        }

        private void ConfigureSingletons(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserLectureRepository, UserLectureRepository>();
            services.AddSingleton<ILectureRepository, LectureRepository>();
            services.AddSingleton<IAttendenceRepository, AttendenceRepository>();
            services.AddSingleton<IAttendenceService, AttendenceService>();
            services.AddSingleton<ILectureService, LectureService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IFaceInfoRepository, FaceInfoRepository>();
            services.AddSingleton<IFaceInfoService, FaceInfoService>();
            services.AddSingleton<IAttendenceImageRepository, AttendenceImageRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
