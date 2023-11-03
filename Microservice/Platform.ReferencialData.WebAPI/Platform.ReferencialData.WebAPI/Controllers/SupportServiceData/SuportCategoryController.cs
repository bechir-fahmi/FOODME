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
    public class SuportCategoryController : ControllerBase
    {
        private readonly ISuportCategoryService _suportCategoryService;
        private readonly ILogger<SuportCategoryController> _logger;
        private readonly IMapper _mapper;

        public SuportCategoryController(ISuportCategoryService suportCategoryService, ILogger<SuportCategoryController> logger, IMapper mapper)
        {
            _suportCategoryService = suportCategoryService;
            _logger = logger;
            _mapper = mapper;
        }



        [Route("GetAllSuportCategory")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllSuportCategory([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var SuportCategoryList = _suportCategoryService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = SuportCategoryList.TotalCount,
                    PageSize = SuportCategoryList.PageSize,
                    CurrentPage = SuportCategoryList.CurrentPage,
                    TotalPages = SuportCategoryList.TotalPages,
                    HasNext = SuportCategoryList.HasNext,
                    HasPrevious = SuportCategoryList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(SuportCategoryList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllSuportCategory)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }



        [Route("AddSuportCategory")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddSuportCategory([FromBody] SuportCategoryDTO SuportCategory)
        {
            try
            {
                _suportCategoryService.Add(SuportCategory);
                return Created(nameof(AddSuportCategory), SuportCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddSuportCategory)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetSuportCategory/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSuportCategory(int id)
        {
            try
            {
                SuportCategoryDTO SuportCategoryDTO = _suportCategoryService.Get(id);
                return Ok(SuportCategoryDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetSuportCategory)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveSuportCategory/{VendorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveSuportCategory(int Id)
        {
            try
            {
                _suportCategoryService.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveSuportCategory)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateSuportCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSuportCategory(SuportCategoryDTO SuportCategory)
        {
            try
            {
                _suportCategoryService.Update(SuportCategory);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateSuportCategory)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
