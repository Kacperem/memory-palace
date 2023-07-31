using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Models;
using MemoryPalaceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryPalaceAPI.Controllers
{
    [Route("api/TwoDigitSystem")]
    [ApiController]

    public class TwoDigitSystemController : ControllerBase
    {
        private readonly ITwoDigitSystemService _twoDigitSystemService;
        public TwoDigitSystemController(ITwoDigitSystemService twoDigitSystemService)
        {
            _twoDigitSystemService = twoDigitSystemService;
        }
        [HttpPost]
        public ActionResult Create([FromBody] CreateTwoDigitSystemDto createTwoDigitSystemDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _twoDigitSystemService.Create(createTwoDigitSystemDto);
            return Created($"/api/TwoDigitSystem/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _twoDigitSystemService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
        [HttpGet]
        public ActionResult<IEnumerable<TwoDigitSystemDto>> GetAll()
        {
            var twoDigitSystemsDtos = _twoDigitSystemService.GetAll();

            return Ok(twoDigitSystemsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<TwoDigitSystemDto> GetById([FromRoute] int id)
        {
            var twoDigitSystemsDto = _twoDigitSystemService.GetById(id);
            if (twoDigitSystemsDto is null) return NotFound(); ;
            
            return Ok(twoDigitSystemsDto);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateTwoDigitSystemDto createTwoDigitSystemDto, [FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _twoDigitSystemService.Update(id, createTwoDigitSystemDto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
