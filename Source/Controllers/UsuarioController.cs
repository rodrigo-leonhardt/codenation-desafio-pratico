using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Source.DTOs;
using Source.Models;
using Source.Services;
using System.Collections.Generic;
using System.Linq;

namespace Source.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]    
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        /// <summary>
        /// Lista todos os Usuarios
        /// </summary>
        /// <response code="200">Dados dos Usuários</response>        
        /// <response code="401">Não autorizado</response>        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<UsuarioDTO>> GetAll()
        {
            var result = this._service.BuscarTodos()
                .Select(u => this._mapper.Map<Usuario, UsuarioDTO>(u))
                .ToList();

            return Ok(result);
        }

        /// <summary>
        /// Lista apenas o Usuário informado
        /// </summary>
        /// <response code="200">Dados do Usuário</response>
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Usuário não encontrado</response>        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioDTO> Get(int id)
        {
            var result = this._service.BuscarPorId(id);

            if (result == null)
                return NotFound();
            else
                return Ok(this._mapper.Map<Usuario, UsuarioDTO>(result));

        }

        /// <summary>
        /// Inclui um novo Usuário
        /// </summary>
        /// <response code="201">Usuário criado</response>
        /// <response code="400">Os dados informados são inválidos</response>                
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UsuarioDTO> Inserir([FromBody, BindRequired] UsuarioDTO usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = this._mapper.Map<UsuarioDTO, Usuario>(usuario);

            obj.Id = 0;

            var result = this._service.Salvar(obj);

            return Created($"{Request.Path}/{result.Id}", this._mapper.Map<Usuario, UsuarioDTO>(result));
        }

        /// <summary>
        /// Modifica um Usuário
        /// </summary>
        /// <response code="200">Dados do Usuário alterados com sucesso</response>
        /// <response code="400">Os dados informados são inválidos</response>
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Usuário não encontrado</response>        
        [HttpPut]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioDTO> Alterar([FromBody, BindRequired] UsuarioDTO usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!this._service.ExistePorId(usuario.Id))
                return NotFound("Usuário não encontrado. Impossível alterar");

            var obj = this._mapper.Map<UsuarioDTO, Usuario>(usuario);

            var result = this._service.Salvar(obj);

            return Ok(this._mapper.Map<Usuario, UsuarioDTO>(result));
        }

        /// <summary>
        /// Remove o Usuário informado
        /// </summary>
        /// <response code="204">Usuário removido com sucesso</response>   
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Usuário não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Remover(int id)
        {
            var obj = this._service.BuscarPorId(id);

            if (obj == null)
                return NotFound("Usuário não encontrado. Impossível remover");


            this._service.Remover(obj);

            return NoContent();
        }

        /// <summary>
        /// Realiza login
        /// </summary>
        /// <response code="200">Login realizado</response>
        /// <response code="400">Os dados informados são inválidos</response>                 
        /// <response code="401">Login não autorizado</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]     
        [Produces("text/plain")]
        public ActionResult<string> Login([FromBody, BindRequired] UsuarioLoginDTO login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = this._service.BuscarPorEmailSenha(login.Email, login.Senha);

            if (obj == null)
                return Unauthorized();


            var result = this._service.GerarToken(obj);

            return Ok(result);
        }

    }

}
