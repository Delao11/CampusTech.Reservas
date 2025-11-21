using System;
using Microsoft.AspNetCore.Mvc;
using CampusTech.Reservas.Data;
using CampusTech.Reservas.Models;

namespace CampusTech.Reservas.Controllers
{
    public class ReservasController : Controller
    {
        private readonly IReservaRepository _repo;

        public ReservasController(IReservaRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var lista = _repo.GetAll();
            return View(lista);
        }

        public IActionResult Create()
        {
            return View(new Reserva { FechaReserva = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva reserva)
        {
            // Validaciones manuales adicionales

            // 1) Laboratorio no sea None
            if (reserva.Laboratorio == Laboratorio.None)
            {
                ModelState.AddModelError(nameof(reserva.Laboratorio), "Debe seleccionar un laboratorio válido.");
            }

            // 2) Fecha no en el pasado
            var hoy = DateTime.Today;
            if (reserva.FechaReserva.Date < hoy)
            {
                ModelState.AddModelError(nameof(reserva.FechaReserva), "La fecha no puede ser anterior a la fecha actual.");
            }

            // 3) Horas: HoraFin > HoraInicio y duración > 0
            if (reserva.HoraFin <= reserva.HoraInicio)
            {
                ModelState.AddModelError(nameof(reserva.HoraFin), "La hora de fin debe ser mayor que la hora de inicio.");
            }

            // 4) Código único
            if (_repo.CodigoExiste(reserva.CodigoReserva))
            {
                ModelState.AddModelError(nameof(reserva.CodigoReserva), "El código de reserva ya existe. Debe ser único.");
            }

            if (!ModelState.IsValid)
            {
                return View(reserva);
            }

            // Todo OK: agregar
            _repo.Add(reserva);
            TempData["SuccessMessage"] = "Reserva creada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var r = _repo.GetById(id);
            if (r == null) return NotFound();
            return View(r);
        }
    }
}
