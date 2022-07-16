using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class PacienteController : Controller
    {
        TurnosContext context = new();
        public async Task<IActionResult> Index()
        {
            return View(await context.Paciente.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paciente = await context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //Esta propiedad es para que no me agreguen los campos desde la url. Me aseguro que se haga desde el formulario
        //y que el usuario este registrado
        [ValidateAntiForgeryToken]
        //Este metodo va a recibir los campos para crear el objeto desde el formulario
        public async Task<IActionResult> Create([Bind("IdPaciente,Nombre,Apellido,Direccion,Telefono,Email")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                context.Add(paciente);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }
        public async Task<IActionResult> Edit (int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var paciente = await context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaciente,Nombre,Apellido,Direccion,Telefono,Email")] Paciente paciente)
        {
            if (id != paciente.IdPaciente)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Update(paciente);
                await context.SaveChangesAsync();
                RedirectToAction(nameof(Index));
            }
            return View(paciente);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var paciente = await context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == id);
            if(paciente== null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        //Con el action name le asignamos un alias al deletConfirmed porque si le dejo solo delete es igual al primer delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var paciente = await context.Paciente.FindAsync(id);
            if (paciente ==null)
            {
                return NotFound();
            }
            context.Paciente.Remove(paciente);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
