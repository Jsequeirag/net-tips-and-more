using System.ComponentModel.DataAnnotations;


namespace CrudProject.Models
{
    public class ContactoModel
    {
        public int IdContacto { get; set; }
  
        [Required(ErrorMessage ="El campo Nombre es obligatorio")]
        public string? Apellidos { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombres { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string?Correo { get; set; }
    }
}
