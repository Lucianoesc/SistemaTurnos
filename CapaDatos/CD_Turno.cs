using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CapaDatos
{
    public class CD_Turno
    {
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select COUNT(*) + 1 from TURNO");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception)
                {
                    idcorrelativo = 0;
                }
            }
            return idcorrelativo;
        }

        public bool Registrar(Turno obj,DataTable DetalleTurno, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTurno", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdPaciente", obj.oPaciente.IdPaciente);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("DetalleTurno", DetalleTurno);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }
            return Respuesta;
        }

        public Turno ObtenerTurno(string numero)
        {
            Turno obj = new Turno();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select t.IdTurno,");
                    query.AppendLine("u.NombreCompleto,");
                    query.AppendLine("pa.Documento,pa.NombreCompleto[NombreCompletoPaciente],");
                    query.AppendLine("t.NumeroDocumento,CONVERT(char(10), t.FechaRegistro, 103)[FechaRegistro]");
                    query.AppendLine("from TURNO t");
                    query.AppendLine("inner join USUARIO u on u.IdUsuario = t.IdUsuario");
                    query.AppendLine("inner join PACIENTE pa on pa.IdPaciente = t.IdPaciente");
                    query.AppendLine("where t.NumeroDocumento = @numero");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero",numero);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Turno()
                            {
                                IdTurno = Convert.ToInt32(dr["IdTurno"]),
                                oUsuario = new Usuario() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                oPaciente = new Paciente() { Documento = dr["Documento"].ToString(), NombreCompletoPaciente = dr["NombreCompletoPaciente"].ToString() },
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),

                            };
                        }
                    }


                }
                catch (Exception)
                {
                    obj = new Turno();
                }
            }

            return obj;
        }

        public List<Detalle_Turno> ObtenerDetalleTurno(int idturno)
        {
            List<Detalle_Turno> oLista = new List<Detalle_Turno>();
            try
            {
                using(SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();


                    query.AppendLine("select m.NombreCompleto,m.Documento,m.NumeroMatricula from DETALLE_TURNO dt");
                    query.AppendLine("inner join MEDICO m on m.IdMedico = dt.IdMedico");
                    query.AppendLine("where dt.IdTurno = @IdTurno");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@IdTurno", idturno);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Detalle_Turno()
                            {
                                oMedico = new Medico() { NombreCompleto = dr["NombreCompleto"].ToString(), DocumentoMedico = dr["Documento"].ToString(), NumeroMatricula = dr["NumeroMatricula"].ToString() }
                            });
                        }
                    }    

                }    
            }
            catch(Exception ex)
            {
                oLista = new List<Detalle_Turno>();
            }
            return oLista;
        }

    }
}
