using Microsoft.EntityFrameworkCore;
using Source.DTOs;
using Source.Models;
using System.Collections.Generic;
using System.Linq;

namespace Source.Services
{
    public class LogService : ILogService
    {
        private readonly LogContext _context;

        public LogService(LogContext context)
        {
            this._context = context;
        }

        public Log BuscarPorId(int id)
        {
            return this._context.Logs
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public LogDTO BuscarDTOPorId(int id)
        {
            return this._context.Logs
                .Where(t => t.Id == id)
                .Select(l => new LogDTO
                {
                    Id = l.Id,
                    Arquivado = l.Arquivado,
                    Origem = l.Origem,
                    Data = l.Data,
                    Titulo = l.Titulo,
                    Detalhes = l.Detalhes,
                    Eventos = l.Eventos,
                    TipoId = l.TipoId,
                    Tipo = l.Tipo.Titulo,
                    AmbienteId = l.AmbienteId,
                    Ambiente = l.Ambiente.Titulo,
                    UsuarioId = l.UsuarioId,
                    Usuario = l.Usuario.Email
                })
                .FirstOrDefault();
        }

        public IEnumerable<LogDTO> BuscarDTO(int? tipolog_id, int? ambiente_id, string origem, string titulo, string detalhes, ILogServiceOrdenacao? ordenacao)
        {            
            var result = this._context.Logs.AsQueryable();

            if (tipolog_id.HasValue)
                result = result.Where(l => l.TipoId == tipolog_id);

            if (ambiente_id.HasValue)
                result = result.Where(l => l.AmbienteId == ambiente_id);

            if (origem != null)
                result = result.Where(l => l.Origem.Contains(origem));

            if (titulo != null)
                result = result.Where(l => l.Titulo.Contains(titulo));

            if (detalhes != null)
                result = result.Where(l => l.Detalhes.Contains(detalhes));

            if (ordenacao.HasValue)
            {

                if (ordenacao == ILogServiceOrdenacao.Eventos)
                    result = result.OrderByDescending(l => l.Eventos);
                else
                    result = result.OrderBy(l => l.Tipo.Titulo);

            }

            return result
                .Select(l => new LogDTO
                {
                    Id = l.Id,
                    Arquivado = l.Arquivado,
                    Origem = l.Origem,
                    Data = l.Data,
                    Titulo = l.Titulo,
                    Detalhes = l.Detalhes,
                    Eventos = l.Eventos,
                    TipoId = l.TipoId,
                    Tipo = l.Tipo.Titulo,
                    AmbienteId = l.AmbienteId,
                    Ambiente = l.Ambiente.Titulo,
                    UsuarioId = l.UsuarioId,
                    Usuario = l.Usuario.Email
                })
                .ToList();
        }        

        public Log Salvar(Log log)
        {
            var state = log.Id == 0 ? EntityState.Added : EntityState.Modified;

            this._context.Entry(log).State = state;
            this._context.SaveChanges();

            return log;
        }

        public void Remover(int log_id)
        {
            var log = this.BuscarPorId(log_id);

            if (log == null)
                return;

            this._context.Logs.Remove(log);
            this._context.SaveChanges();
        }

        public void Arquivar(int log_id, bool value)
        { 
            var log = this.BuscarPorId(log_id);

            if (log == null)
                return;

            log.Arquivado = value;

            this.Salvar(log);            
        }

}
}
