using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.Business.Business_Service_Implementation;
using Platform.Tracking.DtoModel.Response;

namespace Platform.Tracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandActionSummaryController : Controller
    {
        private readonly ILogger<BrandActionController> _logger;
        private readonly IBrandActionSummaryService _brandActionSummaryService;

        public BrandActionSummaryController(ILogger<BrandActionController> logger,
            IBrandActionSummaryService brandActionSummaryService)
        {
            _logger = logger;
            _brandActionSummaryService = brandActionSummaryService;
        }
        [Route("getBrandActionSummaryViews")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBrandActionSummaryViews()
        {
            try
            {
                var getBrandActionSummaryViews = _brandActionSummaryService.getBrandActionSummaryViews();
                return Ok(getBrandActionSummaryViews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetBrandActionSummaryViews)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }

        [Route("getAllBrandActionSummary")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getAllBrandActionSummary()
        {
            try
            {
                var BrandActionSummaries = _brandActionSummaryService.GetAllBrandActionSummary();
                return Ok(BrandActionSummaries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getAllBrandActionSummary)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }
        [Route("getAllBrandActionSummaryTotalUsers")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getAllBrandActionSummaryTotalUsers()
        {
            try
            {
                var BrandActionSummaryTotalUsers = _brandActionSummaryService.GetAllBrandActionSummaryTotalUsers();
                return Ok(BrandActionSummaryTotalUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(getAllBrandActionSummary)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }

        [Route("AddOrUpdateBrandActionSummary")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddOrUpdateBrandActionSummary()
        {
            try
            {
                var BrandActionSummaryViews = _brandActionSummaryService.getBrandActionSummaryViews();

                var responseDTO = _brandActionSummaryService.AddOrUpdateBrandActionSummary(BrandActionSummaryViews);
                if (responseDTO.Result.StatusCodes == 200)
                {
                    return Ok(responseDTO);
                }
                else
                {
                    return BadRequest(responseDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrUpdateBrandActionSummary)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }

    }
}
