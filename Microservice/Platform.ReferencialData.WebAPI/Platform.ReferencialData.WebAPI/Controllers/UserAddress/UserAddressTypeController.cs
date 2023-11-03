using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.UserAddressData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.UserAddress
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressTypeController : ControllerBase
    {
        private readonly IUserAddressTypeService _UserAddressTypeService;
        private readonly ILogger<UserAddressTypeController> _logger;
        private readonly IMapper _mapper;

        public UserAddressTypeController(IUserAddressTypeService userAddressTypeService, ILogger<UserAddressTypeController> logger, IMapper mapper)
        {
            _UserAddressTypeService = userAddressTypeService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllUserAddressType")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUserAddressType([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var userAddressTypeList = _UserAddressTypeService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = userAddressTypeList.TotalCount,
                    PageSize = userAddressTypeList.PageSize,
                    CurrentPage = userAddressTypeList.CurrentPage,
                    TotalPages = userAddressTypeList.TotalPages,
                    HasNext = userAddressTypeList.HasNext,
                    HasPrevious = userAddressTypeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(userAddressTypeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllUserAddressType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("GetAllActiveUserAddressType")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveUserAddressType([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var userAddressTypeList = _UserAddressTypeService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = userAddressTypeList.TotalCount,
                    PageSize = userAddressTypeList.PageSize,
                    CurrentPage = userAddressTypeList.CurrentPage,
                    TotalPages = userAddressTypeList.TotalPages,
                    HasNext = userAddressTypeList.HasNext,
                    HasPrevious = userAddressTypeList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(userAddressTypeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveUserAddressType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("AddUserAddressType")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddUserAddressType([FromBody] UserAddressTypeDTO userAddressType)
        {
            try
            {
                _UserAddressTypeService.Add(userAddressType);
                return Created(nameof(AddUserAddressType), userAddressType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddUserAddressType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveUserAddressType/{userAddressTypeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveUserAddressType(int userAddressTypeId)
        {
            try
            {
                _UserAddressTypeService.Remove(userAddressTypeId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveUserAddressType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateuserAddressType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateuserAddressType(UserAddressTypeDTO userAddressType)
        {
            try
            {
                _UserAddressTypeService.Update(userAddressType);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateuserAddressType)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
