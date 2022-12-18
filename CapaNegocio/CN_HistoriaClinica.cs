using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_HistoriaClinica
    {
        private string recordatorio;
        private string evolucion;
        private string prescripcion;
        private string evento;
        private string detalle;
        private string origen;

        public CN_HistoriaClinica(string idpaciente, string paciente,string recordatorio,string evolucion, string prescripcion, string usuario, string idusuario, string evento, string detalle, string origen)
        {
            this.recordatorio = recordatorio;
            this.evolucion= evolucion;
            this.prescripcion= prescripcion;
            this.evento = evento;
            this.detalle = detalle;
            this.origen = origen;

            CD_HistoriaClinica Guardar = new CD_HistoriaClinica(idpaciente,paciente,recordatorio,evolucion,prescripcion,usuario, idusuario, evento, detalle, origen);
        }
    }
}
