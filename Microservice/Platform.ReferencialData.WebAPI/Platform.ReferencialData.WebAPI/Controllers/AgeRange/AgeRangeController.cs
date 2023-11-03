using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.AgeRangeData;
using Platform.ReferentialData.DtoModel.AgeRangeData;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.AgeRange
{
    public class AgeRangeController : ControllerBase
    {
        private readonly IAgeRangeService _ageRangeService;
        private readonly ILogger<AgeRangeController> _logger;
        private readonly IMapper _mapper;

        public AgeRangeController(IAgeRangeService ageRangeService, ILogger<AgeRangeController> logger, IMapper mapper)
        {
            _ageRangeService = ageRangeService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllAgeRanges")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAgeRanges([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var ageRangeList = _ageRangeService.GetAll(pagedParameters);
                PaginationData metadata = new PaginationData
                {
                    TotalCount = ageRangeList.TotalCount,
                    PageSize = ageRangeList.PageSize,
                    CurrentPage = ageRangeList.CurrentPage,
                    TotalPages = ageRangeList.TotalPages,
                    HasNext = ageRangeList.HasNext,
                    HasPrevious = ageRangeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(ageRangeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllAgeRanges)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetAllActiveAgeRanges")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveAgeRanges([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var ageRangeList = _ageRangeService.GetAllActiveData(pagedParameters);
                PaginationData metadata = new PaginationData
                {
                    TotalCount = ageRangeList.TotalCount,
                    PageSize = ageRangeList.PageSize,
                    CurrentPage = ageRangeList.CurrentPage,
                    TotalPages = ageRangeList.TotalPages,
                    HasNext = ageRangeList.HasNext,
                    HasPrevious = ageRangeList.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(ageRangeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveAgeRanges)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("AddAgeRange")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddAgeRange([FromBody] AgeRangeDTO AgeRange)
        {
            try
            {
                _ageRangeService.Add(AgeRange);
                return Created(nameof(AddAgeRange), AgeRange);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddAgeRange)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetAgeRange/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAgeRange(int id)
        {
            try
            {
                AgeRangeDTO ageRangeDTO = _ageRangeService.Get(id);
                return Ok(ageRangeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAgeRange)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveAgeRange")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveAgeRange([FromBody] AgeRangeDTO AgeRange)
        {
            try
            {
                _ageRangeService.Remove(AgeRange);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveAgeRange)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateAgeRange")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAgeRange([FromBody] AgeRangeDTO ageRange)
        {
            try
            {
                _ageRangeService.Update(ageRange);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateAgeRange)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        /* [HttpGet]
         [Route("GetFilteredData")]
         [ProducesResponseType(StatusCodes.Status204NoContent)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         public ActionResult<List<UnlikeReasonDTO>> GetFilteredData([FromQuery] FoodTypeFilterDTO filter)
         {
             var result = _ageRangeService.GetFilteredData(filter);

             if (result == null || result.Count == 0)
             {
                 return NotFound();
             }

             return Ok(result);
         }*/
    }
}
