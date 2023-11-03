using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.SharedClasses.Pagination;
using System.Security.Claims;
using Platform.Shared.Enum;
using Platform.ReferencialData.Business.business_services_implementations.Authentication;
using Microsoft.Build.Framework;

namespace Platform.ReferencialData.WebAPI.Controllers.Authentification
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IUserRoleService _userRoleService;
       
            public UserController(IUserService userService,
            IUserRoleService userRoleService,
            IConfiguration configuration,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        [AllowAnonymous]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var usersDTOList = _userService.GetAllUsersAsync(pagedParameters);
                foreach (var userDTO in usersDTOList)
                {
                    userDTO.Roles = (List<RoleDTO>)_userRoleService.GetUserRoles(userDTO.Id).Result;
                }
                PaginationData metadata = new PaginationData
                {
                    TotalCount = usersDTOList.TotalCount,
                    PageSize = usersDTOList.PageSize,
                    CurrentPage = usersDTOList.CurrentPage,
                    TotalPages = usersDTOList.TotalPages,
                    HasNext = usersDTOList.HasNext,
                    HasPrevious = usersDTOList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(usersDTOList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAll)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        
        [HttpGet("GetAllUsers/{usertype}")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.ViewAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsersByUserType([FromQuery] PagedParameters pagedParameters, string usertype)
        {
            try
            {
                var usersDTOList = _userService.GetUsersByUserTypeAsync(pagedParameters, usertype);
                foreach (var userDTO in usersDTOList)
                {
                    userDTO.Roles = (List<RoleDTO>) _userRoleService.GetUserRoles(userDTO.Id).Result;
                }

                PaginationData metadata = new PaginationData
                {
                    TotalCount = usersDTOList.TotalCount,
                    PageSize = usersDTOList.PageSize,
                    CurrentPage = usersDTOList.CurrentPage,
                    TotalPages = usersDTOList.TotalPages,
                    HasNext = usersDTOList.HasNext,
                    HasPrevious = usersDTOList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(usersDTOList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAll)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [HttpGet("GetUser/id/{id}")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string id)
        {
            try
            {
                var userDTO = _userService.GetUserAsync(id);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


        [HttpGet("GetUserByPhoneNumber/{phoneNumber}")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                var userDTO = _userService.GetUserByPhoneNumber(phoneNumber);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetUserByPhoneNumber)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


        [HttpGet("Current")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCurrent()
        {
            try
            {
                string? id = HttpContext.User.FindFirstValue("Id");
                if (id == null)
                {
                    return Unauthorized("User unauthorized");
                }
                var userDTO =  _userService.GetUserAsync(id);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPost]
        [Route("AddUser")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        {
            try
            {
                _logger.LogInformation($"Adding user");
                var addResult = await _userService.AddUser(userDTO);
                if (addResult.StatusCodes == 200)
                {
                    var emailBody = "Please confirm your email address <a href=\"#URL#\">Click here</a>";
                    var confirmationUrl = Url.Action("ConfirmEmail", "Account", new { email = userDTO.Email, code = addResult.Token }, Request.Scheme);
                    var email = emailBody.Replace("#URL#", confirmationUrl);
                    EmailHelperSMTP emailHelper = new EmailHelperSMTP();

                    bool emailResponse = emailHelper.SendEmail(userDTO.Email, email, _configuration);
                    if (emailResponse)
                    {
                        return Ok(addResult);
                    }
                    return BadRequest(new
                    {
                        Error = "Log email failed"
                    });

                }
                return BadRequest(new
                {
                    Error = "User Added but email not send it !"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddUser)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPost]
        [Route("AddUserByAdmin")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserByAdmin([FromBody] UserDTO userDTO)
        {
            try
            {
                _logger.LogInformation($"Adding user");
                var addResult = await _userService.AddUserByAdmin(userDTO);
                if (addResult.StatusCodes == 200)
                {
                    var emailBody = "Please confirm your email address <a href=\"#URL#\">Click here</a>";
                    var confirmationUrl = Url.Action("ConfirmEmail", "Account", new { email = userDTO.Email, code = addResult.Token }, Request.Scheme);
                    var email = emailBody.Replace("#URL#", confirmationUrl);
                    EmailHelperSMTP emailHelper = new EmailHelperSMTP();

                    bool emailResponse = emailHelper.SendEmail(userDTO.Email, email, _configuration);
                    if (emailResponse)
                    {
                        return Ok(addResult);
                    }
                    return BadRequest(new
                    {
                        Error = "User Added but email is not sent !"
                    });

                }
                else if (addResult.StatusCodes == 401|| addResult.StatusCodes==404)
                    {
                    return BadRequest(addResult);
                    }
                
                return BadRequest(new
                {
                    Error = "an error occured while adding the user"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddUser)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


        [HttpPatch]
        [Route("UpdateUser")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] UserDTO userDTO)
        {
            try
            {
                var Id = HttpContext.User.FindFirstValue("Id");
                userDTO.Id  = Id;
                var updateResult = _userService.Update(userDTO).Result;
                if (updateResult.StatusCodes == 200)
                {
                    if (updateResult.Token != "")
                    {
                        var emailBody = "Please confirm your email address <a href=\"#URL#\">Click here</a>";
                        var confirmationUrl = Url.Action("ConfirmEmail", "Account", new { email = userDTO.Email, code = updateResult.Token }, Request.Scheme);
                        var email = emailBody.Replace("#URL#", confirmationUrl);
                        EmailHelperSMTP emailHelper = new EmailHelperSMTP();

                        bool emailResponse = emailHelper.SendEmail(userDTO.Email, email, _configuration);
                        if (emailResponse)
                        {
                            return Ok(updateResult);
                        }
                        return BadRequest(new
                        {
                            Error = "email failed to be send"
                        });

                    }
                    return Ok(updateResult);
                }
                return BadRequest(updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Update)}");
                return StatusCode(500, $"Internal server Error: {ex}");
            }
        }

        [HttpPatch]
        [Route("UpdateUserByAdmin")]
        //[Authorize("Permissions.UserManagement.Update")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUserByAdmin([FromBody] UserDTO userDTO)
        {
            try
            {
                var updateResult= _userService.UpdateUserByAdmin(userDTO).Result;
                if (updateResult.StatusCodes == 200)
                {
                    if (updateResult.Token != "")
                    {
                        var emailBody = "Please confirm your email address <a href=\"#URL#\">Click here</a>";
                        var confirmationUrl = Url.Action("ConfirmEmail", "Account", new { email = userDTO.Email, code = updateResult.Token }, Request.Scheme);
                        var email = emailBody.Replace("#URL#", confirmationUrl);
                        EmailHelperSMTP emailHelper = new EmailHelperSMTP();

                        bool emailResponse = emailHelper.SendEmail(userDTO.Email, email, _configuration);
                        if (emailResponse)
                        {
                            return Ok(updateResult);
                        }
                        return BadRequest(new
                        {
                            Error = "email failed to be send"
                        });

                    }
                    return Ok(updateResult);
                }
                return BadRequest(updateResult);
            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Update)}");
                return StatusCode(500, ex);
            }
        }
        [HttpPatch]
        [Route("UpdateUserStatus/{id}/{status}")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.Update")]
        //[Authorize("Permissions.UserManagement.Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUserStatus(string id, string status)
        {
            try
            {
                Status userStatus = (Status)Enum.Parse(typeof(Status), status, ignoreCase: true);
                _userService.Update(id, userStatus) ;
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Update)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPatch]
        [Route("AddUserToBlackList/{UserId}")]
        [AllowAnonymous]
        //[Authorize("Permissions.All.Delete")]
        //[Authorize("Permissions.UserManagement.Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddUserToBlackList(string UserId)
        {
            try
            {
                _userService.Update(UserId, Status.isBlackListed);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddUserToBlackList)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        [Route("RemoveUser/{userId}")]
        [AllowAnonymous]
        //[Authorize("Permissions.UserManagement.Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _userService.Delete(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Delete)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        /**************************************************___ GetUserByGender___**********************************************************/
        [HttpGet("GetAllGender")]
        [AllowAnonymous]
        //[Authorize("Permissions.Dashboard.ViewAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllGender()
        {
            var userVMList = await _userService.GetAllUsersWithGenderAsync();

            if (userVMList != null && userVMList.Count > 0)
            {
                int totalUsers = userVMList.Count;
                int maleCount = userVMList.Count(user => user.Gender == "Male");
                int femaleCount = userVMList.Count(user => user.Gender == "Female");
                int malePercentage = 0;
                int femalePercentage = 0;
                if (totalUsers > 0)
                {
                    malePercentage = GetPercentage(maleCount, totalUsers);
                    femalePercentage = GetPercentage(femaleCount, totalUsers);
                }
                var result = new
                {
                    MaleCount = maleCount,
                    FemaleCount = femaleCount,
                    MalePercentage = malePercentage,
                    FemalePercentage = femalePercentage,
                    // Users = userVMList, 
                    TotalUsers = totalUsers
                };

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        private int GetPercentage(int count, int totalUsers)
        {
            decimal percentage = (decimal)count / totalUsers * 100;
            return (int)percentage;
        }


        /**************************************************___ End GetUserByGender___**********************************************************/

        /**************************************************___ Costomer last week ___**********************************************************/
        [HttpGet("GetAllGenderLastWeek")]
        [AllowAnonymous]
        //[Authorize("Permissions.Dashboard.ViewAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllGenderLastWeek()
        {
            var userBMList = await _userService.GetCustomerLastweekAsync();

            if (userBMList != null && userBMList.Count > 0)
            {

                DateTime currentDate = DateTime.Now;


                DateTime lastWeek = currentDate.AddDays(-7);
                var lastWeekUsers = userBMList.Where(user => user.CreationTime >= lastWeek);

                int maleCount = lastWeekUsers.Count(user => user.Gender == "Male");
                int femaleCount = lastWeekUsers.Count(user => user.Gender == "Female");

                int totalUsers = maleCount + femaleCount;
                int malePercentage = 0;
                int femalePercentage = 0;
                if (totalUsers > 0) { 
                malePercentage = GetPercentage(maleCount, totalUsers);
                femalePercentage = GetPercentage(femaleCount, totalUsers);
                }
                var result = new
                {
                    MaleCount = maleCount,
                    FemaleCount = femaleCount,
                    MalePercentage = malePercentage,
                    FemalePercentage = femalePercentage,
                    TotalUsers = totalUsers
                };

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        /**************************************************___End Costomer last week ___*******************************************************/

        /**************************************************___ Costomer last Day ___**********************************************************/
        [HttpGet("GetAllGenderLastDay")]
        [AllowAnonymous]
        //[Authorize("Permissions.Dashboard.ViewAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllGenderLastDay()
        {
            var userBMList = await _userService.GetCustomerLastDayAsync();

            if (userBMList != null && userBMList.Count > 0)
            {

                DateTime currentDate = DateTime.Now;


                DateTime lastDay = currentDate.AddHours(-24);
                var lastDayUsers = userBMList.Where(user => user.CreationTime >= lastDay);

                int maleCount = lastDayUsers.Count(user => user.Gender == "Male");
                int femaleCount = lastDayUsers.Count(user => user.Gender == "Female");

                int totalUsers = maleCount + femaleCount;
                int malePercentage = 0;
                int femalePercentage = 0;
                if (totalUsers > 0)
                {
                    malePercentage = GetPercentage(maleCount, totalUsers);
                    femalePercentage = GetPercentage(femaleCount, totalUsers);
                }
                var result = new
                {
                    MaleCount = maleCount,
                    FemaleCount = femaleCount,
                    MalePercentage = malePercentage,
                    FemalePercentage = femalePercentage,
                    TotalUsers = totalUsers
                };


                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
