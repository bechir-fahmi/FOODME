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
    public class LanguageResourceController : ControllerBase
    {
        private readonly ILanguageResourceService _languageResourceService;
        private readonly ILogger<LanguageResourceController> _logger;
        private readonly IMapper _mapper;

        public LanguageResourceController(ILanguageResourceService languageResourceService, ILogger<LanguageResourceController> logger, IMapper mapper)
        {
            _languageResourceService = languageResourceService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllLanguageResource")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllLanguageResource([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var LanguageResourceList = _languageResourceService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = LanguageResourceList.TotalCount,
                    PageSize = LanguageResourceList.PageSize,
                    CurrentPage = LanguageResourceList.CurrentPage,
                    TotalPages = LanguageResourceList.TotalPages,
                    HasNext = LanguageResourceList.HasNext,
                    HasPrevious = LanguageResourceList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(LanguageResourceList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLanguageResource)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetLanguageResource/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLanguageResource(int id)
        {
            try
            {
                LanguageResourceDTO languageDTO = _languageResourceService.Get(id);
                return Ok(languageDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLanguageResource)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetLanguageResourcesByCode")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public List<LanguageResourceDTO> GetLanguageResourcesByCode(Guid code)
        {
            try
            {
                var LanguageResourceList = _languageResourceService.GetLanguageResourcesByCode(code);
                return LanguageResourceList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLanguageResourcesByCode)}");
                var LanguageResourceList = _languageResourceService.GetLanguageResourcesByCode(code);
                return LanguageResourceList;
                
            }

        }

        [Route("AddLanguageResource")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddLanguageResource([FromBody] LanguageResourceDTO resourceDto)
        {
            try
            {
                _languageResourceService.Add(resourceDto);
                return Created(nameof(AddLanguageResource), resourceDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddLanguageResource)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveLanguageResource/{LanguageResourceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveLanguageResource(int LanguageResourceId)
        {
            try
            {
                _languageResourceService.Remove(LanguageResourceId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveLanguageResource)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateLanguageResource")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLanguageResource(LanguageResourceDTO dto)
        {
            try
            {
                _languageResourceService.Update(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateLanguageResource)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
