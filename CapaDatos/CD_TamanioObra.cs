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
    public class CD_TamanioObra
    {
        public List<Tamanio> Listar()
        {
            List<Tamanio> lista = new List<Tamanio>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idTamanio,CantBocas,PersonalTrabajo from TAMANIO_OBRA");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(new Tamanio()
                            {
                                IdTamanio = Convert.ToInt32(dr["idTamanio"]),
                                CantBocas = Convert.ToInt32(dr["CantBocas"]),
                                PersonalTrabajo = Convert.ToInt32(dr["PersonalTrabajo"]),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                    lista = new List<Tamanio>();
                }
            }

            return lista;

        }




        public int Registrar(Tamanio obj, out string Mensaje)
        {
            int idTamaniogenerado = 0;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegistrarTamanioObra", oconexion);
                    cmd.Parameters.AddWithValue("CantBocas", obj.CantBocas);
                    cmd.Parameters.AddWithValue("PersonalTrabajo", obj.PersonalTrabajo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idTamaniogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idTamaniogenerado = 0;
                Mensaje = ex.Message;
            }

            return idTamaniogenerado;
        }



        public bool Editar(Tamanio obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;


            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_EditarTamanioObra", oconexion);
                    cmd.Parameters.AddWithValue("idTamanio", obj.IdTamanio);
                    cmd.Parameters.AddWithValue("CantBocas", obj.CantBocas);
                    cmd.Parameters.AddWithValue("PersonalTrabajo", obj.PersonalTrabajo);
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


        public bool Eliminar(Tamanio obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarTamanioObra", oconexion);
                    cmd.Parameters.AddWithValue("idTamanio", obj.IdTamanio);
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
    }
}
