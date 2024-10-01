using ServicioBack.Entities.Models;
using ServicioBack.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _repository;
        public ServicioService(IServicioRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TServicio>> ConsultsFiltersAsync(string promo, string nombre)
        {
            return await _repository.ConsultsFiltersAsync(promo, nombre);
        }

        public Task<bool> DeleteLogicAsync(int id)
        {
            return _repository.DeleteLogicAsync(id);
        }

        public async Task<List<TServicio>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> SaveAsync(TServicio servicio)
        {
            return await _repository.SaveAsync(servicio);
        }

        public async Task<int> GetNextId()
        {
            return await _repository.GetNextId();
        }
    }
}
