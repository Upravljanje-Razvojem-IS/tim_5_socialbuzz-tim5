using IsporukaService.DTOs.LokacijaDTOs;
using IsporukaService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IsporukaService.Controllers
{
    [ApiController]
    [Route("api/lokacija")]
    [Authorize]
    public class LokacijaController : ControllerBase
    {
        private readonly ILokacijaRepository _repository;

        public LokacijaController(ILokacijaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Uzimanje svih lokacija
        /// </summary>
        /// <returns>Lista lokacija</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAll()
        {
            var entities = _repository.Get();

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Lokacija po Id-u
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lokacija</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Kreiranje lokacije
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>nova lokacija</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult PostCoorporate([FromBody] LokacijaCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update lokacije
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>nova lokacija</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult PutCoorporate(Guid id, LokacijaCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Brisanje lokacije
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public ActionResult DeleteCoorporate(Guid id)
        {
            _repository.Delete(id);

            return NoContent();
        }
    }
}
