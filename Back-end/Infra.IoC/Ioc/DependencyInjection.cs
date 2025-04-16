using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;


using Infra.Contexto;
using Domain.Interfaces;
using Infra.Repositories;
using Aplication.Interfaces.Repositories;
using Aplication.Services;
using Aplication.Mapping;
namespace Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));
            //Service
            services.AddScoped<INotesService, NotesService>();
            //Mapping
            services.AddAutoMapper(typeof(NotesProfile));

            //Repository
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }

    }
}