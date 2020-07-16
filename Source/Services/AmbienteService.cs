using Microsoft.EntityFrameworkCore;
using Source.Models;
using System.Collections.Generic;
using System.Linq;

namespace Source.Services
{
    public class AmbienteService : IAmbienteService
    {
        private readonly LogContext _context;

        public AmbienteService(LogContext context)
        {
            this._context = context;
        }

        public Ambiente BuscarPorId(int id)
        {
            return this._context.Ambientes
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Ambiente> BuscarTodos()
        {
            return this._context.Ambientes.ToList();
        }

        public bool ExistePorId(int id)
        {
            return this._context.Ambientes
                .Any(t => t.Id == id);
        }

        public Ambiente Salvar(Ambiente ambiente)
        {
            var state = ambiente.Id == 0 ? EntityState.Added : EntityState.Modified;

            this._context.Entry(ambiente).State = state;
            this._context.SaveChanges();

            return ambiente;
        }

        public void Remover(Ambiente ambiente)
        {
            this._context.Ambientes.Remove(ambiente);
            this._context.SaveChanges();
        }

    }
}
