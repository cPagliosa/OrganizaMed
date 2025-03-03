﻿using FluentValidation;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;

namespace Med.dominio.ModuloAtividade
{
    public class ValidarAtividade : AbstractValidator<Atividade>
    {
        public ValidarAtividade()
        {
            RuleFor(a => a.Medicos).NotNull().NotEmpty();
            RuleFor(a => a.Inicio).NotNull().NotEmpty();
            RuleFor(a => a.Termino).NotNull().NotEmpty();
            RuleFor(a => a.Titulo).NotNull().NotEmpty();

            RuleFor(a => a).Must((atividade) =>
            {
                List<string> erro = new List<string>();
                if (atividade.Tipo == TipoAtividade.Consulta)
                {
                    
                    if (atividade.Medicos.Count() != 1)
                    {
                        //context.AddFailure("Medicos", "Uma Consulta deve ter exatamente um médico.");
                        erro.Add("Uma Consulta deve ter exatamente um médico.");
                    }
                }
                else
                {
                    if (!atividade.Medicos.Any())
                    {
                        //context.AddFailure("Medicos", "Uma atividade do tipo especificado deve ter pelo menos um médico.");
                        erro.Add("Uma atividade do tipo especificado deve ter pelo menos um médico.");
                    }
                }
                return !erro.Any();
            }).WithMessage("erro de tipo");

        }
    }
}
