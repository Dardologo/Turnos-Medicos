using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class MedicoController : Controller

      
    {
        TurnosContext context = new();
        public async Task<IActionResult> Index()
        {

            return View(await context.Medicos.ToListAsync());
        }

        public async Task<IActionResult> Details (int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var medico = await context.Medicos.Where(m => m.IdMedico == id).Include(me => me.MedicoEspecialidad)
                .ThenInclude(e => e.Especialidad)
                .FirstOrDefaultAsync();
            if (medico ==null)
            {
                return NotFound();
            }
            return View(medico);
        }

        public IActionResult Create()
        {
            //contiene todos los regirtros de la tabla especialidad, todas las especialidades
            ViewData["ListaEspecialidades"] = new SelectList(context.Especialidades, "IdEspecialidad", "Descripcion");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind
            ("IdMedico, Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")]Medico medico, int IdEspecialidad)
        {
            if (ModelState.IsValid)
            {
               // medico.Id = 0;
                context.Add(medico);
                await context.SaveChangesAsync();
                var medicoEspecialidad = new MedicoEspecialidad();
                medicoEspecialidad.IdMedico = medico.IdMedico;
                medicoEspecialidad.IdEspecialidad = IdEspecialidad;
                context.Add(medicoEspecialidad);
                await context.SaveChangesAsync();



                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }
        public async Task<IActionResult> Edit (int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            
            //Cruzamos los datos para que cruce lso datos de la tabla medico con los registros de MedicoEspecialidad
            //El include es similar al inner join para cruzar datos a travez de los campos PK
            var medico = await context.Medicos.Where(m => m.IdMedico == id)
                .Include(med => med.MedicoEspecialidad).FirstOrDefaultAsync();

            if (medico==null)
            {
                return NotFound();
            }
            ViewData["ListaEspecialidades"] = new SelectList(
                context.Especialidades,"IdEspecialidad","Descripcion", medico.MedicoEspecialidad[0].IdEspecialidad);
                //le ponemos 0 porque obtenemos un solo objeto
                
            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind("IdMedico,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")]Medico medico, int idEspecialidad)
        {

            medico.IdMedico = id;
            if (id != medico.IdMedico || idEspecialidad==0 )
             {
                 return NotFound();
             }
            if (ModelState.IsValid)
            {
                try
                {
                    
                    context.Update(medico);
                    await context.SaveChangesAsync();
                    var medicoEspecialidad = await context.MedicoEspecialidad
                        .FirstOrDefaultAsync(me => me.IdMedico == id);
                    //NO ME DEJA SETEAR UNA PK ASIQUE LA REMOVEMOS Y DESPUES LA CREAMOS
                    context.Remove(medicoEspecialidad);
                    await context.SaveChangesAsync();
                    medicoEspecialidad.IdEspecialidad = idEspecialidad;
                    context.Add(medicoEspecialidad);
                    await context.SaveChangesAsync();
                }
                //Tratamiento de concurrencia
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteMedico(medico.IdMedico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var medico = await context.Medicos.FirstOrDefaultAsync(m => m.IdMedico==id);
            if (medico==null)
            {
                return NotFound();
            }
            return View(medico);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var medicoEspecialidad = await context.MedicoEspecialidad
                .FirstOrDefaultAsync(me => me.IdMedico == id);

            //medico especialidad es null, se ve que en la tabla MedicoEspecialidad no hay ninguno con ese id
            context.MedicoEspecialidad.Remove(medicoEspecialidad);
            await context.SaveChangesAsync();

            var medico = await context.Medicos.FindAsync(id);

            context.Medicos.Remove(medico);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //Busca el medico que recibe como parametro y devuelve si lo encontro o no
           private bool ExisteMedico(int id)
        {
            return context.Medicos.Any(e => e.IdMedico == id);
        } 
        public string TraerHorarioAtencionDesde(int idMedico)
        {
            var HorarioAtencionDesde = context.Medicos.Where(m => m.IdMedico == idMedico).FirstOrDefault().HorarioAtencionDesde;
            return HorarioAtencionDesde.Hour + ":" + HorarioAtencionDesde.Minute;
        }
        public string TraerHorarioAtencionHasta(int idMedico)
        {
            var HorarioAtencionHasta = context.Medicos.Where(m => m.IdMedico == idMedico).FirstOrDefault().HorarioAtencionHasta;
            return HorarioAtencionHasta.Hour + ":" + HorarioAtencionHasta.Minute;
        }
    }
}
