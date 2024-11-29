namespace OrganizaMed.WebApi.ViewModels
{
    public class InserirMedicoViewModels
    {
        public required string Nome { get; set; }
        public required string Crm { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
    }

    public class EditarMedicoViewModel
    {
        public required string Nome { get; set; }
        public required string Crm { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required DateTime Cooldown { get; set; }
    }

    public class ListarMedicoViewModel
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Crm { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required DateTime Cooldown { get; set; }
    }

    public class VisualizarMedicoViewModel
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Crm { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required DateTime Cooldown { get; set; }
        public required List<ListarAtividadeViewModel> Atividades { get; set; }
    }
}
