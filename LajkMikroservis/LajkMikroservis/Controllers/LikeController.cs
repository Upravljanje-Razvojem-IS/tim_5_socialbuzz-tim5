using LajkMikroservis.DTOs.LikeDTO;
using LajkMikroservis.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LajkMikroservis.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/like")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _repository;

        public LikeController(ILikeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Svi lajkovi
        /// </summary>
        /// <returns>list lajkova</returns>
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
        /// Like po id-u
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lajk</returns>
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
        /// Kreiranje lajka
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>novi lajk</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult Post([FromBody] LikeCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update lajka
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Novi lajk</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, LikeCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Brisanje lajka po id-u
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

        /// <summary>
        /// Preuzimanje svih lajkova po id-u usera
        /// </summary>
        /// <param name="id"></param>
        /// <returns>lista lajkova</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("user/{id}")]
        public ActionResult GetByUserId(int id)
        {
            var list = _repository.GetAllByUserId(id);

            if (list.Count == 0)
                return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Preuzimanje svih lajkova po id-u posta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista lajkova</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("post/{id}")]
        public ActionResult GetByPostId(int id)
        {
            var list = _repository.GetAllByPostId(id);

            if (list.Count == 0)
                return NoContent();

            return Ok(list);
        }
    }
}
