using LajkMikroservis.DTOs.LikeTipDTO;
using LajkMikroservis.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LajkMikroservis.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/like-tip")]
    public class LikeTipController : ControllerBase
    {
        private readonly ILikeTipRepository _repository;

        public LikeTipController(ILikeTipRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Svi tipovi lajka
        /// </summary>
        /// <returns>list tipova</returns>
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
        /// Tip po idu
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lajk tip</returns>
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
        /// Kreiranje novog tipa lajka
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>novi tip lajka</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult Post([FromBody] LikeTipCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update tipa lajka
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Novi tip lajka</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, LikeTipCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Brisanje tipa lajka po idu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _repository.Delete(id);

            return NoContent();
        }
    }
}
