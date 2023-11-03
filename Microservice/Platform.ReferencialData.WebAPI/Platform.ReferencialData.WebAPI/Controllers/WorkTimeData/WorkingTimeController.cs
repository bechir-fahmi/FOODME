using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.WorkingTimeData;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.WorkTimeData
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingTimeController : ControllerBase
    {
        private readonly IClenderWorkingTimeService _workingTimeService;
        private readonly ILogger<WorkingTimeController> _logger;
        private readonly IMapper _mapper;

        public WorkingTimeController(IClenderWorkingTimeService workingTimeService, IMapper mapper, ILogger<WorkingTimeController> logger)
        {
            _workingTimeService = workingTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetAllWorkingTimes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllWorkingTimes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var workingTimesList = _workingTimeService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = workingTimesList.TotalCount,
                    PageSize = workingTimesList.PageSize,
                    CurrentPage = workingTimesList.CurrentPage,
                    TotalPages = workingTimesList.TotalPages,
                    HasNext = workingTimesList.HasNext,
                    HasPrevious = workingTimesList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(workingTimesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllWorkingTimes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("AddWorkingTime")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddWorkingTime([FromBody] ClenderWorkingTimeDTO workingTime)
        {
            try
            {
                _workingTimeService.Add(workingTime);
                return Created(nameof(AddWorkingTime), workingTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddWorkingTime)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("GetWorkingTime/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWorkingTime(int id)
        {
            try
            {
                ClenderWorkingTimeDTO refData = _workingTimeService.Get(id);
                if (refData == null)
                    return NotFound();
                return Ok(refData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetWorkingTime)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveWorkingTime/{VendorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveWorkingTime(int Id)
        {
            try
            {
                _workingTimeService.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveWorkingTime)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("UpdateWorkingTime")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateWorkingTime([FromBody] ClenderWorkingTimeDTO workingTime)
        {
            try
            {
                if (workingTime == null)
                {
                    return BadRequest("WorkingTimeDTO cannot be null");
                }

                _workingTimeService.Update(workingTime);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateWorkingTime)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


    }

}
