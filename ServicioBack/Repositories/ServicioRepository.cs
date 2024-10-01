using Microsoft.EntityFrameworkCore;
using ServicioBack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly TurnosDbContext _context;
        public ServicioRepository(TurnosDbContext context)
        {
            _context = context;
        }
        public async Task<List<TServicio>> ConsultsFiltersAsync(string promo, string nombre)
        {
            var query = _context.TServicios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(s => s.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrWhiteSpace(promo))
            {
                query = query.Where(s => s.EnPromocion.Contains(promo));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> DeleteLogicAsync(int id)
        {
            var servicio = await _context.TServicios.FindAsync(id);
            if (servicio == null)
            {
                return false;
            }
            servicio.Estado = false;
            _context.TServicios.Update(servicio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TServicio>> GetAll()
        {
            try
            {
                return await _context.TServicios.Where(s => s.Estado == true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener servicios", ex);
            }
        }

        public async Task<bool> SaveAsync(TServicio servicio)
        {
            try
            {
                if (servicio.Id == 0)
                {
                    servicio.Id = await GetNextId();
                    await _context.TServicios.AddAsync(servicio);
                }
                else
                {
                    _context.TServicios.Update(servicio);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public async Task<int> GetNextId()
        {
            return await _context.TServicios.MaxAsync(s => s.Id + 1);
        }


    }
}
