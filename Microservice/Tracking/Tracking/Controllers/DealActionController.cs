using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.Shared.SharedClasses.Pagination;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.DtoModel.GetDeals;
using System.Drawing.Printing;

namespace Platform.Tracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealActionController : Controller
    {
        private readonly ILogger<DealActionController> _logger;
        private readonly IDealAction _dealAction;
        public DealActionController(ILogger<DealActionController> logger, IDealAction dealActionService)
        {
            _logger = logger;
            _dealAction = dealActionService;
        }
        [Route("AddDealAction")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddDealAction([FromBody] DealsActionDTO dealsActionDTO)
        {
            try
            {
                DealsActionDTO dealsAction=_dealAction.AddDealAction(dealsActionDTO);
                return Created(nameof(AddDealAction),dealsAction);
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddDealAction)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
          
        }

        [Route("GetAllDealAction")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllDealAction([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var dealActionList = _dealAction.GetDealsActionPagedList(pagedParameters);
                PaginationData metadata = new PaginationData
                {
                    TotalCount = dealActionList.TotalCount,
                    PageSize = dealActionList.PageSize,
                    CurrentPage = dealActionList.CurrentPage,
                    TotalPages = dealActionList.TotalPages,
                    HasNext = dealActionList.HasNext,
                    HasPrevious = dealActionList.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(dealActionList);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDealAction)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
        }


    }
}
