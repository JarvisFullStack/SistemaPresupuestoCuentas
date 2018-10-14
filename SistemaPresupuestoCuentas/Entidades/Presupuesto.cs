using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPresupuestoCuentas.Entidades
{
    public class Presupuesto
    {
        [Key]
        public int PresupuestoId { get; set; }
        public String Descripcion;
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }

        public Presupuesto()
        {
            PresupuestoId = 0;
            Descripcion = string.Empty;
            Monto = 0;
            Fecha = DateTime.Now;
        }
    }
}
