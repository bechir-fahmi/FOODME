using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.FoodTypeData;
using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.Shared.SharedClasses.Pagination;
using System.Text.Json.Serialization;
using System.Text.Json;
using Platform.Shared.Images;

namespace Platform.ReferencialData.WebAPI.Controllers.FoodData
{
    public class FoodTypeController : ControllerBase
    {
        private readonly IFoodTypeService _foodTypeService;
        private readonly ILogger<FoodTypeController> _logger;
        private readonly IMapper _mapper;

        public FoodTypeController(IFoodTypeService foodTypeService, ILogger<FoodTypeController> logger, IMapper mapper)
        {
            _foodTypeService = foodTypeService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllFoodTypes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllFoodTypes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var foodTypeList = _foodTypeService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = foodTypeList.TotalCount,
                    PageSize = foodTypeList.PageSize,
                    CurrentPage = foodTypeList.CurrentPage,
                    TotalPages = foodTypeList.TotalPages,
                    HasNext = foodTypeList.HasNext,
                    HasPrevious = foodTypeList.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(foodTypeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllFoodTypes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetAllActiveFoodTypes")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveFoodTypes([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var foodTypeList = _foodTypeService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = foodTypeList.TotalCount,
                    PageSize = foodTypeList.PageSize,
                    CurrentPage = foodTypeList.CurrentPage,
                    TotalPages = foodTypeList.TotalPages,
                    HasNext = foodTypeList.HasNext,
                    HasPrevious = foodTypeList.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(foodTypeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveFoodTypes)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("AddFoodType")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddFoodType([FromBody] FoodTypeDTO foodType)
        {
            try
            {
                foreach (var LanguageRessource in foodType.LanguageResourceSet?.LanguageRessource)
                {
                    var image = LanguageRessource.Image;
                    if (!string.IsNullOrEmpty(image))
                    {
                        var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                        LanguageRessource.Image = imageURL;
                    }

                }
                _foodTypeService.Add(foodType);
                return Created(nameof(AddFoodType), foodType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddFoodType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetFoodType/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFoodType(int id)
        {
            try
            {
                FoodTypeDTO foodDTO = _foodTypeService.Get(id);
                return Ok(foodDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetFoodType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetFoodTypesByTag/{tag}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFoodTypesByTag(string tag, [FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var foodTypeListByTag = _foodTypeService.Get(tag,pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = foodTypeListByTag.TotalCount,
                    PageSize = foodTypeListByTag.PageSize,
                    CurrentPage = foodTypeListByTag.CurrentPage,
                    TotalPages = foodTypeListByTag.TotalPages,
                    HasNext = foodTypeListByTag.HasNext,
                    HasPrevious = foodTypeListByTag.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(foodTypeListByTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetFoodTypesByTag)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveFoodType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveFoodType([FromBody]FoodTypeDTO foodType)
        {
            try
            {
                _foodTypeService.Remove(foodType);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveFoodType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateFoodType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateFoodType([FromBody]FoodTypeDTO foodType)
        {
            try
            {
                _foodTypeService.Update(foodType);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateFoodType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
       /* [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UnlikeReasonDTO>> GetFilteredData([FromQuery] FoodTypeFilterDTO filter)
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
