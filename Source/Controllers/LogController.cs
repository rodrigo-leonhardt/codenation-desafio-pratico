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
using System.Security.Claims;

namespace Source.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _service;
        private readonly IMapper _mapper;

        public LogController(ILogService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }        

        /// <summary>
        /// Lista os Logs
        /// </summary>
        /// <param name="ordenacao">
        /// 1 = Tipo de Log         
        /// 2 = Eventos
        /// </param>
        /// <response code="200">Dados dos Logs</response>   
        /// <response code="401">Não autorizado</response>        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LogDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<LogDTO>> Get(int? tipolog_id, int? ambiente_id, string origem, string titulo, string detalhes, ILogServiceOrdenacao? ordenacao)
        {
            var result = this._service.BuscarDTO(tipolog_id, ambiente_id, origem, titulo, detalhes, ordenacao);

            return Ok(result);
        }

        /// <summary>
        /// Lista apenas o Log informado
        /// </summary>
        /// <response code="200">Dados do Log</response>
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Log não encontrado</response>        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AmbienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LogDTO> Get(int id)
        {
            var result = this._service.BuscarDTOPorId(id);

            if (result == null)
                return NotFound();
            else
                return Ok(result);

        }

        /// <summary>
        /// Inclui um novo Log
        /// </summary>
        /// <response code="201">Log criado</response>
        /// <response code="400">Os dados informados são inválidos</response>        
        /// <response code="401">Não autorizado</response>        
        [HttpPost]
        [ProducesResponseType(typeof(LogInserirDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<LogInserirDTO> Inserir([FromBody, BindRequired] LogInserirDTO log)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = this._mapper.Map<LogInserirDTO, Log>(log);

            obj.Id = 0;
            obj.Arquivado = false;
            obj.UsuarioId = User.Claims
                                .Where(c => c.Type == ClaimTypes.Sid)
                                .Select(c => int.Parse(c.Value))
                                .FirstOrDefault();

            var result = this._service.Salvar(obj);

            return Created($"{Request.Path}/{result.Id}", this._mapper.Map<Log, LogInserirDTO>(result));
        }

        /// <summary>
        /// Remove o Log informado
        /// </summary>
        /// <response code="204">Log removido com sucesso</response>        
        /// <response code="401">Não autorizado</response>                
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        public ActionResult Remover(int id)
        {
            this._service.Remover(id);
            
            return NoContent();
        }

        /// <summary>
        /// Arquiva o Log informado
        /// </summary>
        /// <response code="204">Log arquivado com sucesso</response>        
        /// <response code="401">Não autorizado</response>                
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Arquivar(int id)
        {
            this._service.Arquivar(id, true);

            return NoContent();
        }

    }

}
