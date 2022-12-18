using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Xml.Linq;

namespace CapaDatos
{
    public class CD_Usuario
    {
        // Este metodo devuelve una lista de usuarios
        public List<Usuario> Listar()
        {

            List<Usuario> Lista = new List<Usuario>();
            // el sqlconnection nos permite conectarnos a la base de datos con la cadena de conexion, previamente creada en la clase "Conexion.cs"
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                // el try catch es un capturador de errores, si tenemos algun problema al momento de conectarnos a la base de datos.
                // en caso de haber encontrado errores, que el catch nos devuelba una lista nueva.
                // Al final de no haber encontrado errores, decimos que nos retorne la lista con sus respectivas propiedades guardadas
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select u.IdUsuario,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IdRol,r.Descripcion from usuario u");
                    query.AppendLine("inner join rol r on r.IdRol = u.IdRol");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    // especificamos de que tipo es el comando.
                    cmd.CommandType = CommandType.Text;
                    // abrimos la cadena de conexion para que pueda ejecutar este comando.
                    oconexion.Open();
                    //  el SqlDataReader es el encargado de leer el resultado de nuestro comando.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //como es una lista y en sql es una tabla, usamos un mientras, leas cada una de nuestras filas, del select.
                        //Lo guarde en la lista de la clase Usuarios.
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                //guardamos en IdUsuario de la clase usuario, especificamente lo que lea el objeto Data Reader donde esté ["Idusuario"]
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]), Descripcion = dr["Descripcion"].ToString() }

                            });
                        }
                    }


                }
                catch (Exception)
                {
                    Lista = new List<Usuario>();
                }
            }
            return Lista;
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            //CREATE PROC SP_REGISTRARUSUARIO(
            //@Documento varchar(50),
            //@NombreCompleto varchar(100),
            //@Correo varchar(100),
            //@Clave varchar(100),
            //@IdRol int,
            //@Estado bit,
            //@IdUsuarioResultado int output,
            //@Mensaje varchar(500) output
            //)

            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }


            return idusuariogenerado;
        }
        public bool Editar(Usuario obj, out string Mensaje)
        {
            //CREATE PROC SP_EDITARUSUARIO(
            //@IdUsuario int,
            //@Documento varchar(50),
            //@NombreCompleto varchar(100),
            //@Correo varchar(100),
            //@Clave varchar(100),
            //@IdRol int,
            //@Estado bit,
            //@Respuesta bit output,
            //@Mensaje varchar(500) output
            //)

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
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
        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            //CREATE PROC SP_ELIMINARUSUARIO(
            //@IdUsuario int,
            //@Respuesta bit output,
            //@Mensaje varchar(500) output
            //)

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
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
