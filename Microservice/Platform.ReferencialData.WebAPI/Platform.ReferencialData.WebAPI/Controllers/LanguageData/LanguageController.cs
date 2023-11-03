using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.LanguageData
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly ILogger<LanguageController> _logger;
        private readonly IMapper _mapper;

        public LanguageController(ILogger<LanguageController> logger, IMapper mapper, ILanguageService languageService)
        {

            _logger = logger;
            _mapper = mapper;
            _languageService = languageService;
        }

        [Route("GetAllLanguages")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllLanguages([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var LanguageList = _languageService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = LanguageList.TotalCount,
                    PageSize = LanguageList.PageSize,
                    CurrentPage = LanguageList.CurrentPage,
                    TotalPages = LanguageList.TotalPages,
                    HasNext = LanguageList.HasNext,
                    HasPrevious = LanguageList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(LanguageList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLanguages)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetAllActiveLanguages")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveLanguages([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var LanguageList = _languageService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = LanguageList.TotalCount,
                    PageSize = LanguageList.PageSize,
                    CurrentPage = LanguageList.CurrentPage,
                    TotalPages = LanguageList.TotalPages,
                    HasNext = LanguageList.HasNext,
                    HasPrevious = LanguageList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(LanguageList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveLanguages)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetLanguage/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLanguage(int id)
        {
            try
            {
                LanguageDTO languageDTO = _languageService.Get(id);
                return Ok(languageDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLanguage)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
         [Route("GetDefaultLanguage")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDefaultLanguage()
        {
            try
            {
                LanguageDTO languageDTO = _languageService.Get(x => x.isDefault == true);
                return Ok(languageDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDefaultLanguage)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [Route("AddLanguage")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddLanguage([FromBody] LanguageDTO language)
        {
            try
            {
                _languageService.Add(language);
                return Created(nameof(AddLanguage), language);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddLanguage)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
           
        }


        [HttpDelete]
        [Route("RemoveLanguage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Remove([FromBody] LanguageDTO language)
        {
            try
            {
                _languageService.Remove(language);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Remove)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [Route("UpdateLanguage")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLanguage([FromBody]LanguageDTO language)
        {
            try
            {
                _languageService.Update(language);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateLanguage)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
