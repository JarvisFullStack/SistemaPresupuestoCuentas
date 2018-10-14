using SistemaPresupuestoCuentas.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPresupuestoCuentas.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<TipoCuenta> TipoCuenta { get; set; }
        public DbSet<Presupuesto> Presupuesto { get; set; }


        public Contexto() : base("ConStr")
        {


        }
    }
}
