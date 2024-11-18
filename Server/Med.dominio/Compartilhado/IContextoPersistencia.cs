using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med.dominio.Compartilhado
{
    public interface IContextoPersistencia
    {
        Task<bool> GravarAsync();
    }
}
