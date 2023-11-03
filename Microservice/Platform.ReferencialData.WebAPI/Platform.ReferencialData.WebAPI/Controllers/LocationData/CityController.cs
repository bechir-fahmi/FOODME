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
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper, ILogger<CityController> logger)
        {
            _cityService = cityService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetAllCities")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCities([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var CitiesList = _cityService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = CitiesList.TotalCount,
                    PageSize = CitiesList.PageSize,
                    CurrentPage = CitiesList.CurrentPage,
                    TotalPages = CitiesList.TotalPages,
                    HasNext = CitiesList.HasNext,
                    HasPrevious = CitiesList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(CitiesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllCities)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveCities")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveCities([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var CitiesList = _cityService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = CitiesList.TotalCount,
                    PageSize = CitiesList.PageSize,
                    CurrentPage = CitiesList.CurrentPage,
                    TotalPages = CitiesList.TotalPages,
                    HasNext = CitiesList.HasNext,
                    HasPrevious = CitiesList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(CitiesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveCities)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetCityByRegionId/{regionId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCityByRegionId(int regionId)
        {
            try
            {
                List<CityDTO> cityList = _cityService.GetCityByRegionId(regionId);

                return Ok(cityList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCityByRegionId)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [Route("GetCity/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCity(int id)
        {
            try
            {
                CityDTO refData = _cityService.Get(id);
                if (refData == null)
                    return NotFound();
                return Ok(refData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCity)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }




        [Route("AddCity")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddCity([FromBody] CityDTO city)
        {
            try
            {
                _cityService.Add(city);
                return Created(nameof(AddCity), city);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddCity)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveCity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveCity([FromBody] CityDTO city)
        {
            try
            {
                _cityService.Remove(city);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveCity)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateCity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCity([FromBody] CityDTO city)
        {
            try
            {
                _cityService.Update(city);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateCity)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<CityDTO>> GetFilteredData([FromQuery] CityFillter filter)
        {
            var result = _cityService.GetFilteredData(filter);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
