using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ReporteHistoriaClinica
    {
        private CD_ReporteHistoriaClinica objcd_HistoriaClinica = new CD_ReporteHistoriaClinica();
        public List<Historia_Clinica> Listar()
        {
            return objcd_HistoriaClinica.Listar();

        }
    }
}
