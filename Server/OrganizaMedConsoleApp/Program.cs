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
            var medico1 = new Medico(
                "Paulo", "65262-SC",
                "pp@gmail.com", "(49)999552200");

            var medico2 = new Medico(
                "Joao", "65262-SC",
                "jao@gmail.com", "(49)999552211");

            var optionsBuilder = new DbContextOptionsBuilder<MedDbContext>();

            optionsBuilder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=OrganizaMedWebApi;Integrated Security=True");

            var dbContext = new MedDbContext(optionsBuilder.Options);

            List<Medico> medicos = new List<Medico>();

            medicos.Add(medico1);

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            var atividade1 = new Atividade("consulta pediatra", inicio, termino, medicos, false);

            medicos.Add(medico2);

            var atividade2 = new Atividade("cirurgia", inicio, termino, medicos, true);

            dbContext.Add(medico1);
            dbContext.Add(medico2);
            dbContext.Add(atividade1);
            dbContext.Add(atividade2);
            dbContext.SaveChanges();

            Console.ReadLine();
        }
    }
}
