using ServicioBack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Services.Turnos
{
    public interface ITurnoService
    {
        Task<List<TTurno>> GetAllTurnos();
        Task<List<TTurno>> GetTurnosFilters(string cliente, DateTime fecha, string hora);
        Task<bool> AddTurnos(TTurno turno);
        Task<bool> UpdateTurnos(TTurno turno, int id);
        Task<bool> LowTurnos(int id_turno, int id_servicio, string motivo);
    }
}
