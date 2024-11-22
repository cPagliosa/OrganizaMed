using Med.dominio.ModuloMedico;

namespace Med.Dominio.Tests.ModuloMedico
{
    [TestClass]
    public class MedicoTest
    {
        [TestMethod]
        public void Deve_Add_Tempo_Descanso_Cirurgia()
        {
            Medico m = new Medico(
                "Lucas","45862-SC",
                "Luc@gmail.com","(49)988552200");

            DateTime termino = new DateTime(2024, 11, 22, 10, 0, 0);

            m.Cooldown = m.Descanso(TimeSpan.FromHours(4), termino);

            DateTime descansoEsperado = new DateTime(2024, 11, 22, 14, 0, 0);

            Assert.AreEqual(descansoEsperado,m.Cooldown);
        }
        [TestMethod]
        public void Deve_Add_Tempo_Descanso_Consulta()
        {
            Medico m = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            DateTime termino = new DateTime(2024, 11, 22, 10, 0, 0);

            m.Cooldown = m.Descanso(TimeSpan.FromMinutes(10), termino);

            DateTime descansoEsperado = new DateTime(2024, 11, 22, 10, 10, 0);

            Assert.AreEqual(descansoEsperado, m.Cooldown);
        }

        [TestMethod]
        public void Deve_Retornar_Nada_Erros_Entrada()
        {
            Medico m = new Medico(
                "Lucas", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            List<string> errosEncontrados = m.Validar();

            Assert.AreEqual(0,errosEncontrados.Count());
        }
        [TestMethod]
        public void Deve_Retornar_Erro_Nome()
        {
            Medico m = new Medico(
                "L", "45862-SC",
                "Luc@gmail.com", "(49)988552200");

            List<string> errosEncontrados = m.Validar();

            string erroEsperado = "O nome precisa ter mais de dois caracteres";

            Assert.AreEqual(erroEsperado, errosEncontrados[0]);
        }
        [TestMethod]
        public void Deve_Retornar_Erro_CRM()
        {
            Medico m = new Medico(
                "Lucas", "458-SC",
                "Luc@gmail.com", "(49)988552200");

            List<string> errosEncontrados = m.Validar();

            string erroEsperado = "O CRM invalido";

            Assert.AreEqual(erroEsperado, errosEncontrados[0]);
        }
    }
}
