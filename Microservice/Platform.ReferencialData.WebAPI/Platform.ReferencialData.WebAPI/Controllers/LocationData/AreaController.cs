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
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly ILogger<AreaController> _logger;
        private readonly IMapper _mapper;

        public AreaController(IAreaService areaService, IMapper mapper, ILogger<AreaController> logger)
        {
            _areaService = areaService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetAllAreas")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAreas([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var AreasList = _areaService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = AreasList.TotalCount,
                    PageSize = AreasList.PageSize,
                    CurrentPage = AreasList.CurrentPage,
                    TotalPages = AreasList.TotalPages,
                    HasNext = AreasList.HasNext,
                    HasPrevious = AreasList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(AreasList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllAreas)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveAreas")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveAreas([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var AreasList = _areaService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = AreasList.TotalCount,
                    PageSize = AreasList.PageSize,
                    CurrentPage = AreasList.CurrentPage,
                    TotalPages = AreasList.TotalPages,
                    HasNext = AreasList.HasNext,
                    HasPrevious = AreasList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(AreasList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveAreas)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetAreaByCityId/{cityId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAreaByCityId(int cityId)
        {
            try
            {
                List<AreaDTO> areaList = _areaService.GetAreaByCityId(cityId);

                return Ok(areaList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAreaByCityId)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("AddArea")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddArea([FromBody] AreaDTO area)
        {
            try
            {
                _areaService.Add(area);
                return Created(nameof(AddArea), area);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddArea)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        
        [Route("GetArea/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetArea(int id)
        {
            try
            {
                AreaDTO refData = _areaService.Get(id);
                if (refData == null)
                    return NotFound();
                return Ok(refData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetArea)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveArea")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveArea([FromBody] AreaDTO area)
        {
            try
            {
                _areaService.Remove(area);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveArea)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateArea")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateArea(AreaDTO area)
        {
            try
            {
                _areaService.Update(area);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateArea)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<AreaDTO>> GetFilteredData([FromQuery] AreaFillter filter)
        {
            var result = _areaService.GetFilteredData(filter);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
