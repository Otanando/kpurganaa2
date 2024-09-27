using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data;
using System.Security.Claims;
using System.Data.SqlClient;


namespace CapaDatos
{
    public  class CD_TipoHabitacion
    {

        public List<Tipohabitacion> listar()
        {

            List<Tipohabitacion> lista = new List<Tipohabitacion>();


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))


                {

                    string query = "SELECT IdTipoHabitacion, Descripcion, Activo FROM TIPOHABITACION";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Tipohabitacion()
                             {

                                IdTipoHabitacion = Convert.ToInt32(dr["IdTipoHabitacion"]),
                                Descripcion = dr ["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])

                            });
                            
                        }
                    }
                  
                }
            }

            catch
            {

                lista = new List<Tipohabitacion>();

            }

            return lista;


        }






        

        public bool Editar(Tipohabitacion obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
             

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_EditarTipoHabitacion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("IdTipoHabitacion", obj.IdTipoHabitacion); // Asegúrate de que el ID está en tu objeto Usuario
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }
            }
            catch (SqlException ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }


        public bool Eliminar(int id , out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {


                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_EliminarTipoHabitacion", oconexion);
                  
                    cmd.Parameters.AddWithValue("IdTipoHabitacion", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }
            }
            catch (SqlException ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }


    }
}
