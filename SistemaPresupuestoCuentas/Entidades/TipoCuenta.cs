using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPresupuestoCuentas.Entidades
{
    public class TipoCuenta
    {
        [Key]
        public int TipoId { get; set; }
        public String Descripcion { get; set; }

        public TipoCuenta()
        {
            TipoId = 0;
            Descripcion = string.Empty;
        }
    }
}
