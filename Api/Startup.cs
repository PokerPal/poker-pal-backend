using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

using Application;
using Application.Cryptography;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Persistence;

namespace Api
{
    /// <summary>
    /// Configures and starts the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adds services to the container.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <exception cref="ArgumentException">
        /// Throws an ArgumentException if neither a database connection string nor the option to
        /// use an in-memory database were provided in the startup configuration.
        /// </exception>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(jsonOptions =>
                jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PokerPal",
                    Version = "v1",
                    Description = "PokerPal API",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddApplicationServices();
            services.AddCryptoServices();

            services
                .AddDatabaseContextFactory(options =>
                {
                    if (this.Configuration["USE_IN_MEMORY_DATABASE"] == "TRUE")
                    {
                        options.UseInMemoryDatabase("example_in_memory_database");
                    }
                    else if (string.IsNullOrWhiteSpace(
                        this.Configuration["DATABASE_CONNECTION_STRING"]))
                    {
                        throw new ArgumentException(
                            "USE_IN_MEMORY_DATABASE was not set and no DATABASE_CONNECTION_STRING was provided.");
                    }
                    else
                    {
                        options.UseConnectionString(
                            this.Configuration["DATABASE_CONNECTION_STRING"]);
                    }
                });
        }

        /// <summary>
        /// Called by the runtime to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The app being configured.</param>
        /// <param name="env">The current hosting environment.</param>
        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:3000"));

            app.UseRouting();

            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PokerPal API V1"));
            }

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
