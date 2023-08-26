using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Models;
using MemoryPalaceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryPalaceAPI.Controllers
{
    [Route("api/TwoDigitSystem")]
    [ApiController] //this annotation is used to auto validate a DTO
    [Authorize]

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
            var id = _twoDigitSystemService.Create(createTwoDigitSystemDto);
            return Created($"/api/TwoDigitSystem/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _twoDigitSystemService.Delete(id);

            return NoContent();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public ActionResult<IEnumerable<TwoDigitSystemDto>> GetAll()
        {
            var twoDigitSystemsDtos = _twoDigitSystemService.GetAll();

            return Ok(twoDigitSystemsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<TwoDigitSystemDto> GetById([FromRoute] int id)
        {
            var twoDigitSystemsDto = _twoDigitSystemService.GetById(id);
            return Ok(twoDigitSystemsDto);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateTwoDigitSystemDto createTwoDigitSystemDto, [FromRoute]int id)
        {
            _twoDigitSystemService.Update(id, createTwoDigitSystemDto);
            return Ok();
        }
    }
}
