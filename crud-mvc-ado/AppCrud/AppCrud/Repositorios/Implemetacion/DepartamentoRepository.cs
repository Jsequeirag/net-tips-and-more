using AppCrud.Models;
using AppCrud.Repositorios.contrato;
using System.Data.SqlClient;

namespace AppCrud.Repositorios.Implemetacion
{
    public class DepartamentoRepository : IGenericRepository<Departamento>
    {
        private readonly String _cadenaSQL = "";
        
        public DepartamentoRepository(IConfiguration configuration) {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }
        public Task<bool> Editar(Departamento modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(Departamento modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Departamento>> Lista()
        {
          List<Departamento> _lista=new List<Departamento>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaDepartamentos", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using(var dr=await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new Departamento
                        {
                            idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
               

            }
            return _lista;
        }
    }
}
