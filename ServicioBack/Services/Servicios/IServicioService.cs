using ServicioBack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Services.Servicios
{
    public interface IServicioService
    {
        Task<List<TServicio>> GetAll();
        Task<bool> SaveAsync(TServicio servicio);
        Task<bool> DeleteLogicAsync(int id);
        Task<List<TServicio>> ConsultsFiltersAsync(string promo, string nombre);

        Task<int> GetNextId();
    }
}
