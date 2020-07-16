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
    public class AmbienteController : ControllerBase
    {
        private readonly IAmbienteService _service;
        private readonly IMapper _mapper;

        public AmbienteController(IAmbienteService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        /// <summary>
        /// Lista todos os Ambientes
        /// </summary>
        /// <response code="200">Dados dos Ambientes</response>   
        /// <response code="401">Não autorizado</response>        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AmbienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<AmbienteDTO>> GetAll()
        {
            var result = this._service.BuscarTodos()
                .Select(a => this._mapper.Map<Ambiente, AmbienteDTO>(a))
                .ToList();

            return Ok(result);
        }

        /// <summary>
        /// Lista apenas o Ambiente informado
        /// </summary>
        /// <response code="200">Dados do Ambiente</response>
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Ambiente não encontrado</response>        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AmbienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AmbienteDTO> Get(int id)
        {
            var result = this._service.BuscarPorId(id);

            if (result == null)
                return NotFound();
            else
                return Ok(this._mapper.Map<Ambiente, AmbienteDTO>(result));

        }

        /// <summary>
        /// Inclui um novo Ambiente
        /// </summary>
        /// <response code="201">Ambiente criado</response>
        /// <response code="400">Os dados informados são inválidos</response>        
        /// <response code="401">Não autorizado</response>        
        [HttpPost]
        [ProducesResponseType(typeof(AmbienteDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<AmbienteDTO> Inserir([FromBody, BindRequired] AmbienteDTO ambiente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = this._mapper.Map<AmbienteDTO, Ambiente>(ambiente);

            obj.Id = 0;

            var result = this._service.Salvar(obj);

            return Created($"{Request.Path}/{result.Id}", this._mapper.Map<Ambiente, AmbienteDTO>(result));
        }

        /// <summary>
        /// Modifica um Ambiente
        /// </summary>
        /// <response code="200">Dados do Ambiente alterados com sucesso</response>
        /// <response code="400">Os dados informados são inválidos</response>
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Ambiente não encontrado</response>        
        [HttpPut]
        [ProducesResponseType(typeof(AmbienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AmbienteDTO> Alterar([FromBody, BindRequired] AmbienteDTO ambiente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!this._service.ExistePorId(ambiente.Id))
                return NotFound("Tipo Log não encontrado. Impossível alterar");

            var obj = this._mapper.Map<AmbienteDTO, Ambiente>(ambiente);

            var result = this._service.Salvar(obj);

            return Ok(this._mapper.Map<Ambiente, AmbienteDTO>(result));
        }

        /// <summary>
        /// Remove o Ambiente informado
        /// </summary>
        /// <response code="204">Ambiente removido com sucesso</response>        
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Ambiente não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Remover(int id)
        {
            var obj = this._service.BuscarPorId(id);

            if (obj == null)
                return NotFound("Tipo Log não encontrado. Impossível remover");


            this._service.Remover(obj);

            return NoContent();
        }

    }

}
