using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.MealData;
using Platform.ReferentialData.DtoModel.MealData;
using Platform.Shared.Images;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.MealType
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTypeController : ControllerBase
    {
        private readonly IMealTypeService _mealTypeService;
        private readonly ILogger<MealTypeController> _logger;
        private readonly IMapper _mapper;

        public MealTypeController(IMealTypeService mealTypeService, ILogger<MealTypeController> logger, IMapper mapper)
        {
            _mealTypeService = mealTypeService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllMealTypes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMealTypes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var mealTypeList = _mealTypeService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = mealTypeList.TotalCount,
                    PageSize = mealTypeList.PageSize,
                    CurrentPage = mealTypeList.CurrentPage,
                    TotalPages = mealTypeList.TotalPages,
                    HasNext = mealTypeList.HasNext,
                    HasPrevious = mealTypeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(mealTypeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllMealTypes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveMealTypes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveMealTypes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var mealTypeList = _mealTypeService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = mealTypeList.TotalCount,
                    PageSize = mealTypeList.PageSize,
                    CurrentPage = mealTypeList.CurrentPage,
                    TotalPages = mealTypeList.TotalPages,
                    HasNext = mealTypeList.HasNext,
                    HasPrevious = mealTypeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(mealTypeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveMealTypes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetMealTypesByTag/{tag}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetMealTypesByTag(string tag, [FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var MealTypeListByTag = _mealTypeService.Get(tag, pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = MealTypeListByTag.TotalCount,
                    PageSize = MealTypeListByTag.PageSize,
                    CurrentPage = MealTypeListByTag.CurrentPage,
                    TotalPages = MealTypeListByTag.TotalPages,
                    HasNext = MealTypeListByTag.HasNext,
                    HasPrevious = MealTypeListByTag.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(MealTypeListByTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMealTypesByTag)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


        [Route("AddMealType")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddMealType([FromBody] MealTypeDTO mealType)
        {
            try
            {
                foreach (var LanguageRessource in mealType.LanguageResourceSet?.LanguageRessource)
                {
                    var image = LanguageRessource.Image;
                    if (!string.IsNullOrEmpty(image))
                    {
                        var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                        LanguageRessource.Image = imageURL;
                    }

                }
                _mealTypeService.Add(mealType);
                return Created(nameof(AddMealType), mealType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddMealType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetMealType/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetMealType(int id)
        {
            try
            {
                MealTypeDTO mealTypeDTO = _mealTypeService.Get(id);
                return Ok(mealTypeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMealType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveMealType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveMealType([FromBody] MealTypeDTO mealType)
        {
            try
            {
                _mealTypeService.Remove(mealType);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveMealType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateMealType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateMealType([FromBody] MealTypeDTO mealType)
        {
            try
            {
                
                _mealTypeService.Update(mealType);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateMealType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
      /*  [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UnlikeReasonDTO>> GetFilteredData([FromQuery] MealTypeFilterDTO filter)
        {
            var result = _mealTypeService.GetFilteredData(filter);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }*/
    }
}