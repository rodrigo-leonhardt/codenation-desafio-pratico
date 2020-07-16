using Microsoft.EntityFrameworkCore;
using Source.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly LogContext _context;

        public UsuarioService(LogContext context)
        {
            this._context = context;
        }

        public Usuario BuscarPorId(int id)
        {
            return this._context.Usuarios
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            return this._context.Usuarios.ToList();
        }

        public bool ExistePorId(int id)
        {
            return this._context.Usuarios
                .Any(t => t.Id == id);
        }

        public Usuario Salvar(Usuario usuario)
        {
            var state = usuario.Id == 0 ? EntityState.Added : EntityState.Modified;

            this._context.Entry(usuario).State = state;
            this._context.SaveChanges();

            return usuario;
        }

        public void Remover(Usuario usuario)
        {
            this._context.Usuarios.Remove(usuario);
            this._context.SaveChanges();
        }

        public bool ExistePorEmailSenha(string email, string senha)
        {
            return this._context.Usuarios
                .Any(t => t.Email == email && t.Senha == senha);
        }

        public Usuario BuscarPorEmailSenha(string email, string senha)
        {
            return this._context.Usuarios
                .FirstOrDefault(t => t.Email == email && t.Senha == senha);
        }

        public string GerarToken(Usuario usuario)
        {
            return TokenService.GenerateToken(usuario.Email, usuario.Id.ToString());
        }

    }
}
