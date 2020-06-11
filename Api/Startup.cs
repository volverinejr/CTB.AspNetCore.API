using System.Linq;
using Domain.Modulos.Infracao;
using Domain.Modulos.Infracao.Grupo;
using Domain.Modulos.Infracao.Infracao;
using Domain.Modulos.Infracao.Natureza;
using Domain.Modulos.TaxaSelic;
using Infra.Contexts;
using Infra.Repository;
using Infracao;
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

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddResponseCompression(
                options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
                }
            );


            services.AddMemoryCache();
            
            /*
                        services.AddResponseCaching(
                            options =>
                            {
                                options.MaximumBodySize = 2048;
                                options.UseCaseSensitivePaths = true;
                            }
                        );
            */

            services.AddControllers();

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            //            services.AddDbContext<DataContext>(opt => opt.UseSqlServer( Configuration.GetConnectionString("connectionString") ));




            services.AddScoped<ITaxaSelicRepository, TaxaSelicRepository>();
            services.AddScoped<TaxaSelicService, TaxaSelicService>();

            services.AddScoped<INaturezaRepository, NaturezaRepository>();
            services.AddScoped<NaturezaService, NaturezaService>();

            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<GrupoService, GrupoService>();

            services.AddScoped<IInfracaoRepository, InfracaoRepository>();
            services.AddScoped<InfracaoService, InfracaoService>();



            services.AddSwaggerGen(
                c => {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title="Documentando a API", Version = "v1" } );
                }
            );
        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(
                c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documentacao API V1");
                }
            );

            app.UseRouting();

            app.UseCors(
                x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
