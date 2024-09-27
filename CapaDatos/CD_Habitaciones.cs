using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Reflection;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Habitaciones
    {



        public List<Habitacion> listar()
        {

            List<Habitacion> lista = new List<Habitacion>();


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))


                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" select h.Idhabitacion, h.Nombre,h.Descripcion, ");
                    sb.AppendLine(" t.IdTipoHabitacion, t.Descripcion[DesTipoHabitacion], ");
                    sb.AppendLine("h.Precio,h.Stock,h.RutaImagen,h.NombreImagen,h.Activo ");
                    sb.AppendLine("from HABITACIONES h ");
                    sb.AppendLine("inner join TIPOHABITACION t on t.IdTipoHabitacion = h.IdTipoHabitacion");
                  
                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                     
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Habitacion()
                                {
                                    IdHabitacion = Convert.ToInt32(dr["IdHabitacion"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),                              
                                    oTipoHabitacion = new Tipohabitacion() { IdTipoHabitacion = Convert.ToInt32(dr["IdTipoHabitacion"]), Descripcion = dr["DesTipoHabitacion"].ToString(), },
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CO")),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"])

                                });
                            }


                        }
                    }

                }
            }

            catch
            {

                lista = new List<Habitacion>();

            }

            return lista;


        }


         public int Registrar(Habitacion obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistarHabitacion", oconexion);




                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("IdTipoHabitacion", obj.oTipoHabitacion.IdTipoHabitacion);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }

            return idautogenerado;
        }


        public bool Editar(Habitacion obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {


                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarHabitacion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("IdHabitacion", obj.IdHabitacion); // Asegúrate de tener el IdHabitacion
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("IdTipoHabitacion", obj.oTipoHabitacion.IdTipoHabitacion);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
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
            return resultado;
        }


        public bool GuardarDatosImagen(Habitacion  obj, out string Mensaje)
        {
            bool resultado = false; 
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    //{
                    //   CommandType = CommandType.StoredProcedure
                    //};

                    string query = "update habitacion set RutaImagen = @rutaimagen, NombreImagen = @nombreimagen where idHabitacion = @IdHabitacion";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue(" @rutaimagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue(" @nombreimagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("IdHabitacion", obj.IdHabitacion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;

                    }
                    else
                    {

                        Mensaje = "No se puedo Actualizar la imagen";
                    }

                        
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
        
            return (resultado); 

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {


                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_EliminarHabitacion", oconexion);

                    cmd.Parameters.AddWithValue("IdHabitacion", id);
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
            return resultado;
        }


    }
}
