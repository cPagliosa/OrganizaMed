using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.ModuloMedico;
using Med.Infra.Orm.Compartinhado;
using Med.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace Med.Infra.Orm.Tests.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoOrmTest
    {
        private MedDbContext db;

        private RepositorioMedicoOrm repositorioMedico;
        public RepositorioMedicoOrmTest()
        {
            var builder = new DbContextOptionsBuilder<MedDbContext>();

            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=OrganizaMed;Integrated Security=True");

            db = new MedDbContext(builder.Options);

            repositorioMedico = new RepositorioMedicoOrm(db);

            db.Set<Medico>().RemoveRange(db.Set<Medico>());
            db.SaveChanges();

        }

        [TestMethod]
        public void Deve_Cadastrar_Medico()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            repositorioMedico.Inserir(m1);
            db.SaveChanges();

            Medico medicoEncontrado = repositorioMedico.SelecionarPorId(m1.Id);

            Assert.IsNotNull(medicoEncontrado);
            Assert.AreEqual(m1.Id, medicoEncontrado.Id);
            Assert.AreEqual(m1.Nome, medicoEncontrado.Nome);
            Assert.AreEqual(m1.CRM, medicoEncontrado.CRM);
            Assert.AreEqual(m1.Email, medicoEncontrado.Email);
        }
    }
}
