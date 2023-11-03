using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.MealData;
using Platform.ReferentialData.DtoModel.MealData;
using Platform.Shared.Images;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.MealData
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTimingController : ControllerBase
    {
        private readonly IMealTimingService _mealTimingService;
        private readonly ILogger<MealTimingController> _logger;
        private readonly IMapper _mapper;

        public MealTimingController(IMealTimingService mealTimingService, ILogger<MealTimingController> logger, IMapper mapper)
        {
            _mealTimingService = mealTimingService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllMealTiming")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMealTiming([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var MealTimingList = _mealTimingService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = MealTimingList.TotalCount,
                    PageSize = MealTimingList.PageSize,
                    CurrentPage = MealTimingList.CurrentPage,
                    TotalPages = MealTimingList.TotalPages,
                    HasNext = MealTimingList.HasNext,
                    HasPrevious = MealTimingList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(MealTimingList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllMealTiming)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveMealTiming")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveMealTiming([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var MealTimingList = _mealTimingService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = MealTimingList.TotalCount,
                    PageSize = MealTimingList.PageSize,
                    CurrentPage = MealTimingList.CurrentPage,
                    TotalPages = MealTimingList.TotalPages,
                    HasNext = MealTimingList.HasNext,
                    HasPrevious = MealTimingList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(MealTimingList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveMealTiming)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetAllMealTimingByTag/{tag}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMealTimingByTag(string tag, [FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var MealTimingListByTag = _mealTimingService.Get(tag, pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = MealTimingListByTag.TotalCount,
                    PageSize = MealTimingListByTag.PageSize,
                    CurrentPage = MealTimingListByTag.CurrentPage,
                    TotalPages = MealTimingListByTag.TotalPages,
                    HasNext = MealTimingListByTag.HasNext,
                    HasPrevious = MealTimingListByTag.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(MealTimingListByTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllMealTimingByTag)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }



        [Route("AddMealTiming")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddMealTiming([FromBody] MealTimingDTO mealTiming)
        {
            try
            {
                foreach (var LanguageRessource in mealTiming.LanguageResourceSet?.LanguageRessource)
                {
                    var image = LanguageRessource.Image;
                    if (!string.IsNullOrEmpty(image))
                    {
                        var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, image);
                        LanguageRessource.Image = imageURL;
                    }

                }
                _mealTimingService.Add(mealTiming);
                return Created(nameof(AddMealTiming), mealTiming);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddMealTiming)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetMealTiming/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetMealTiming(int id)
        {
            try
            {
                MealTimingDTO MealTimingDTO = _mealTimingService.Get(id);
                return Ok(MealTimingDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMealTiming)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveMealTiming")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveMealTiming([FromBody] MealTimingDTO MealTiming)
        {
            try
            {
                _mealTimingService.Remove(MealTiming);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveMealTiming)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateMealTiming")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateMealTiming([FromBody] MealTimingDTO MealTiming)
        {
            try
            {
                
                _mealTimingService.Update(MealTiming);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateMealTiming)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
       /* [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       public ActionResult<List<UnlikeReasonDTO>> GetFilteredData([FromQuery] MealTimingFilterDTO filter)
        {
            var result = _mealTimingService.GetFilteredData(filter);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }*/
            
    }
}
