using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class EspecialidadController: Controller
    {
        
        TurnosContext context = new();
        public async Task<IActionResult> Index()
        {

            IEnumerable<Especialidad> especialidades = null;
            //Se utiliza como un medio donde podemos ubicar que objeto queremos que viva. Dentro de las llaves vive
           //con el metodo toList entity framework carga todo el modelo de la bd en la memoria para acceder mas rapido
                especialidades= await context.Especialidades.ToListAsync();
            
                return View( especialidades);
        }
        //Con el ? lo que hacemos es no obtener una excepcion en caso de que sea null la variable
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                //Con este metodo retornamos un error 404 que es uno generico. Esta pagina no existe
                return NotFound();
            }
            var especialidad = await context.Especialidades.FindAsync(id);
            if (especialidad == null)
            {
                //Puede ser que no exista en la bd
                return NotFound();
            }
            return View(especialidad);
        }
        //Agregamos otro metodo encargado del proceso post. Tomar los datos del formulario y grabarlos en nuestra 
        //tabla
        //Los corchetes son para colocar la prop que nos permite enlazar los datos del formulario con este metodo
        //La propiedad bind esta compuesta por los dos campos del formulario.
        //Con el HttpPost indicamos que este emtodo es el encargado de realizar esta accion, de enviarle a la bd
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdEspecialidad,Descripcion")] Especialidad especialidad)
        {
            if(id != especialidad.IdEspecialidad)
            {
                return NotFound();
            }
            //Si esto nos devuelve true quiere decir que el enlace de la prop bind esta ok y podemos guardarla
            if (ModelState.IsValid)
            {
                context.Update(especialidad);
                await context.SaveChangesAsync();
                //Desspues de hacer estos cambios nos va a redirigir a la lista general del listado de especialidades
                return RedirectToAction(nameof(Index));
            }
            return View(especialidad);
        }
        [HttpGet]
        public async Task<IActionResult> Delete (int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var especialidad = await context.Especialidades.FirstOrDefaultAsync(e => e.IdEspecialidad==id);
            //este metodo le enviamos una propiedad para que pueda realizar la busqueda y devolvernos la primer coincidencia
            //Si no encuentra nada nos devuelve nulo y la asgina a especialidad
            if(especialidad == null)
            {
                return NotFound();
            }
            return View(especialidad);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var especialidad = await context.Especialidades.FindAsync(id);
            context.Especialidades.Remove(especialidad);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //Este metodo va a recibir los campos para crear el objeto desde el formulario
        public async Task<IActionResult> Create([Bind("IdEspecialidad, Descripcion")]Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                context.Add(especialidad);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(); 
        }
    }
}
