using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.Shared.SharedClasses.Pagination;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.Business.Business_Service_Implementation;
using Platform.Tracking.DtoModel.Offre;

namespace Platform.Tracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferActionController : Controller
    {
        private readonly ILogger<OfferActionController> _logger;
        private readonly IOfferActionService _offerActionService;
        public OfferActionController(ILogger<OfferActionController> logger,IOfferActionService offerActionService)
        {
            _logger= logger;
            _offerActionService= offerActionService;
        }
        [Route("GetAllOfferAction")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult GetAllOfferAction([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var offreActionList = _offerActionService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = offreActionList.TotalCount,
                    PageSize = offreActionList.PageSize,
                    CurrentPage = offreActionList.CurrentPage,
                    TotalPages = offreActionList.TotalPages,
                    HasNext = offreActionList.HasNext,
                    HasPrevious = offreActionList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(offreActionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllOfferAction)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
          

        }

        [Route("AddOfferAction")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddOfferAction([FromBody] OfferActionDTO offerActionDTO)
        {
            try
            {
                OfferActionDTO offerAction = _offerActionService.AddOfferAction(offerActionDTO);
                return Created(nameof(AddOfferAction), offerAction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOfferAction)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
        }

        /*********************************************************************************************************/
        [HttpGet("{offerId}/clicks")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CountClicksByTimeAndSocialMedia(Guid offerId)
        {
            try
            {
                // Validate the offerId (you should adapt this to your validation logic).
                if (offerId == Guid.Empty)
                {
                    return BadRequest("Invalid offerId");
                }

                var result = _offerActionService.CountClicksByTimeAndSocialMedia(offerId);

                if (result == null)
                {
                    return NotFound("Offer not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging and monitoring purposes.
                _logger.LogError(ex, "An error occurred while processing CountClicksByTimeAndSocialMedia.");

                // Return a more user-friendly error message.
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        /*********************************************************************************************************/

    }
}
