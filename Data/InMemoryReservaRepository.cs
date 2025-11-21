using System;
using System.Collections.Generic;
using System.Linq;
using CampusTech.Reservas.Models;

namespace CampusTech.Reservas.Data
{
    public class InMemoryReservaRepository : IReservaRepository
    {
        private readonly List<Reserva> _reservas = new();
        private int _nextId = 1;

        public IEnumerable<Reserva> GetAll() => _reservas.OrderBy(r => r.FechaReserva).ThenBy(r => r.HoraInicio);

        public Reserva GetById(int id) => _reservas.FirstOrDefault(r => r.Id == id);

        public bool CodigoExiste(string codigo) => _reservas.Any(r => string.Equals(r.CodigoReserva, codigo, StringComparison.OrdinalIgnoreCase));

        public void Add(Reserva reserva)
        {
            reserva.Id = _nextId++;
            _reservas.Add(reserva);
        }
    }
}
