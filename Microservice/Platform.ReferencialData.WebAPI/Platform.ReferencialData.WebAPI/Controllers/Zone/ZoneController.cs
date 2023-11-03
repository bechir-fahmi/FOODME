using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services;
using Platform.ReferentialData.DtoModel;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.Zone
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _zoneService;
        private readonly ILogger<ZoneController> _logger;
        private readonly IMapper _mapper;

        public ZoneController(IZoneService zoneService, ILogger<ZoneController> logger, IMapper mapper)
        {
            _zoneService = zoneService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllZone")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllZone([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var ZoneList = _zoneService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = ZoneList.TotalCount,
                    PageSize = ZoneList.PageSize,
                    CurrentPage = ZoneList.CurrentPage,
                    TotalPages = ZoneList.TotalPages,
                    HasNext = ZoneList.HasNext,
                    HasPrevious = ZoneList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(ZoneList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllZone)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveZone")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveZone([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var ZoneList = _zoneService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = ZoneList.TotalCount,
                    PageSize = ZoneList.PageSize,
                    CurrentPage = ZoneList.CurrentPage,
                    TotalPages = ZoneList.TotalPages,
                    HasNext = ZoneList.HasNext,
                    HasPrevious = ZoneList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(ZoneList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveZone)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("AddZone")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddZone([FromBody] ZoneDTO zone)
        {
            try
            {
                _zoneService.Add(zone);
                return Created(nameof(AddZone), zone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddZone)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("GetZone/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetZone(int id)
        {
            try
            {
                ZoneDTO zoneDto = _zoneService.Get(id);
                return Ok(zoneDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetZone)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveZone")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveZone([FromBody] ZoneDTO zone)
        {
            try
            {
                _zoneService.Remove(zone);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveZone)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateZone")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateZone([FromBody]ZoneDTO zone)
        {
            try
            {
                _zoneService.Update(zone);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateZone)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
