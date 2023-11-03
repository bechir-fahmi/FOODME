using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.WebAPI.Controllers.Authentification
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleClaimController : ControllerBase
    {
        private readonly ILogger<UserClaimController> _logger;
        private readonly IMapper _mapper;

        private readonly IRoleClaimService _roleClaimService;

        public RoleClaimController(IRoleClaimService roleClaimService, ILogger<UserClaimController> logger, IMapper mapper)
        {
            _roleClaimService = roleClaimService;
            _logger = logger;
            _mapper = mapper;
        }

        //[Authorize(Policy = "GetAllRoles")]
        [HttpGet("{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClaimsFromRole(string roleId)
        {
            try
            {
                var allRoles = await _roleClaimService.GetAllClaimsFromRole(roleId);
                return Ok(allRoles);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllClaimsFromRole)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        //[Authorize(Policy = "AddClaimsToRole")]
        [HttpPost("AddClaimsToRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDTO> AddClaimsToRole(string roleId, IList<RoleClaimDTO> claims)
        {
            try
            {
                ResponseDTO addResult = await _roleClaimService.AddClaimsToRole(roleId, claims);
                return addResult;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddClaimsToRole)}");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    Error = $"Internal server Error. Please try later, error : {ex}"
                };
            }
        }

        //[Authorize(Policy = "AddClaimsToRole")]
        [HttpPatch("UpdateClaimsInRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDTO> UpdateClaimsInRole(RoleDTO roleDTO)
        {
            try
            {
                ResponseDTO updateResult = await _roleClaimService.UpdateClaimsInRole(roleDTO);
                return updateResult;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateClaimsInRole)}");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    Error = $"Internal server Error. Please try later, error : {ex}"
                };
            }
        }


        //[Authorize(Policy = "AddClaimsToRole")]
        [HttpDelete("DeleteClaimsFromRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDTO> DeleteClaimsFromRole(RoleDTO roleDTO)
        {
            try
            {
                ResponseDTO deleteResult = await _roleClaimService.DeleteClaimsFromRole(roleDTO);
                return deleteResult;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteClaimsFromRole)}");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    Error = $"Internal server Error. Please try later, error : {ex}"
                };
            }
        }
    }
}
