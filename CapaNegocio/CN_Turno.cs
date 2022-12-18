using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Turno
    {
        private CD_Turno objcd_turno = new CD_Turno();
        public int ObtenerCorrelativo()
        {
            return objcd_turno.ObtenerCorrelativo();

        }
        public bool Registrar(Turno obj,DataTable DetalleTurno, out string Mensaje)
        {
            return objcd_turno.Registrar(obj,DetalleTurno, out Mensaje);

        }
        public Turno ObtenerTurno(string numero)
        {
            Turno oTurno = objcd_turno.ObtenerTurno(numero);

            if(oTurno.IdTurno != 0)
            {
                List<Detalle_Turno> oDetalleTurno = objcd_turno.ObtenerDetalleTurno(oTurno.IdTurno);

                oTurno.oDetalleTurno = oDetalleTurno;
            }
            return oTurno;
        }
    }
}
