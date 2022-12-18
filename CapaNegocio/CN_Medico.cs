using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Medico
    {
        private CD_Medico objcd_Medico = new CD_Medico();
        public List<Medico> Listar()
        {
            return objcd_Medico.Listar();

        }
        public int Registrar(Medico obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.NumeroMatricula == "")
            {
                Mensaje += "Es necesario el numero de matricula del Medico\n";
            }
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre completo del Medico\n";
            }

            if (obj.DocumentoMedico == "")
            {
                Mensaje += "Es necesario el documento del Medico\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Medico.Registrar(obj, out Mensaje);
            }


        }
        public bool Editar(Medico obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.NumeroMatricula == "")
            {
                Mensaje += "Es necesario el numero de matricula del Medico\n";
            }
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre completo del Medico\n";
            }

            if (obj.DocumentoMedico == "")
            {
                Mensaje += "Es necesario el documento del Medico\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Medico.Editar(obj, out Mensaje);
            }
        }
        public bool Eliminar(Medico obj, out string Mensaje)
        {
            return objcd_Medico.Eliminar(obj, out Mensaje);

        }
    }
}
