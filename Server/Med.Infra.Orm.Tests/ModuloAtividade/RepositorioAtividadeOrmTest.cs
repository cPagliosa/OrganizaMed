using Med.dominio.ModuloMedico;
using Med.Infra.Orm.Compartinhado;
using Med.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.ModuloAtividade;
using Med.Infra.Orm.ModuloAtividade;

namespace Med.Infra.Orm.Tests.ModuloAtividade
{
    [TestClass]
    public class RepositorioAtividadeOrmTest
    {
        private MedDbContext db;

        private RepositorioAtividedeOrm repositorioAtividade;
        private RepositorioMedicoOrm repositorioMedico;
        public RepositorioAtividadeOrmTest()
        {
            var builder = new DbContextOptionsBuilder<MedDbContext>();

            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=OrganizaMed;Integrated Security=True");

            db = new MedDbContext(builder.Options);

            repositorioAtividade = new RepositorioAtividedeOrm(db);
            repositorioMedico = new RepositorioMedicoOrm(db);

            db.Set<Atividade>().RemoveRange(db.Set<Atividade>());
            db.Set<Medico>().RemoveRange(db.Set<Medico>());
            db.SaveChanges();
        }

        [TestMethod]
        public void Deve_Cadastrar_Atividade_Cirurgia()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            Medico m2 = new Medico(
                "Caio", "43862-SC",
                "caca@gmail.com", "(49)999552200");

            repositorioMedico.Inserir(m1);
            repositorioMedico.Inserir(m2);
            db.SaveChanges();

            List<Medico> medicos = repositorioMedico.SelecionarTodos();

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            Atividade a = new Atividade(
                "Transpante", inicio, termino,
                medicos, false);

            repositorioAtividade.Inserir(a);
            db.SaveChanges();

            Atividade atividadeEncontrado = repositorioAtividade.SelecionarPorId(a.Id);

            Assert.IsNotNull(atividadeEncontrado);
            Assert.AreEqual(a.Id, atividadeEncontrado.Id);
            Assert.AreEqual(a.Titulo, atividadeEncontrado.Titulo);
            Assert.AreEqual(a.Tipo, atividadeEncontrado.Tipo);
            Assert.AreEqual(a.Termino.Date, atividadeEncontrado.Termino.Date);
            Assert.AreEqual(a.Inicio.Date, atividadeEncontrado.Termino.Date);
        }
        [TestMethod]
        public void Deve_Cadastrar_Atividade_Consulta()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");


            repositorioMedico.Inserir(m1);
            db.SaveChanges();

            List<Medico> medicos = repositorioMedico.SelecionarTodos();

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            Atividade a = new Atividade(
                "Transpante", inicio, termino,
                medicos, true);

            repositorioAtividade.Inserir(a);
            db.SaveChanges();

            Atividade atividadeEncontrado = repositorioAtividade.SelecionarPorId(a.Id);

            Assert.IsNotNull(atividadeEncontrado);
            Assert.AreEqual(a.Id, atividadeEncontrado.Id);
            Assert.AreEqual(a.Titulo, atividadeEncontrado.Titulo);
            Assert.AreEqual(a.Tipo, atividadeEncontrado.Tipo);
            Assert.AreEqual(a.Termino.Date, atividadeEncontrado.Termino.Date);
            Assert.AreEqual(a.Inicio.Date, atividadeEncontrado.Termino.Date);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Erro_Cadastrar_Atividade_Cirurgia()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            Medico m2 = new Medico(
                "Caio", "43862-SC",
                "caca@gmail.com", "(49)999552200");

            repositorioMedico.Inserir(m1);
            repositorioMedico.Inserir(m2);
            db.SaveChanges();

            List<Medico> medicos = repositorioMedico.SelecionarTodos();

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            Atividade a = new Atividade(
                "Transpante", inicio, termino,
                medicos, true);

            repositorioAtividade.Inserir(a);
            db.SaveChanges();

            Atividade atividadeEncontrado = repositorioAtividade.SelecionarPorId(a.Id);

            Assert.Fail("Uma Consulta deve ter exatamente um médico.");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Erro_Cadastrar_Atividade_Conflito_De_Horario()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            repositorioMedico.Inserir(m1);
            db.SaveChanges();

            List<Medico> medicos = repositorioMedico.SelecionarTodos();

            // Primeira atividade
            DateTime inicio1 = new DateTime(2024, 11, 22, 10, 0, 0);
            DateTime termino1 = new DateTime(2024, 11, 22, 10, 30, 0);
            Atividade a1 = new Atividade("Consulta Geral", inicio1, termino1, medicos, true);

            repositorioAtividade.Inserir(a1);
            db.SaveChanges();

            // Atividade conflitante
            DateTime inicio2 = new DateTime(2024, 11, 22, 10, 15, 0);
            DateTime termino2 = new DateTime(2024, 11, 22, 10, 45, 0);
            Atividade a2 = new Atividade("Nova Consulta", inicio2, termino2, medicos, true);

            repositorioAtividade.Inserir(a2); // Deve lançar exceção devido ao conflito de horário
            db.SaveChanges();

            Assert.Fail("Não deveria ser possível cadastrar atividades com horários conflitantes para o mesmo médico.");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Erro_Cadastrar_Atividade_Antes_Do_Cooldown()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            repositorioMedico.Inserir(m1);
            db.SaveChanges();

            List<Medico> medicos = repositorioMedico.SelecionarTodos();

            // Primeira atividade (que define cooldown)
            DateTime inicio1 = new DateTime(2024, 11, 22, 10, 0, 0);
            DateTime termino1 = new DateTime(2024, 11, 22, 11, 0, 0);
            Atividade a1 = new Atividade("Cirurgia", inicio1, termino1, medicos, false);

            repositorioAtividade.Inserir(a1);
            db.SaveChanges();

            // Tentativa de agendar nova atividade antes do cooldown expirar
            DateTime inicio2 = new DateTime(2024, 11, 22, 12, 30, 0); // Cooldown termina às 15h (11h + 4h)
            DateTime termino2 = new DateTime(2024, 11, 22, 13, 0, 0);
            Atividade a2 = new Atividade("Consulta Pós-Cirúrgica", inicio2, termino2, medicos, true);

            repositorioAtividade.Inserir(a2); // Deve lançar exceção devido ao cooldown
            db.SaveChanges();

            Assert.Fail("Não deveria ser possível cadastrar uma atividade antes do cooldown expirar.");
        }
        [TestMethod]
        public void Deve_Cadastrar_Atividades_Sem_Conflito()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            repositorioMedico.Inserir(m1);
            db.SaveChanges();

            List<Medico> medicos = repositorioMedico.SelecionarTodos();

            // Primeira atividade
            DateTime inicio1 = new DateTime(2024, 11, 22, 8, 0, 0);
            DateTime termino1 = new DateTime(2024, 11, 22, 9, 0, 0);
            Atividade a1 = new Atividade("Consulta Geral", inicio1, termino1, medicos, true);

            repositorioAtividade.Inserir(a1);
            db.SaveChanges();

            // Segunda atividade em horário não conflitante
            DateTime inicio2 = new DateTime(2024, 11, 22, 9, 30, 0);
            DateTime termino2 = new DateTime(2024, 11, 22, 10, 30, 0);
            Atividade a2 = new Atividade("Consulta Especializada", inicio2, termino2, medicos, true);

            repositorioAtividade.Inserir(a2);
            db.SaveChanges();

            Atividade atividadeEncontrada1 = repositorioAtividade.SelecionarPorId(a1.Id);
            Atividade atividadeEncontrada2 = repositorioAtividade.SelecionarPorId(a2.Id);

            Assert.IsNotNull(atividadeEncontrada1);
            Assert.IsNotNull(atividadeEncontrada2);
            Assert.AreEqual(a1.Titulo, atividadeEncontrada1.Titulo);
            Assert.AreEqual(a2.Titulo, atividadeEncontrada2.Titulo);
        }
    }
}
