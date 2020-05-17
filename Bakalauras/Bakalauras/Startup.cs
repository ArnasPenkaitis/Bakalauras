using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Bakalauras.Modeling;
using Bakalauras.Services;
using Bakalauras.Services.Interfaces;
using Bakalauras.Shared.DataManagement;
using Bakalauras.Shared.DataManagement.BaseRepository;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;
using Bakalauras.Shared.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Bakalauras
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
            string securityKey = "this_is_my_super_long_security_key";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Bakalauras",
                    ValidAudience = "readers",
                    IssuerSigningKey = symmetricSecurityKey
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });

            var mapperConfiguration = new AutoMapper.MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new MapperConfiguration());
            });

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(x =>
                new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorageConnectionString")));

            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton(mapper);
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IVisualizationService, VisualizationService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<DbContext, DatabaseContext>();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseAuthentication();
            app.UseSignalR(routes => { routes.MapHub<PushingHub>("/pushingHub"); });
            app.UseMvc();
        }
    }
}
