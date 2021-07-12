using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD_MVC.Models
{
    public class DataCursos
    {
        private string _cnnString;
        public DataCursos()
        {
            _cnnString = ConfigurationManager.ConnectionStrings["InstitutoConnection"].ConnectionString;
        }


        public List<EntidadesEstatus> ConsultarLst()
        {
            List<EntidadesEstatus> lstEstatus = new List<EntidadesEstatus>();
            string query = $"select id,nombre,Clave from EstatusAlumnos";
            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {

                    SqlCommand comando = new SqlCommand(query, con);
                    comando.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        EntidadesEstatus estatus = new EntidadesEstatus();
                        estatus.id = Convert.ToInt32(reader["id"]);
                        estatus.Nombre = Convert.ToString(reader["nombre"]);
                        estatus.Clave = Convert.ToString(reader["Clave"]);
                        lstEstatus.Add(estatus);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Consultar ", ex);
            }

            return lstEstatus;
        }
        public void EliminarEstado(int id)
        {
            string f = Convert.ToString(id);

            string query = ($"delete EstatusAlumnos where id = {f}");

            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {
                    SqlCommand comando = new SqlCommand(query, con);
                    comando.CommandType = CommandType.Text;
                    con.Open();
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar", ex);
            }
        }


        public int AgregarEstatus(string clave, string nombre)
        {


            int IdRegreso = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {
                    SqlCommand comando = new SqlCommand("sp_AddEstatus", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlParameter paramID = new SqlParameter("id", SqlDbType.Int);
                    paramID.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(paramID);

                    comando.Parameters.AddWithValue("@clave", clave);
                    comando.Parameters.AddWithValue("@nombre", nombre);

                    int numfilaAfec = comando.ExecuteNonQuery();

                    if (numfilaAfec > 0)
                    {
                        IdRegreso = Convert.ToInt32(comando.Parameters["id"].Value);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar", ex);
            }
            return IdRegreso;
        }

        public void ModificarEstatus(int id, string nombre, string clave)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {
                    SqlCommand comando = new SqlCommand("sp_ActualizarEstatus", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@clave", clave);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Actualizar", ex);
            }


        }



    }
}