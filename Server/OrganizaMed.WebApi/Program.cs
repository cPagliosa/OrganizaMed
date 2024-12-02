
using Med.dominio.Compartilhado;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using Med.Infra.Orm.Compartinhado;
using Med.Infra.Orm.ModuloAtividade;
using Med.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using OrganizaMed.Aplicacao.ModuloAtividade;
using OrganizaMed.Aplicacao.ModuloMedico;
using OrganizaMed.WebApi.Config;
using OrganizaMed.WebApi.Config.Mapping;
using static OrganizaMed.WebApi.Config.AtividadeProfile;

namespace OrganizaMed.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string politicaCorsPersonalizada = "_politicaCorsPersonalizada";

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            builder.Services.AddDbContext<IContextoPersistencia, MedDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString, dbOptions => dbOptions.EnableRetryOnFailure());
            });

            builder.Services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
            builder.Services.AddScoped<ServicoMedico>();

            builder.Services.AddScoped<IRepositorioAtividade, RepositorioAtividedeOrm>();
            builder.Services.AddScoped<ServicoAtividade>();

            builder.Services.AddScoped<FormsAtividadeMappingAction>();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<MedicoProfile>();
                config.AddProfile<AtividadeProfile>();
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: politicaCorsPersonalizada, policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            //Migrações
            {
                using var scope = app.Services.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<IContextoPersistencia>();

                if (dbContext is MedDbContext medDbContext)
                {
                    MigradorBD.AtualizarBancoDados(medDbContext);
                }
            }

            app.UseHttpsRedirection();

            app.UseCors(politicaCorsPersonalizada);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
