using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Med.Infra.Orm.Compartinhado
{
    public static class MigradorBD
    {
        public static bool AtualizarBancoDados(DbContext db)
        {
            var qtdMigracoesPendentes = db.Database.GetPendingMigrations().Count();

            if (qtdMigracoesPendentes == 0)
            {
                Console.WriteLine("Nenhuma migração pendente, continuando...");

                return false;
            }

            Console.WriteLine("Aplicando migrações pendentes, isso pode demorar alguns segundos...");

            db.Database.Migrate();

            return true;
        }
    }
}
