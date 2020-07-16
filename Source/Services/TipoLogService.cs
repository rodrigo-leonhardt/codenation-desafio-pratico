using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Source.Models;

namespace Source.Services
{
    public class TipoLogService : ITipoLogService
    {
        private readonly LogContext _context;

        public TipoLogService(LogContext context)
        {
            this._context = context;
        }

        public TipoLog BuscarPorId(int id)
        {
            return this._context.TiposLog
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<TipoLog> BuscarTodos()
        {
            return this._context.TiposLog.ToList();
        }

        public bool ExistePorId(int id)
        {
            return this._context.TiposLog
                .Any(t => t.Id == id);
        }

        public TipoLog Salvar(TipoLog tipolog)
        {
            var state = tipolog.Id == 0 ? EntityState.Added : EntityState.Modified;

            this._context.Entry(tipolog).State = state;
            this._context.SaveChanges();

            return tipolog;
        }

        public void Remover(TipoLog tipolog)
        {
            this._context.TiposLog.Remove(tipolog);
            this._context.SaveChanges();
        }

    }
}
