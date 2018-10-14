using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPresupuestoCuentas.Entidades
{
    public class Cuenta
    {
        [Key]
        public int CuentaId { get; set; }
        public string Descripcion { get; set; }
        public int TipoId;
        public double Monto;

        public Cuenta()
        {
            CuentaId = 0;
            Descripcion = string.Empty;
            TipoId = 0;
            Monto = 0.0;
        }
    }
}
