using Med.dominio.ModuloMedico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.ModuloAtividade;

namespace Med.Dominio.Tests.ModuloAtividade
{
    [TestClass]
    public class AtividadeTest
    {
        [TestMethod]
        public void Deve_Retornar_Nenhum_Erros_Entrada_Consulta()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            List<Medico> medicos = new List<Medico>();

            medicos.Add(m1);

            Atividade a = new Atividade(
                "Consulta Pediatra", inicio, termino,
                medicos, true);

            List<string> errosEncontrados = a.Validar();

            Assert.AreEqual(0, errosEncontrados.Count());
        }
        [TestMethod]
        public void Deve_Retornar_Erro_Entrada_Mais_Mediocos_Consulta()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            Medico m2 = new Medico(
                "Lucas2", "45862-SC",
                "Luc2@gmail.com", "(49)988552200");

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            List<Medico> medicos = new List<Medico>();

            medicos.Add(m1);
            medicos.Add(m2);

            Atividade a = new Atividade(
                "Consulta Pediatra", inicio, termino,
                medicos, true);

            List<string> errosEncontrados = a.Validar();

            string erroEsperado = "Na consulta, é obrigatório ter exatamente um médico.";

            Assert.AreEqual(erroEsperado, errosEncontrados[0]);
        }
        [TestMethod]
        public void Deve_Retornar_Nenhum_Erro_Entrada_Cirurgia()
        {
            Medico m1 = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            DateTime inicio = new DateTime(2024, 11, 22, 10, 0, 0);

            DateTime termino = new DateTime(2024, 11, 22, 10, 30, 0);

            List<Medico> medicos = new List<Medico>();

            medicos.Add(m1);

            Atividade a = new Atividade(
                "Consulta Pediatra", inicio, termino,
                medicos, false);

            List<string> errosEncontrados = a.Validar();

            Assert.AreEqual(0, errosEncontrados.Count());
        }
    }
}
