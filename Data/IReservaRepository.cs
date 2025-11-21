using System.Collections.Generic;
using CampusTech.Reservas.Models;

namespace CampusTech.Reservas.Data
{
    public interface IReservaRepository
    {
        IEnumerable<Reserva> GetAll();
        Reserva GetById(int id);
        bool CodigoExiste(string codigo);
        void Add(Reserva reserva);
    }
}
