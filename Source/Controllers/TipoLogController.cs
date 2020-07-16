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
    public class TipoLogController : ControllerBase
    {
        private readonly ITipoLogService _service;
        private readonly IMapper _mapper;

        public TipoLogController(ITipoLogService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        /// <summary>
        /// Lista todos os Tipos de Log
        /// </summary>
        /// <response code="200">Dados dos Tipos de Log</response>   
        /// <response code="401">Não autorizado</response>        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TipoLogDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<TipoLogDTO>> GetAll()
        {
            var result = this._service.BuscarTodos()
                .Select(t => this._mapper.Map<TipoLog, TipoLogDTO>(t))
                .ToList();

            return Ok(result);
        }

        /// <summary>
        /// Lista apenas o Tipo de Log informado
        /// </summary>
        /// <response code="200">Dados do Tipo de Log</response>
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Tipo de Log não encontrado</response>        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TipoLogDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TipoLogDTO> Get(int id)
        {
            var result = this._service.BuscarPorId(id);

            if (result == null)
                return NotFound();
            else
                return Ok(this._mapper.Map<TipoLog, TipoLogDTO>(result));

        }

        /// <summary>
        /// Inclui um novo Tipo de Log
        /// </summary>
        /// <response code="201">Tipo de Log criado</response>
        /// <response code="400">Os dados informados são inválidos</response>        
        /// <response code="401">Não autorizado</response>        
        [HttpPost]
        [ProducesResponseType(typeof(TipoLogDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipoLogDTO> Inserir([FromBody, BindRequired] TipoLogDTO tipolog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = this._mapper.Map<TipoLogDTO, TipoLog>(tipolog);

            obj.Id = 0;

            var result = this._service.Salvar(obj);

            return Created($"{Request.Path}/{result.Id}", this._mapper.Map<TipoLog, TipoLogDTO>(result));
        }

        /// <summary>
        /// Modifica um Tipo de Log
        /// </summary>
        /// <response code="200">Dados do Tipo de Log alterados com sucesso</response>
        /// <response code="400">Os dados informados são inválidos</response>
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Tipo de Log não encontrado</response>        
        [HttpPut]
        [ProducesResponseType(typeof(TipoLogDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TipoLogDTO> Alterar([FromBody, BindRequired] TipoLogDTO tipolog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!this._service.ExistePorId(tipolog.Id))
                return NotFound("Tipo Log não encontrado. Impossível alterar");

            var obj = this._mapper.Map<TipoLogDTO, TipoLog>(tipolog);

            var result = this._service.Salvar(obj);

            return Ok(this._mapper.Map<TipoLog, TipoLogDTO>(result));
        }

        /// <summary>
        /// Remove o Tipo de Log informado
        /// </summary>
        /// <response code="204">Tipo de Log removido com sucesso</response>        
        /// <response code="401">Não autorizado</response>        
        /// <response code="404">Tipo de Log não encontrado</response>
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
