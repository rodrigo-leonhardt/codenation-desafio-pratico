using Source.Models;
using System.Collections.Generic;

namespace Source.Services
{
    public interface IAmbienteService
    {
        Ambiente BuscarPorId(int id);
        bool ExistePorId(int id);
        IEnumerable<Ambiente> BuscarTodos();
        Ambiente Salvar(Ambiente ambiente);
        void Remover(Ambiente ambiente);

    }
}
