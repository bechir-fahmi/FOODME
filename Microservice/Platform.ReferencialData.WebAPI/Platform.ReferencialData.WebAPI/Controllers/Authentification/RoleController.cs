using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.SharedClasses.Pagination;
using System.Security.Claims;

namespace Platform.ReferencialData.WebAPI.Controllers.Authentification
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger, IMapper mapper)
        {
            _roleService = roleService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var rolesDTO = await _roleService.GetAllRoles(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = rolesDTO.TotalCount,
                    PageSize = rolesDTO.PageSize,
                    CurrentPage = rolesDTO.CurrentPage,
                    TotalPages = rolesDTO.TotalPages,
                    HasNext = rolesDTO.HasNext,
                    HasPrevious = rolesDTO.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(rolesDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAll)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpGet("GetRole/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string Id)
        {
            try
            {
                var roleDTO = await _roleService.GetRole(Id);
                return Ok(roleDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpGet("GetRoleByUserName/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoleByUserName(string userName)
        {
            try
            {
                List<RoleDTO> roles = _roleService.GetRolesByUserName(userName);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetRoleByUserName)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [HttpPost]
        [Route("AddRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateRoleDTO roleDTO)
        {
            try
            {
                //string? userId = HttpContext.User.FindFirstValue("Id");
                //if (userId == null)
                //{
                //    return Unauthorized("user unauthorize");
                //};
                var userId = "SUPER ADMIN";
                var createRoleResult = await _roleService.CreateRole(roleDTO, userId);
                return Ok(createRoleResult);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Create)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }


        [HttpPatch]
        [Route("UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] RoleDTO roleDTO)
        {
            try
            {
                var updateRoleResult = await _roleService.UpdateRole(roleDTO);

                return Ok(updateRoleResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Update)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpDelete]
        [Route("RemoveRole/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string roleId)
        {
            try
            {
                var deleteResult = await _roleService.DeleteRole(roleId);
                return Ok(deleteResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Delete)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
