using System;
using System.Linq;
using System.Text;
using Business.BusinessModel;
using Business.Services;
using EFCoreCommon.Model;
using medAppointments.OperationFilter;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Repository.Repositories;

namespace medAppointments
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<MedAppointmentsContext>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientServices, PatientServices>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentServices, AppointmentServices>();
            services.AddScoped<IAppointmentTypeRepository, AppointmentTypeRepository>();
            services.AddScoped<IAppointmentTypeServices, AppointmentTypeServices>();


            var myDb1ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MedAppointmentsContext>(options =>
                options.UseSqlServer(myDb1ConnectionString)
                    .EnableSensitiveDataLogging());





            var jwtSection = Configuration.GetSection("jwt");
            var jwtOptions = new JwtOptions();
            jwtSection.Bind(jwtOptions);
            services.Configure<JwtOptions>(jwtSection);
            var secretKey = jwtOptions.SecretKey;
            var key = Encoding.ASCII.GetBytes(secretKey);
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
                        ValidateLifetime = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });




            services.ConfigureSwaggerGen(options =>
            {
                // Set the comments path for the Swagger JSON and UI.

                var basePath = AppContext.BaseDirectory;
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                
                options.OperationFilter<ApiParameters>();

            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Med WebAPI",
                    Version = "v1",
                    TermsOfService = new Uri("http://tempuri.org/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Leonardo Sanchez",
                        Email = "leonardosanchez4@hotmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Asp Net Core 3.1",
                        Url = new Uri("http://www.doc4us.com.co")
                    }

                });

                c.CustomSchemaIds(x => x.FullName);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                        }
            });
            });

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddOData();

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

            });



            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();

                    });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "med API V1");
                c.RoutePrefix = string.Empty;
            });
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Select().Filter().OrderBy().Count().Expand().MaxTop(10);
                endpoints.EnableDependencyInjection();
                
            });
        }
    }
}
