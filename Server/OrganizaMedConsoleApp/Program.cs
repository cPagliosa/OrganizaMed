using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using Med.Infra.Orm.Compartinhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OrganizaMedConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var medico = new Medico(
                "Caio", "45262-SC",
                "caio@gmail.com", "(49)999552200");

            var optionsBuilder = new DbContextOptionsBuilder<MedDbContext>();

            optionsBuilder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=OrganizaMedWebApi;Integrated Security=True");

            var dbContext = new MedDbContext(optionsBuilder.Options);
            List<Medico> medicos = new List<Medico>();
            medicos.Add(medico);

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            var atividade = new Atividade("consulta pediatra", inicio, termino, medicos, false);

            dbContext.Add(medico);
            dbContext.Add(atividade);
            dbContext.SaveChanges();

            Console.ReadLine();
        }
    }
}
