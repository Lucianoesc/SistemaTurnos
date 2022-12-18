using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CapaDatos
{
    public class CD_Medico
    {
        public List<Medico> Listar()
        {

            List<Medico> Lista = new List<Medico>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdMedico, Documento, NombreCompleto, e.IdEspecialidad, e.Descripcion[DescripcionEspecialidad],Correo, Direccion, Telefono, NumeroMatricula, m.Estado from Medico m");
                    query.AppendLine("inner join ESPECIALIDAD e on e.IdEspecialidad = m.IdEspecialidad");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            Lista.Add(new Medico()
                            {

                                IdMedico = Convert.ToInt32(dr["IdMedico"]),
                                DocumentoMedico = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                NumeroMatricula = dr["NumeroMatricula"].ToString(),
                                oEspecialidad= new Especialidad() { IdEspecialidad = Convert.ToInt32(dr["IdEspecialidad"]), Descripcion = dr["DescripcionEspecialidad"].ToString(), },
                                Estado = Convert.ToBoolean(dr["Estado"]),

                            });
                        }
                    }


                }
                catch (Exception)
                {
                    Lista = new List<Medico>();
                }
            }
            return Lista;
        }
        public int Registrar(Medico obj, out string Mensaje)
        {

            int idMedicogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMedico".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("Documento", obj.DocumentoMedico);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("NumeroMatricula", obj.NumeroMatricula);
                    cmd.Parameters.AddWithValue("IdEspecialidad", obj.oEspecialidad.IdEspecialidad);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idMedicogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idMedicogenerado = 0;
                Mensaje = ex.Message;
            }


            return idMedicogenerado;
        }
        public bool Editar(Medico obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarMedico".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("IdMedico", obj.IdMedico);
                    cmd.Parameters.AddWithValue("Documento", obj.DocumentoMedico);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("NumeroMatricula", obj.NumeroMatricula);
                    cmd.Parameters.AddWithValue("IdEspecialidad", obj.oEspecialidad.IdEspecialidad);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }


            return respuesta;
        }
        public bool Eliminar(Medico obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarMedico".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("IdMedico", obj.IdMedico);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }


            return respuesta;
        }
    }
}
