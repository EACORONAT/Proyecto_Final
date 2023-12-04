using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using MvcCongresoTIC.Data;
using MvcCongresoTIC.Models;

namespace MvcCongresoTIC.Controllers
{
    public class ConferenciasController : Controller
    {
        private readonly MvcCongresoTICContext _context;
        public ConferenciasController(MvcCongresoTICContext context)
        {
            _context = context;
        }        

        // GET: Conferencias
        public async Task<IActionResult> Index()
        {
              return _context.Conferencia != null ? 
                          View(await _context.Conferencia.ToListAsync()) :
                          Problem("Entity set 'MvcCongresoTICContext.Conferencia'  is null.");
        }

        // GET: Conferencias/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Conferencia == null)
            {
                return NotFound();
            }

            var conferencia = await _context.Conferencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conferencia == null)
            {
                return NotFound();
            }

            return View(conferencia);
        }

        // GET: Conferencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conferencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Horario,NombreConferencista")] Conferencia conferencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conferencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conferencia);
        }

        // GET: Conferencias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Conferencia == null)
            {
                return NotFound();
            }

            var conferencia = await _context.Conferencia.FindAsync(id);
            if (conferencia == null)
            {
                return NotFound();
            }
            return View(conferencia);
        }

        // POST: Conferencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Horario,NombreConferencista")] Conferencia conferencia)
        {
            if (id != conferencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conferencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenciaExists(conferencia.Id))
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
            return View(conferencia);
        }

        // GET: Conferencias/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Conferencia == null)
            {
                return NotFound();
            }

            var conferencia = await _context.Conferencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conferencia == null)
            {
                return NotFound();
            }

            return View(conferencia);
        }

        // POST: Conferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conferencia == null)
            {
                return Problem("Entity set 'MvcCongresoTICContext.Conferencia'  is null.");
            }
            var conferencia = await _context.Conferencia.FindAsync(id);
            if (conferencia != null)
            {
                _context.Conferencia.Remove(conferencia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenciaExists(int id)
        {
          return (_context.Conferencia?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: RegistrarAsistencia
        [HttpGet]
        public IActionResult RegistrarAsistencia(int conferenciaId)
        {
            //var conferencia = _context.Conferencia.Find(conferenciaId);
            var conferencia = _context.Conferencia
                .Include(c => c.Asistentes)
                .Include(c => c.Asistentes).ThenInclude(r => r.Participante)
                .FirstOrDefault(c => c.Id == conferenciaId);

            if (conferencia == null)
            {
                return NotFound(); // Maneja el caso en que la conferencia no existe
            }

            var participantes = _context.Participante.ToList(); // Obtén la lista de participantes

            var viewModel = new RegistroConferenciaVM
            {
                ConferenciaId = conferencia.Id,
                ConferenciaTitulo = conferencia.Titulo,
                Participantes = participantes,
                ConfirmacionAsistencia = conferencia.ConfirmacionAsistencia
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RegistrarAsistencia(RegistroConferenciaVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar si ya existe un registro para la misma conferencia y participante
                    var existingRegistro = _context.RegistrosConferencias
                        .FirstOrDefault(r => r.ConferenciaId == viewModel.ConferenciaId && r.ParticipanteId == viewModel.ParticipanteId);

                    if (existingRegistro != null)
                    {
                        // Ya existe un registro, mostrar mensaje de alerta
                        TempData["Alerta"] = "El participante ya está registrado para esta conferencia";

                        // Redirigir a la misma vista
                        viewModel.Participantes = _context.Participante.ToList();
                        return View(viewModel);
                    }

                    // No existe un registro, proceder con el registro
                    var registro = new RegistroConferencia
                    {
                        ConferenciaId = viewModel.ConferenciaId,
                        ParticipanteId = viewModel.ParticipanteId,
                        ConfirmacionAsistencia = viewModel.ConfirmacionAsistencia
                    };

                    _context.RegistrosConferencias.Add(registro);
                    _context.SaveChanges();

                    // Agregar el registro a la colección de asistentes de la conferencia
                    var conferencia = _context.Conferencia
                        .Include(c => c.Asistentes)
                        .FirstOrDefault(c => c.Id == viewModel.ConferenciaId);

                    conferencia?.Asistentes.Add(registro);
                    _context.SaveChanges();

                    // Redirigir a la página de confirmación u otra página según sea necesario
                    //return RedirectToAction(nameof(ListadoAsistentes));
                    return RedirectToAction("Listado", new { conferenciaId = viewModel.ConferenciaId });
                }
                catch (Exception ex)
                {
                    // Capturar la excepción de SQL
                    TempData["Alerta"] = "Error al guardar en la base de datos: " + ex.Message;

                    // Puedes loguear la excepción si es necesario
                    // Log.Error($"Error al guardar en la base de datos: {ex.Message}", ex);

                    // Redirigir a la misma vista
                    viewModel.Participantes = _context.Participante.ToList();
                    return View(viewModel);
                }
            }

            // Si el modelo no es válido, vuelve a mostrar el formulario con errores
            viewModel.Participantes = _context.Participante.ToList();
            return View(viewModel);
        }


        //[HttpPost]
        //public IActionResult RegistrarAsistencia(RegistroConferenciaVM viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var registro = new RegistroConferencia
        //            {
        //                ConferenciaId = viewModel.ConferenciaId,
        //                ParticipanteId = viewModel.ParticipanteId,
        //                ConfirmacionAsistencia = viewModel.ConfirmacionAsistencia
        //            };

        //            _context.RegistrosConferencias.Add(registro);
        //            _context.SaveChanges();

        //            // Agregar el registro a la colección de asistentes de la conferencia
        //            var conferencia = _context.Conferencia
        //                .Include(c => c.Asistentes)
        //                .FirstOrDefault(c => c.Id == viewModel.ConferenciaId);

        //            conferencia?.Asistentes.Add(registro);
        //            _context.SaveChanges();

        //            // Redirigir a la página de confirmación u otra página según sea necesario
        //            //return RedirectToAction(nameof(ListadoAsistentes));
        //            return RedirectToAction("Listado", new { conferenciaId = viewModel.ConferenciaId });
        //        }catch (Exception ex)
        //        {
        //            // Capturar la excepción de SQL
        //            TempData["Alerta"] = "El participante ya se encuentra registrado";

        //            // Puedes loguear la excepción si es necesario
        //            // Log.Error($"Error al guardar en la base de datos: {ex.Message}", ex);

        //            // Redirigir a la misma vista
        //            viewModel.Participantes = _context.Participante.ToList();
        //            return View(viewModel);
        //        }
        //    }

        //    // Si el modelo no es válido, vuelve a mostrar el formulario con errores
        //    viewModel.Participantes = _context.Participante.ToList();
        //    return View(viewModel);
        //}

        // GET: Listado        
        public IActionResult Listado(int conferenciaId)
        {
            var conferencia = _context.Conferencia
                .Include(c => c.Asistentes)
                    .ThenInclude(r => r.Participante)
                .FirstOrDefault(c => c.Id == conferenciaId);

            if (conferencia == null)
            {
                return NotFound();
            }

            return View(conferencia);
        }
    }
}