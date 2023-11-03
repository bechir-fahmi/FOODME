using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.Shared.Enum;
using Platform.Shared.SharedClasses.Pagination;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.DtoModel.Response;

namespace Platform.Tracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandActionController : Controller
    {
        private readonly ILogger<BrandActionController> _logger;
        private readonly IBrandActionService _brandActionService;

        public BrandActionController(ILogger<BrandActionController> logger,
            IBrandActionService brandActionService)
        {
            _logger = logger;
            _brandActionService = brandActionService;
        }

        [Route("AddViewDetail")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddViewDetail([FromBody] BrandActionDTO brandActionDTO)
        {
            try
            {
                BrandActionDTO brandAction = _brandActionService.AddBrandAction(brandActionDTO, Shared.Enum.TypeOfAction.ViewDetails);
                return Created(nameof(AddViewDetail), brandAction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddViewDetail)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
        }

        [Route("AddGoToApp")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddGoToApp([FromBody] BrandActionDTO brandActionDTO)
        {
            try
            {
                BrandActionDTO brandAction = _brandActionService.AddBrandAction(brandActionDTO, Shared.Enum.TypeOfAction.GoToApp);
                return Created(nameof(AddViewDetail), brandAction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddViewDetail)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
        }

        [Route("GetAllBrandAction")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllBrandAction([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var brandActionList = _brandActionService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = brandActionList.TotalCount,
                    PageSize = brandActionList.PageSize,
                    CurrentPage = brandActionList.CurrentPage,
                    TotalPages = brandActionList.TotalPages,
                    HasNext = brandActionList.HasNext,
                    HasPrevious = brandActionList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllBrandAction)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

        [Route("GetAllViewDetailsByBrand")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllViewDetailsByBrand()
        {
            try
            {
                var brandActionList = _brandActionService.GetAllActionsByBrand(TypeOfAction.ViewDetails);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllViewDetailsByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

        [Route("GetAllGoToAppByBrand")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllGoToAppByBrand()
        {
            try
            {
                var brandActionList = _brandActionService.GetAllActionsByBrand(TypeOfAction.GoToApp);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllGoToAppByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

        /**********************************************************************************************************************/
        [Route("GetAllViewDetailsByBrandID")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllViewDetailsByBrandId()
        {
            try
            {
                var brandActionList = _brandActionService.GetAllActionsByBrandId(TypeOfAction.ViewDetails);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllViewDetailsByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

        [Route("GetAllGoToAppByBrandID")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllGoToAppByBrandId()
        {
            try
            {
                var brandActionList = _brandActionService.GetAllActionsByBrandId(TypeOfAction.GoToApp);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllGoToAppByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }
        /*********************************************************************************************************************/


        [Route("GetAllViewDetailsByPeriodOfTime/{startTime}/{endTime}")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllViewDetailsByPeriodOfTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                var brandActionList = _brandActionService.GetAllActionsByPeriodOfTime(TypeOfAction.ViewDetails, startTime, endTime);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllViewDetailsByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

        [Route("GetAllGoToAppByPeriodOfTime/{startTime}/{endTime}")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllGoToAppByPeriodOfTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                var brandActionList = _brandActionService.GetAllActionsByPeriodOfTime(TypeOfAction.GoToApp, startTime, endTime);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllGoToAppByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }


        [Route("GetAllBrandActionViewDetailsByPeriodOfTime/{brandModelId}/{startTime}/{endTime}")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllBrandActionViewDetailsByPeriodOfTime(Guid brandModelId, DateTime startTime, DateTime endTime)
        {
            try
            {
                var brandActionList = _brandActionService.GetAllBrandActionsByPeriodOfTime(brandModelId, TypeOfAction.ViewDetails, startTime, endTime);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllViewDetailsByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }



        [Route("GetAllBrandActionGoToAppByPeriodOfTime/{brandModelId}/{startTime}/{endTime}")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllBrandActionGoToAppByPeriodOfTime(Guid brandModelId, DateTime startTime, DateTime endTime)
        {
            try
            {
                var brandActionList = _brandActionService.GetAllBrandActionsByPeriodOfTime(brandModelId, TypeOfAction.GoToApp, startTime, endTime);

                return Ok(brandActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllViewDetailsByBrand)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }
    }
}
