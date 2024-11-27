using FluentValidation;

namespace Med.dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>
    {
        public ValidadorMedico()
        {
            RuleFor(m => m.Nome).NotNull().NotEmpty();
            RuleFor(m => m.Email).NotNull().NotEmpty();
            RuleFor(m => m.Telefone).NotNull().NotEmpty();
            RuleFor(m => m.CRM).NotNull().NotEmpty();

        }
    }
}
