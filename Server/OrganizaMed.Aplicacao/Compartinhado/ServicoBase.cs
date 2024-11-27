using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace OrganizaMed.Aplicacao.Compartinhado
{
    public abstract class ServicoBase<TDominio>
    {
        
        protected virtual Result Validar(TDominio obj)
        {
            
        }
        
    }
}
