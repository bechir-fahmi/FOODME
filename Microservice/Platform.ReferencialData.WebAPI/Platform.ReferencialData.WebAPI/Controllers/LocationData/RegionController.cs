using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.LocationData
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly ILogger<RegionController> _logger;
        private readonly IMapper _mapper;

        public RegionController(IRegionService regionService, IMapper mapper, ILogger<RegionController> logger)
        {
            _regionService = regionService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllRegions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllRegions([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var RegionsList = _regionService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = RegionsList.TotalCount,
                    PageSize = RegionsList.PageSize,
                    CurrentPage = RegionsList.CurrentPage,
                    TotalPages = RegionsList.TotalPages,
                    HasNext = RegionsList.HasNext,
                    HasPrevious = RegionsList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(RegionsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllRegions)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllActiveRegions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveRegions([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var RegionsList = _regionService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = RegionsList.TotalCount,
                    PageSize = RegionsList.PageSize,
                    CurrentPage = RegionsList.CurrentPage,
                    TotalPages = RegionsList.TotalPages,
                    HasNext = RegionsList.HasNext,
                    HasPrevious = RegionsList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(RegionsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveRegions)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddRegion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddRegion([FromBody] RegionDTO region)
        {
            try
            {
                _regionService.Add(region);
                return Created(nameof(AddRegion), region);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddRegion)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("GetRegion/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRegion(int id)
        {
            try
            {
                RegionDTO refData = _regionService.Get(id);
                if (refData == null)
                    return NotFound();
                return Ok(refData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetRegion)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveRegion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveRegion([FromBody] RegionDTO region)
        {
            try
            {
                _regionService.Remove(region);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveRegion)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateRegion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateRegion(RegionDTO region)
        {
            try
            {
                _regionService.Update(region);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateRegion)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<RegionDTO>> GetFilteredData([FromQuery] RegionFillter filter)
        {
            var result = _regionService.GetFilteredData(filter);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
