using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.DeliveryModeData;
using Platform.ReferentialData.DtoModel.DeliveryModeData;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.DeliveryModeData
{
    public class DeliveryModeController : ControllerBase
    {
        private readonly IDeliveryModeService _deliveryModeService;
        private readonly ILogger<DeliveryModeController> _logger;
        private readonly IMapper _mapper;

        public DeliveryModeController(IDeliveryModeService deliveryModeService, ILogger<DeliveryModeController> logger, IMapper mapper)
        {
            _deliveryModeService = deliveryModeService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllDeliveryModes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllDeliveryModes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var deliveryModeList = _deliveryModeService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = deliveryModeList.TotalCount,
                    PageSize = deliveryModeList.PageSize,
                    CurrentPage = deliveryModeList.CurrentPage,
                    TotalPages = deliveryModeList.TotalPages,
                    HasNext = deliveryModeList.HasNext,
                    HasPrevious = deliveryModeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(deliveryModeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDeliveryModes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveDeliveryModes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveDeliveryModes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var deliveryModeList = _deliveryModeService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = deliveryModeList.TotalCount,
                    PageSize = deliveryModeList.PageSize,
                    CurrentPage = deliveryModeList.CurrentPage,
                    TotalPages = deliveryModeList.TotalPages,
                    HasNext = deliveryModeList.HasNext,
                    HasPrevious = deliveryModeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(deliveryModeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveDeliveryModes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("AddDeliveryMode")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddDeliveryMode([FromBody] DeliveryModeDTO deliveryMode)
        {
            try
            {
                _deliveryModeService.Add(deliveryMode);
                return Created(nameof(AddDeliveryMode), deliveryMode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddDeliveryMode)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetDeliveryMode/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDeliveryMode(int id)
        {
            try
            {
                DeliveryModeDTO deliveryModeDTO = _deliveryModeService.Get(id);
                return Ok(deliveryModeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDeliveryMode)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveDeliveryType/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveDeliveryType(int Id)
        {
            try
            {
                _deliveryModeService.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveDeliveryType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateDeliveryMode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateDeliveryMode([FromBody] DeliveryModeDTO deliveryMode)
        {
            try
            {
                _deliveryModeService.Update(deliveryMode);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateDeliveryMode)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        /* [HttpGet]
         [Route("GetFilteredData")]
         [ProducesResponseType(StatusCodes.Status204NoContent)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         public ActionResult<List<UnlikeReasonDTO>> GetFilteredData([FromQuery] DeliveryModeFilterDTO filter)
         {
             var result = _deliveryModeService.GetFilteredData(filter);

             if (result == null || result.Count == 0)
             {
                 return NotFound();
             }

             return Ok(result);
         }*/
    }
}