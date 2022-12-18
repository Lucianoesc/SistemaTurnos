using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public Usuario oUsuario { get; set; }
        public Paciente oPaciente { get; set; }
        public string NumeroDocumento { get; set; }
        public List<Detalle_Turno> oDetalleTurno { get; set; }
        public string FechaRegistro { get; set; }
    }
}
