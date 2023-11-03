using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.KitchenTypeData;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using Platform.Shared.SharedClasses.Pagination;
using Platform.Shared.Images;
using Microsoft.Extensions.Logging;

namespace Platform.ReferencialData.WebAPI.Controllers.KitchenType
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitchenTypeController : ControllerBase
    {
        private readonly IKitchenTypeService _KitchenService;
        private readonly ILogger<KitchenTypeController> _logger;
        private readonly IMapper _mapper;

        public KitchenTypeController(IKitchenTypeService kitchenService, ILogger<KitchenTypeController> logger, IMapper mapper)
        {
            _KitchenService = kitchenService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllKitchens")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllKitchens([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var KitchenList = _KitchenService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = KitchenList.TotalCount,
                    PageSize = KitchenList.PageSize,
                    CurrentPage = KitchenList.CurrentPage,
                    TotalPages = KitchenList.TotalPages,
                    HasNext = KitchenList.HasNext,
                    HasPrevious = KitchenList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(KitchenList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllKitchens)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveKitchens")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveKitchens([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var KitchenList = _KitchenService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = KitchenList.TotalCount,
                    PageSize = KitchenList.PageSize,
                    CurrentPage = KitchenList.CurrentPage,
                    TotalPages = KitchenList.TotalPages,
                    HasNext = KitchenList.HasNext,
                    HasPrevious = KitchenList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(KitchenList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveKitchens)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetKitchenTypesByTag/{tag}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetKitchenTypesByTag(string tag, [FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var kitchenListByTag = _KitchenService.Get(tag, pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = kitchenListByTag.TotalCount,
                    PageSize = kitchenListByTag.PageSize,
                    CurrentPage = kitchenListByTag.CurrentPage,
                    TotalPages = kitchenListByTag.TotalPages,
                    HasNext = kitchenListByTag.HasNext,
                    HasPrevious = kitchenListByTag.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(kitchenListByTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetKitchenTypesByTag)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }



        [Route("AddKitchen")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddKitchen([FromBody] KitchenTypeDTO Kitchen)
        {
            try
            {
                foreach (var LanguageRessource in Kitchen.LanguageResourceSet.LanguageRessource)
                {
                    var image = LanguageRessource.Image;
                    if (!string.IsNullOrEmpty(image))
                    {
                        var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                        LanguageRessource.Image = imageURL;
                    }

                }
                _KitchenService.Add(Kitchen);
                
                
                return Created(nameof(AddKitchen), Kitchen);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddKitchen)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetKitchen/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetKitchen(int id)
        {
            try
            {
                KitchenTypeDTO KitchenDTO = _KitchenService.Get(id);
                return Ok(KitchenDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetKitchen)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveKitchen")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveKitchen([FromBody] KitchenTypeDTO Kitchen)
        {
            try
            {
                _KitchenService.Remove(Kitchen);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveKitchen)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateKitchen")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateKitchen([FromBody] KitchenTypeDTO Kitchen)
        {
            try
            {
                /*foreach (var LanguageRessource in Kitchen.LanguageResourceSet.LanguageRessource)
                {

                    var image = LanguageRessource.Image;
                    if (!string.IsNullOrEmpty(image))
                    {
                        var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                        LanguageRessource.Image = imageURL;
                    }

                }*/
                
                _KitchenService.Update(Kitchen);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateKitchen)}");
                return StatusCode(500, $"Internal server Error. Please try later: error = {ex}");
            }
        }
       /* [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UnlikeReasonDTO>> GetFilteredData([FromQuery] KitchenTypeFillter filter)
        {
            var result = _KitchenService.GetFilteredData(filter);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }*/
    }
}
