using Source.DTOs;
using Source.Models;
using System.Collections.Generic;

namespace Source.Services
{
    public enum ILogServiceOrdenacao { TipoLog = 1, Eventos = 2 };
    public interface ILogService
    {
        Log BuscarPorId(int id);
        LogDTO BuscarDTOPorId(int id);
        IEnumerable<LogDTO> BuscarDTO(int? tipolog_id, int? ambiente_id, string origem, string titulo, string detalhes, ILogServiceOrdenacao? ordenacao);        
        Log Salvar(Log log);
        void Remover(int log_id);
        void Arquivar(int log_id, bool value);
    }
}
