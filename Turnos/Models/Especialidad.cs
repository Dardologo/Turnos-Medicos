using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Especialidad
    {
        [Key]
        public int IdEspecialidad { get; set; }
        [StringLength(100, ErrorMessage = "El campo descripcion debe tener como máximo 100 caracteres")]
        [Required (ErrorMessage ="Debe ingresar una descripcion")]
        [Display(Name ="Descripción)", Prompt= "Ingrese una descripcion")]
        public string Descripcion { get; set; }  
        public List<MedicoEspecialidad>? MedicoEspecialidad { get; set; }
    }
}
