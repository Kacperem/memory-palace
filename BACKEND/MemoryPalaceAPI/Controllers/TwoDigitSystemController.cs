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
        /// <response code="201">TwoDigitSystem created successfully</response>
        /// <response code="400">TwoDigitSystem validation error</response>
        /// <response code="401">Invalid or missing authentication credentials</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult Create([FromBody] CreateTwoDigitSystemDto createTwoDigitSystemDto) 
        {
            var id = _twoDigitSystemService.Create(createTwoDigitSystemDto);
            return Created($"/api/TwoDigitSystem/{id}", null);
        }
        /// <summary>
        ///delete TwoDigitSystem by TwoDigitSystem ID
        /// </summary>
        /// <response code="204">TwoDigitSystem deleted successfully</response>
        /// <response code="401">Invalid or missing authentication credentials</response>
        /// <response code="403">Access forbidden</response>
        /// <response code="404">TwoDigitSystem not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _twoDigitSystemService.Delete(id);

            return NoContent();
        }
        /// <summary>
        ///get all TwoDigitSystems, only for admins
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Query validation error</response>
        /// <response code="401">Invalid or missing authentication credentials</response>
        /// <response code="403">Access forbidden</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <response code="200">OK</response>
        /// <response code="401">Invalid or missing authentication credentials</response>
        /// <response code="403">Access forbidden</response>
        /// <response code="404">TwoDigitSystem not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public ActionResult<TwoDigitSystemDto> GetById([FromRoute] int id)
        {
            var twoDigitSystemsDto = _twoDigitSystemService.GetById(id);
            return Ok(twoDigitSystemsDto);
        }
        /// <summary>
        ///get TwoDigitSystem by ID of the user who created it
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="401">Invalid or missing authentication credentials</response>
        /// <response code="403">Access forbidden</response>
        /// <response code="404">TwoDigitSystem not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("UserId/{userId}")]
        public ActionResult<TwoDigitSystemDto> GetByCreatedById([FromRoute] int userId)
        {
            var twoDigitSystemsDto = _twoDigitSystemService.GetByUserId(userId);
            return Ok(twoDigitSystemsDto);
        }
        /// <summary>
        ///update TwoDigitSystem by TwoDigitSystem ID
        /// </summary>>
        /// <response code="200">OK</response>
        /// <response code="400">TwoDigitSystem validation error</response>
        /// <response code="401">Invalid or missing authentication credentials</response>
        /// <response code="403">Access forbidden</response>
        /// <response code="404">TwoDigitSystem not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateTwoDigitSystemDto createTwoDigitSystemDto, [FromRoute]int id)
        {
            _twoDigitSystemService.Update(id, createTwoDigitSystemDto);
            return Ok();
        }
    }
}
