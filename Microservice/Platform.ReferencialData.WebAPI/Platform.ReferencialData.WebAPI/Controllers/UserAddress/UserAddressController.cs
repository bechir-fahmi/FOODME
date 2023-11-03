using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressService _UserAddressService;
        private readonly ILogger<UserAddressController> _logger;
        private readonly IMapper _mapper;

        public UserAddressController(ILogger<UserAddressController> logger, IMapper mapper, IUserAddressService userAddressService)
        {
            _logger = logger;
            _mapper = mapper;
            _UserAddressService = userAddressService;
        }

        [Route("GetAllUserAddress")]
      //  [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUserAddress([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var userAddressList = _UserAddressService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = userAddressList.TotalCount,
                    PageSize = userAddressList.PageSize,
                    CurrentPage = userAddressList.CurrentPage,
                    TotalPages = userAddressList.TotalPages,
                    HasNext = userAddressList.HasNext,
                    HasPrevious = userAddressList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(userAddressList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllUserAddress)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }
        [Route("GetAllActiveUserAddress")]
        //  [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllActiveUserAddress([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var userAddressList = _UserAddressService.GetAllActiveData(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = userAddressList.TotalCount,
                    PageSize = userAddressList.PageSize,
                    CurrentPage = userAddressList.CurrentPage,
                    TotalPages = userAddressList.TotalPages,
                    HasNext = userAddressList.HasNext,
                    HasPrevious = userAddressList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(userAddressList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveUserAddress)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("AddUserAddress")]
        /*[Authorize]*/
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddUserAddress([FromBody] UserAddressDTO userAddress)
        {
            try
            {
                _UserAddressService.Add(userAddress);
                return Created(nameof(AddUserAddress), userAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddUserAddress)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        /*[Authorize]*/
        [Route("RemoveUserAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveUserAddress([FromBody] UserAddressDTO userAddress)
        {
            try
            {
                _UserAddressService.Remove(userAddress);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveUserAddress)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateuserAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateuserAddress(UserAddressDTO userAddress)
        {
            try
            {
                _UserAddressService.Update(userAddress);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateuserAddress)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("GetUserAddressById/{id}")]
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserAddressById(int id)
        {
            try
            {
                var refData = _UserAddressService.Get(id);
                if (refData == null)
                    return NotFound();
                return Ok(refData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetUserAddressById)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("GetUserAddressByUserId/{userId}")]
      //  [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserAddressByUserId(string userId)
        {
            try
            {
                var userAddressList = _UserAddressService.GetUserAddressByUserId(userId);
                return Ok(userAddressList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetUserAddressByUserId)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("GetUserAddressActiveByUserId/{userId}")]
        //  [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserAddressActiveByUserId(string userId)
        {
            try
            {
                var userAddressActiveList = _UserAddressService.GetUserAddressActiveByUserId(userId);
                return Ok(userAddressActiveList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetUserAddressActiveByUserId)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
