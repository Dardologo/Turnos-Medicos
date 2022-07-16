using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class TurnoController : Controller
    {
        TurnosContext context = new();

        public IActionResult Index()
        {
            //El metodo to list hacemos un select * medico. Me trae todo yd espues para cada id le asigno al nombre completo
            //que es el que voy a mostrar
            ViewData["IdMedico"] = new SelectList((from medico in context.Medicos.ToList() select new { IdMedico = medico.IdMedico, NombreCompleto = medico.Nombre + " " + medico.Apellido }), "IdMedico", "NombreCompleto");
            ViewData["IdPaciente"] = new SelectList((from paciente in context.Paciente.ToList() select new { IdPaciente = paciente.IdPaciente, NombreCompleto = paciente.Nombre + " " + paciente.Apellido }), "IdPaciente", "NombreCompleto");
            

            return View();

        }
       
        public IActionResult AsignarTurno()
        {
            
           
              ViewData["EspecialidadId"] = new SelectList(context.Especialidades, "IdEspecialidad", "Descripcion");
            

            return View();
        }

    }
}
