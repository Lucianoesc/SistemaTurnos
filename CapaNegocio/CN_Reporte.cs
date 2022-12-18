using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objcd_reporte = new CD_Reporte();

        public List<ReporteTurnos> Turno(string fechainicio, string fechafin, int idmedico)
        {
            return objcd_reporte.Turno(fechainicio, fechafin, idmedico);
        }
    }
}
