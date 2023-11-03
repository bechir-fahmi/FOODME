using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.OfferData;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.OfferData;
using Platform.Shared.Images;
using Platform.Shared.SharedClasses.Pagination;
using System.Security.Claims;

namespace Platform.ReferencialData.WebAPI.Controllers.Offer
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly ILogger<OfferController> _logger;
        private readonly IMapper _mapper;

        public OfferController(IOfferService offerService, ILogger<OfferController> logger, IMapper mapper)
        {
            _offerService = offerService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllOffers")]
        [AllowAnonymous]
        //[Authorize("Permissions.Offers.ViewAll")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllOffers([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue("Id");
                var offerList = _offerService.GetAll(pagedParameters, userId);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = offerList.TotalCount,
                    PageSize = offerList.PageSize,
                    CurrentPage = offerList.CurrentPage,
                    TotalPages = offerList.TotalPages,
                    HasNext = offerList.HasNext,
                    HasPrevious = offerList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(offerList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(OfferController)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }

        }
        [Route("GetAllActiveOffers")]
        [AllowAnonymous]
        //[Authorize("Permissions.Offers.ViewAll")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveOffers([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var offerList = _offerService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = offerList.TotalCount,
                    PageSize = offerList.PageSize,
                    CurrentPage = offerList.CurrentPage,
                    TotalPages = offerList.TotalPages,
                    HasNext = offerList.HasNext,
                    HasPrevious = offerList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(offerList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveOffers)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }

        }

        [Route("AddOffer")]
        [AllowAnonymous]
        //[Authorize("Permissions.Offers.Create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddOffer([FromBody] OfferDTO Offer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("invalid offer model");
                }
                foreach (var LanguageRessource in Offer.LanguageResourceSet?.LanguageRessource)
                {
                    var image = LanguageRessource.Image;
                    if (image != null)
                    {
                        var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                        LanguageRessource.Image = imageURL;
                    }

                }
                var offerImage = Offer.Image;
                if (offerImage != null)
                {
                    var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, offerImage);
                    Offer.Image = imageURL;
                }

                Offer.Id= Guid.NewGuid();
                var addResult = _offerService.Add(Offer);
                if (addResult.StatusCodes == 201)
                {
                    return Ok(addResult);
                }
                else
                {
                    return BadRequest(addResult);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOffer)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }
        [Route("GetOffer/{id}")]
        [AllowAnonymous]
        //[Authorize("Permissions.Offers.View")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOffer(Guid id)
        {
            try
            {
                OfferDTO offerDTO = _offerService.Get(id);
                if (offerDTO == null)
                {
                    var response = new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        Error = "Offer not found",
                        Message = $"Offer with id : {id} not found"
                    };
                    return BadRequest(response);
                }
                return Ok(offerDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetOffer)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }

        }
        [Route("GetOffersByTag/{tag}")]
        [AllowAnonymous]
        //[Authorize("Permissions.Offers.ViewAll")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOffersByTag(string tag, [FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var OfferListByTag = _offerService.Get(tag, pagedParameters);
                if (OfferListByTag == null)
                    {
                        var response = new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status404NotFound,
                            Error = "Offer not found",
                            Message = $"Offer with tag : {tag} not found"
                        };
                        return BadRequest(response);
                    }

                PaginationData metadata = new PaginationData
                {
                    TotalCount = OfferListByTag.TotalCount,
                    PageSize = OfferListByTag.PageSize,
                    CurrentPage = OfferListByTag.CurrentPage,
                    TotalPages = OfferListByTag.TotalPages,
                    HasNext = OfferListByTag.HasNext,
                    HasPrevious = OfferListByTag.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(OfferListByTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetOffersByTag)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }


        [HttpDelete]
        [AllowAnonymous]
        //[Authorize("Permissions.Offers.Delete")]
        [Route("RemoveOffer/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveOffer(Guid id)
        {
            try
            {
                var removeResult = _offerService.Remove(id);
                if (removeResult.StatusCodes==204)
                {
                    return Ok(removeResult);
                }
                return BadRequest(removeResult);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveOffer)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        //[Authorize("Permissions.Offers.Update")]
        [Route("UpdateOffer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateOffer([FromBody] OfferDTO Offer)
        {
            try
            {
                /*foreach (var LanguageRessource in Offer.LanguageResourceSet?.LanguageRessource)
                {
                    var image = LanguageRessource.Image;
                    if (image != null)
                    {
                        var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                        LanguageRessource.Image = imageURL;
                    }

                }
                var offerImage = Offer.Image;
                if (offerImage != null)
                {
                    var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, offerImage);
                    Offer.Image = imageURL;
                }*/
                var updateResult = _offerService.Update(Offer);

                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateOffer)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }
    }
}
