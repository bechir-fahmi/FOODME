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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryService = countryService; 

            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetAllCountries")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCountries([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var CountriesList = _countryService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = CountriesList.TotalCount,
                    PageSize = CountriesList.PageSize,
                    CurrentPage = CountriesList.CurrentPage,
                    TotalPages = CountriesList.TotalPages,
                    HasNext = CountriesList.HasNext,
                    HasPrevious = CountriesList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                _logger.LogInformation($"Returned {CountriesList.TotalCount} Countries from database.");

                return Ok(CountriesList);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllCountries)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
                
            }

        }
        [Route("GetAllActiveCountries")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveCountries([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var CountriesList = _countryService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = CountriesList.TotalCount,
                    PageSize = CountriesList.PageSize,
                    CurrentPage = CountriesList.CurrentPage,
                    TotalPages = CountriesList.TotalPages,
                    HasNext = CountriesList.HasNext,
                    HasPrevious = CountriesList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                _logger.LogInformation($"Returned {CountriesList.TotalCount} Countries from database.");

                return Ok(CountriesList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveCountries)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");

            }

        }
        [Route("GetCountry/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountry(int id)
        {
            try
            {
                CountryDTO refData = _countryService.Get(id);
                if (refData == null)
                    return NotFound();
                return Ok(refData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }




        [Route("AddCountry")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddCountry([FromBody] CountryDTO country)
        {
            try
            {
                _countryService.Add(country);
                return Created(nameof(AddCountry), country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddCountry)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveCountry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveCountry([FromBody] CountryDTO country)
        {
            try
            {
                _countryService.Remove(country);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveCountry)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateCountry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCountry(CountryDTO country)
        {
            try
            {
                _countryService.Update(country);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<CountryDTO>> GetFilteredData([FromQuery] CountryFillter filter)
        {
            var result = _countryService.GetFilteredData(filter);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
