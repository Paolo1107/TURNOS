using ServicioBack.Entities.Models;
using ServicioBack.Repositories.Turnos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Services.Turnos
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _repoTurno;
        public TurnoService(ITurnoRepository turnoRepository)
        {
            _repoTurno = turnoRepository;
        }

        public Task<bool> AddTurnos(TTurno turno)
        {
            return _repoTurno.AddTurnos(turno);
        }

        public Task<List<TTurno>> GetAllTurnos()
        {
            return _repoTurno.GetAllTurnos();
        }

        public Task<List<TTurno>> GetTurnosFilters(string cliente, DateTime fecha, string hora)
        {
            return _repoTurno.GetTurnosFilters(cliente, fecha, hora);
        }

        public Task<bool> LowTurnos(int id_turno, int id_servicio, string motivo)
        {
            return _repoTurno.LowTurnos(id_turno, id_servicio, motivo);
        }

        public Task<bool> UpdateTurnos(TTurno turno, int id)
        {
            return _repoTurno.UpdateTurnos(turno, id);
        }
    }
}
