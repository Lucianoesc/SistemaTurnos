using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ReporteHistoriaClinica
    {
        public List<Historia_Clinica> Listar()
        {

            List<Historia_Clinica> Lista = new List<Historia_Clinica>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select * from historia_clinica");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            Lista.Add(new Historia_Clinica()
                            {

                                idEvento = Convert.ToInt32(dr["idEvento"]),
                                Fecha = dr["Fecha"].ToString(),
                                Hora = dr["Hora"].ToString(),
                                IdPaciente = dr["IdPaciente"].ToString(),
                                Paciente = dr["Paciente"].ToString(),
                                Recordatorio = dr["Recordatorio"].ToString(),
                                Evolucion = dr["Evolucion"].ToString(),
                                Prescripcion = dr["Prescripcion"].ToString(),
                                IdUsuario = dr["IdUsuario"].ToString(),
                                Usuario = dr["Usuario"].ToString(),
                                Evento = dr["Evento"].ToString(),
                                Detalle = dr["Detalle"].ToString(),
                                Origen = dr["Origen"].ToString(),



                            });
                        }
                    }


                }
                catch (Exception)
                {
                    Lista = new List<Historia_Clinica>();
                }
            }
            return Lista;
        }
    }
}
