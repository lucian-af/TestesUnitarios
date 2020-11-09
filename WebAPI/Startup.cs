using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Text;
using WebAPI.Dados;

namespace WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            #region Compressao de Dados
            services.AddResponseCompression(cmp =>
            {
                cmp.Providers.Add<GzipCompressionProvider>();
                cmp.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.AddResponseCaching();
            #endregion

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            services.AddControllers();

            #region Autenticacao Basica
            var chave = Encoding.ASCII.GetBytes(Config.Segredo);
            services.AddAuthentication(aut =>
            {
                // Geralmente isso é padrão, é provável que mude se usarmos Identity Server
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion

            services.AddScoped<AutoMapperConfig, AutoMapperConfig>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(sw => sw.SwaggerDoc("v1", new OpenApiInfo { Title = "Curso 7194", Version = "v1" }));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(sw => sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso 7194 API Data Driven"));

            app.UseRouting();

            app.UseCors(crs => crs
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
