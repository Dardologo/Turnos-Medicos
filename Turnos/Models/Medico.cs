using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe ingresar una dirección ")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Debe ingresar un teléfono")]
        [Display(Name = "teléfono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Debe ingresar un mail")]
        [EmailAddress (ErrorMessage="No es una dirección de email válida")]
        public string Email { get; set; }
        [Display (Name ="Horario desde")]
        [DataType (DataType.Time)]
        [DisplayFormat (DataFormatString="{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime HorarioAtencionDesde { get; set; }
        [Display(Name = "Horario hasta")]
        [DataType(DataType.Time)]
        [DisplayFormat (DataFormatString="{0:hh:mm tt}", ApplyFormatInEditMode =true)]
        public DateTime HorarioAtencionHasta { get; set; }
        //Si queremos mostrar en la vista de medico una lista de especialidad disp para asignar cuando lo creamos o editamos
        //Podemos vincularlo mediante esta propiedad
        public List<MedicoEspecialidad>? MedicoEspecialidad { get; set; }
        public List<Turno>? Turno { get; set; }






    }
}
