using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Models.TwoDigitSystemModels;
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
        /// <summary>
        ///create a new TwoDigitSystem
        /// </summary>
        [HttpPost]
        public ActionResult Create([FromBody] CreateTwoDigitSystemDto createTwoDigitSystemDto) 
        {
            var id = _twoDigitSystemService.Create(createTwoDigitSystemDto);
            return Created($"/api/TwoDigitSystem/{id}", null);
        }
        /// <summary>
        ///delete TwoDigitSystem by TwoDigitSystem ID
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _twoDigitSystemService.Delete(id);

            return NoContent();
        }
        /// <summary>
        ///get all TwoDigitSystems, only for admins
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public ActionResult<IEnumerable<TwoDigitSystemDto>> GetAll([FromQuery]TwoDigitSystemQuery twoDigitSystemQuery)
        {
            var twoDigitSystemsDtos = _twoDigitSystemService.GetAll(twoDigitSystemQuery);

            return Ok(twoDigitSystemsDtos);
        }
        /// <summary>
        ///get TwoDigitSystem by TwoDigitSystem ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<TwoDigitSystemDto> GetById([FromRoute] int id)
        {
            var twoDigitSystemsDto = _twoDigitSystemService.GetById(id);
            return Ok(twoDigitSystemsDto);
        }
        /// <summary>
        ///get TwoDigitSystem by ID of the user who created it
        /// </summary>
        [HttpGet("UserId/{userid}/")]
        public ActionResult<TwoDigitSystemDto> GetByCreatedById([FromRoute] int userId)
        {
            var twoDigitSystemsDto = _twoDigitSystemService.GetByUserId(userId);
            return Ok(twoDigitSystemsDto);
        }
        /// <summary>
        ///update TwoDigitSystem by TwoDigitSystem ID
        /// </summary>>
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateTwoDigitSystemDto createTwoDigitSystemDto, [FromRoute]int id)
        {
            _twoDigitSystemService.Update(id, createTwoDigitSystemDto);
            return Ok();
        }
    }
}
