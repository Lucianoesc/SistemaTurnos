using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Paciente
    {
        private CD_Paciente objcd_Paciente = new CD_Paciente();
        public List<Paciente> Listar()
        {
            return objcd_Paciente.Listar();

        }
        public int Registrar(Paciente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario del documento del Paciente\n";
            }
            if (obj.NombreCompletoPaciente == "")
            {
                Mensaje += "Es necesario el nombre completo del Paciente\n";
            }

            if (obj.Telefono == "")
            {
                Mensaje += "Es necesario el telefono del Paciente\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Paciente.Registrar(obj, out Mensaje);
            }


        }
        public bool Editar(Paciente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario del documento del Paciente\n";
            }
            if (obj.NombreCompletoPaciente == "")
            {
                Mensaje += "Es necesario el nombre completo del Paciente\n";
            }

            if (obj.Telefono == "")
            {
                Mensaje += "Es necesario el telefono del Paciente\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Paciente.Editar(obj, out Mensaje);
            }
        }
        public bool Eliminar(Paciente obj, out string Mensaje)
        {
            return objcd_Paciente.Eliminar(obj, out Mensaje);

        }
    }
}
