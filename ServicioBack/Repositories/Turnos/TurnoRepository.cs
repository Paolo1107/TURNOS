using Microsoft.EntityFrameworkCore;
using ServicioBack.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServicioBack.Repositories.Turnos
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly TurnosDbContext _contextt;
        public TurnoRepository(TurnosDbContext context)
        {
            _contextt = context;
        }

        public async Task<bool> AddTurnos(TTurno turno)
        {
            try
            {
           
                if (turno.TDetallesTurnos == null || !turno.TDetallesTurnos.Any())
                {
                    throw new Exception("Debe ingresar al menos un servicio.");
                }

          
                var serviciosDuplicados = turno.TDetallesTurnos
                    .GroupBy(d => d.IdServicio)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (serviciosDuplicados.Any())
                {
                    throw new Exception($"Los siguientes servicios están duplicados: {string.Join(", ", serviciosDuplicados)}");
                }

             
                var existeTurno = await _contextt.TTurnos
                    .AnyAsync(t => t.Fecha == turno.Fecha && t.Hora == turno.Hora && t.Cliente == turno.Cliente);

                if (existeTurno)
                {
                    throw new Exception("Ya existe un turno para el cliente en la misma fecha y hora.");
                }

             
                var fechaMinima = DateTime.Now.AddDays(1);
                var fechaMaxima = DateTime.Now.AddDays(45);

                if (turno.Fecha < fechaMinima || turno.Fecha > fechaMaxima)
                {
                    Console.WriteLine("Error, el turno no puede ser reservado con más de 45 días o en una fecha anterior a mañana.");
                    return false;
                }

                
                if (turno.Id == 0)
                {
                    await _contextt.TTurnos.AddAsync(turno);
                }

                await _contextt.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
           
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        public async Task<List<TTurno>> GetAllTurnos()
        {

            try
            {
                return await _contextt.TTurnos.ToListAsync();
            }
            catch (Exception)
            {

                throw new Exception("Error al obtener los turnos");
            }
        }

        public async Task<List<TTurno>> GetTurnosFilters(string cliente, DateTime fecha, string hora)
        {
            var filtros = _contextt.TTurnos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(cliente))
            {
                filtros = filtros.Where(c => c.Cliente.Contains(cliente));
            }


            filtros = filtros.Where(f => f.Fecha == fecha);

            if (!string.IsNullOrWhiteSpace(hora))
            {
                filtros = filtros.Where(h => h.Hora.Contains(hora));
            }

            return await filtros.ToListAsync();
        }

        public async Task<bool> LowTurnos(int id_turno, int id_servicio, string motivo)
        {

            var detalleTurno = await _contextt.TDetallesTurnos.FindAsync(id_turno, id_servicio);


            if (detalleTurno == null)
            {
                Console.WriteLine("El turno o el servicio no existen.");
                return false;
            }


            detalleTurno.Observaciones = motivo;


            return await _contextt.SaveChangesAsync() > 0;
        }



        public async Task<bool> UpdateTurnos(TTurno turno, int id)
        {
            try
            {
                if (turno.Id != id)
                {
                    Console.WriteLine("Los id no son iguales");
                    return false;
                }
                {

                }
                var turnno = _contextt.TTurnos.Find(id);
                if (turno == null) return false;
                turnno.Fecha = turno.Fecha;
                turnno.Hora = turno.Hora;
                turnno.Cliente = turno.Cliente;
                _contextt.TTurnos.Update(turnno);
                return await _contextt.SaveChangesAsync() > 0;

            }
            catch (Exception)
            {

                throw new Exception("Error al actualizar el turno");
            }
        }
    }
}
