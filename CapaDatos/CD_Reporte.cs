using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<ReporteTurnos> Turno(string fechainicio, string fechafin, int idmedico)
        {
            List<ReporteTurnos> lista = new List<ReporteTurnos>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("sp_HistoriaMedica", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idmedico", idmedico);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteTurnos()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                NumeroDocumento =dr["NumeroDocumento"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoPaciente = dr["DocumentoPaciente"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                NombreMedico = dr["NombreMedico"].ToString(),
                                NumeroMatricula = dr["NumeroMatricula"].ToString(),
                                Especialidad = dr["Especialidad"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteTurnos>();
                }
            }
            return lista;
        }
    }
}
