using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.ThemeData;
using Platform.ReferentialData.DtoModel.ThemeData;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.ThemeData
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeService;
        private readonly ILogger<ThemeController> _logger;
        private readonly IMapper _mapper;

        public ThemeController(IThemeService themeService, ILogger<ThemeController> logger, IMapper mapper)
        {
            _themeService = themeService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllThemes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllThemes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var ThemeList = _themeService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = ThemeList.TotalCount,
                    PageSize = ThemeList.PageSize,
                    CurrentPage = ThemeList.CurrentPage,
                    TotalPages = ThemeList.TotalPages,
                    HasNext = ThemeList.HasNext,
                    HasPrevious = ThemeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(ThemeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllThemes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("AddTheme")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTheme([FromBody] ThemeDTO Theme)
        {
            try
            {
                _themeService.Add(Theme);
                return Created(nameof(AddTheme), Theme);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddTheme)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetTheme/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTheme(int id)
        {
            try
            {
                ThemeDTO ThemeDTO = _themeService.Get(id);
                return Ok(ThemeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetTheme)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveTheme/{VendorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveTheme(int Id)
        {
            try
            {
                _themeService.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveTheme)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateTheme")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTheme(ThemeDTO Theme)
        {
            try
            {
                _themeService.Update(Theme);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateTheme)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


    }
}
