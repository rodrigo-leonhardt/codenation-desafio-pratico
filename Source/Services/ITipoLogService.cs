using Source.Models;
using System.Collections.Generic;

namespace Source.Services
{
    public interface ITipoLogService
    {
        TipoLog BuscarPorId(int id);
        bool ExistePorId(int id);
        IEnumerable<TipoLog> BuscarTodos();
        TipoLog Salvar(TipoLog tipolog);
        void Remover(TipoLog tipolog);
    }

}
