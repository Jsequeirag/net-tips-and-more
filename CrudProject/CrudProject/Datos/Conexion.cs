using System.Data.SqlClient;
namespace CrudProject.Datos
    
{
    public class Conexion
    { 
       private string cadenaSQL=string.Empty;
        public Conexion()  {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cadenaSQL=builder.GetSection("connectionString:CadenaSQL").Value;
        }
        public string getCadenaSQL() {  return cadenaSQL; }
    }
}
