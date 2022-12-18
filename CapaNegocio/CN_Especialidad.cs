using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Especialidad
    {
        private CD_Especialidad objcd_Especialidad = new CD_Especialidad();

        public List<Especialidad> Listar()
        {
            return objcd_Especialidad.Listar();

        }
        public int Registrar(Especialidad obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion de la Especialidad\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Especialidad.Registrar(obj, out Mensaje);
            }


        }
        public bool Editar(Especialidad obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion de la Especialidad\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Especialidad.Editar(obj, out Mensaje);
            }
        }
        public bool Eliminar(Especialidad obj, out string Mensaje)
        {
            return objcd_Especialidad.Eliminar(obj, out Mensaje);

        }
    }
}
