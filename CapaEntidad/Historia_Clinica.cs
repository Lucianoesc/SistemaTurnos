using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Historia_Clinica
    {
        public int idEvento { get; set; }
        public string Fecha { get; set; }

        public string Hora { get; set; }
        public string IdPaciente { get; set; }
        public string Paciente { get; set; }
        public string Recordatorio { get; set; }
        public string Evolucion { get; set; }
        public string Prescripcion { get; set; }
        public string IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Evento { get; set; }
        public string Detalle { get; set; }
        public string Origen { get; set; }
    }
}
