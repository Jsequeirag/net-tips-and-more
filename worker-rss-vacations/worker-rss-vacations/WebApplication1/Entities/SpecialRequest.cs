using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class SpecialRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecialRequest_Id { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }//Fecha de creacion Hora CR
        [Required]
        public DateTime DateInitial { get; set; }//Fecha inicial
        [Required]
        public DateTime DateFinally { get; set; } //Fecha final
        public double RequestsDays { get; set; }//Dias solicitados
        [MaxLength(300)]
        public string? Notes { get; set; }

        [Required, MaxLength(100)]
        public string FK_User_Id { get; set; }
 
        [Required, MaxLength(50)]
        public string FK_Conjunct_Id { get; set; }
  
        [Required, MaxLength(100)]
        public string FK_TypeSpecialRequest_Id { get; set; }
   
        public string State_Id { get; set; }
    }
}
