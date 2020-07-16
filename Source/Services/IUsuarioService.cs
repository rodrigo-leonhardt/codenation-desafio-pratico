using Source.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source.Services
{
    public interface IUsuarioService
    {
        Usuario BuscarPorId(int id);
        bool ExistePorId(int id);
        Usuario BuscarPorEmailSenha(string email, string senha);
        bool ExistePorEmailSenha(string email, string senha);
        IEnumerable<Usuario> BuscarTodos();
        Usuario Salvar(Usuario usuario);
        void Remover(Usuario usuario);
        string GerarToken(Usuario usuario);

    }
}
