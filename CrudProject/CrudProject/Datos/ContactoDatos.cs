using System.Data;
using System.Data.SqlClient;
using CrudProject.Models;
namespace CrudProject.Datos

{
    public class ContactoDatos
    {
        public List<ContactoModel> Listar()
        {
            var oLista=new List<ContactoModel>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                cmd.CommandType=CommandType.StoredProcedure;
                using(var dr=cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new ContactoModel()
                        {
                            IdContacto =Convert.ToInt32( dr["IdContacto"]),
                            Nombres= dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo= dr["Correo"].ToString(),

                        });
                    }
                }
            }
            return oLista;
           
        }

        public ContactoModel Obtener(int IdContacto)
        {
            var oContacto = new ContactoModel();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);

                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        oContacto.Nombres = dr["Nombres"].ToString();
                        oContacto.Apellidos = dr["Apellidos"].ToString();
                        oContacto.Telefono = dr["Telefono"].ToString();
                        oContacto.Correo = dr["Correo"].ToString();

                    
                    }
                }
            }
            return oContacto;

        }
        public bool Guardar(ContactoModel oContacto)
            {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                     conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Registrar", conexion);
                    cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                    cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                   
                }
                rpta = true;
            }
            catch (Exception e)
            {   
                string error=e.Message;
                rpta = false; 
            }
            return rpta;
        }

        public bool Editar(ContactoModel oContacto)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);

                    cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);

                    cmd.Parameters.AddWithValue("IdContacto", oContacto.IdContacto);
                    cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                    cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Eliminar(int idContacto)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("IdContacto",idContacto);
                 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }
    }


}
