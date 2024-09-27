using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocio


{
    public class CN_TipoHabitacion
    {
        private CD_TipoHabitacion objCapaDato = new CD_TipoHabitacion();

        public List<Tipohabitacion> listar()
        {
            return objCapaDato.listar();
        }

        public int Registrar(Tipohabitacion obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTipoHabitacion", oconexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }

            return idautogenerado;
        }


        public bool Editar(Tipohabitacion obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {

                Mensaje = "La descripcion del tipo de habitacion no puede estar vacio";

            }

            if (string.IsNullOrEmpty(Mensaje))
            {





                return objCapaDato.Editar(obj, out Mensaje);

            }
            else
            {
                return false;
            } 
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

    }
}
