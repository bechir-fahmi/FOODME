using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.GenericRepository;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.DtoModel.UserStatus;

namespace Platform.Tracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserStatusController : Controller
    {
        private readonly ILogger<UserStatusController> _logger;
        private readonly IUserStatusService _userStatusService;
        public UserStatusController(ILogger<UserStatusController> logger, IUserStatusService userStatusService)
        {
            _logger = logger;
            _userStatusService = userStatusService;
        }


        [Route("GetAllUserByStatus/{status}")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUserByStatus(bool status)
        {
            try
            {
                var userStatusList= _userStatusService.GetAllUserByStatus(status);
                return Ok(userStatusList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(GetAllUserByStatus)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

        [Route("GetAllUserStatus")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUserStatus()
        {
            try
            {
                var userStatusList = _userStatusService.GetAllUserStatus();
                return Ok(userStatusList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(GetAllUserStatus)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
           
        }

        [Route("GetAllUserByStatusAndPeridique")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUserByStatusAndPeridique([FromQuery] Boolean status, DateTime startDate, DateTime endDate)
        {
            try
            {
                var userStatusList = _userStatusService.GetAllUserByStatusAndPeridique(status,startDate,endDate);
                return Ok(userStatusList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(GetAllUserByStatusAndPeridique)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

        [Route("AddActiveUser")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddActiveUser ([FromBody] UserStatusDTO userStatusDTO)
        {
            try{
                _userStatusService.AddActiveUser(userStatusDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddActiveUser)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
        }

        [Route("UpdateLogOutUser")]
        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLogOutUser([FromBody] UserStatusDTO userStatusDTO)
        {
            try
            {
                _userStatusService.UpdateLogOutUser(userStatusDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateLogOutUser)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }
        }

    }
}
