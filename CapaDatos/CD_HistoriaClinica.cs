using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class CD_HistoriaClinica
    {
        public CD_HistoriaClinica(string idpaciente,string paciente,string recordatorio,string evolucion,string prescripcion,string usuario, string idusuario, string evento, string detalle, string origen)
        {
            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            string hora = DateTime.Now.ToString("HH:mm");

            string cadena = "insert into Historia_Clinica (Fecha, Hora, IdPaciente, Paciente, Recordatorio, Evolucion, Prescripcion, IdUsuario, Usuario, Evento, Detalle, Origen)" +
                " " + " values('" + fecha + "', '" + hora + "', '" + idpaciente + "', '" + paciente + "', '" + recordatorio + "','" + evolucion + "','" + prescripcion + "','" + idusuario + "','" + usuario + "'," +
                " '" + evento + "', '" + detalle + "', '" + origen + "')";

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                SqlCommand cmd = new SqlCommand(cadena, oconexion);
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
