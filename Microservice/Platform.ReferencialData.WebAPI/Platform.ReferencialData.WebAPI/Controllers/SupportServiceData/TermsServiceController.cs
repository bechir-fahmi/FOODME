using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.SupportServiceData;
using Platform.ReferencialData.Business.business_services_implementations.SupportServiceData;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.ReferentialData.DtoModel.SupportService;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.SupportServiceData
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsServiceController : ControllerBase
    {
        private readonly ITermsServiceService _termsServiceService;
        private readonly ILogger<TermsServiceController> _logger;
        private readonly IMapper _mapper;

        public TermsServiceController(ITermsServiceService termsServiceService, ILogger<TermsServiceController> logger, IMapper mapper)
        {
            _termsServiceService = termsServiceService;
            _logger = logger;
            _mapper = mapper;
        }


        [Route("GetAllTermsService")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllTermsService([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var TermsServiceList = _termsServiceService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = TermsServiceList.TotalCount,
                    PageSize = TermsServiceList.PageSize,
                    CurrentPage = TermsServiceList.CurrentPage,
                    TotalPages = TermsServiceList.TotalPages,
                    HasNext = TermsServiceList.HasNext,
                    HasPrevious = TermsServiceList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(TermsServiceList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllTermsService)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }



        [Route("AddTermsService")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTermsService([FromBody] TermsServiceDTO TermsService)
        {
            try
            {
                _termsServiceService.Add(TermsService);
                return Created(nameof(AddTermsService), TermsService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddTermsService)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetTermsService/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTermsService(int id)
        {
            try
            {
                TermsServiceDTO TermsServiceDTO = _termsServiceService.Get(id);
                return Ok(TermsServiceDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetTermsService)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveTermsService/{VendorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveTermsService(int Id)
        {
            try
            {
                _termsServiceService.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveTermsService)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateTermsService")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTermsService(TermsServiceDTO TermsService)
        {
            try
            {
                _termsServiceService.Update(TermsService);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateTermsService)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
