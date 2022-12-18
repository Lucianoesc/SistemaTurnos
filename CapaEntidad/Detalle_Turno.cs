using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Detalle_Turno
    {
        public int IdDetalleTurno { get; set; }
        public Medico oMedico { get; set; }
        public string FechaRegistro { get; set; }
    }
}
