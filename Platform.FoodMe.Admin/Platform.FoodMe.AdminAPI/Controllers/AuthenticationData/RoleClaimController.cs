using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.MicroservicesURLs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Platform.FoodMe.AdminAPI.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleClaimController : ControllerBase
    {
        private readonly IMapper _mapper;
        HttpClient httpClient = new HttpClient();

        public RoleClaimController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetAllClaimsFromRole([FromHeader] string authorization, string roleId)
        {
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{Microservice.RefData}/RoleClaim/{roleId}"))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var response = httpClient.Send(request);

                    if (response.IsSuccessStatusCode)
                    {
                        using var stream = response.Content.ReadAsStream();
                        RoleClaimDTO allClaims = JsonSerializer.Deserialize<RoleClaimDTO>(stream, jsonOptions);
                        //RoleClaimvm = _mapper.Map<UserVM>(userDTO);
                        return Ok(allClaims);
                    }
                    return Unauthorized(httpClient);
                }

                return BadRequest();
            }

        }

        [HttpPost("AddClaimsToRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddClaimsToRole([FromHeader] string authorization, RoleDTO roleDTO)
        {
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{Microservice.RefData}/RoleClaim/AddClaimsToRole"))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var response = httpClient.Send(request);

                    if (response.IsSuccessStatusCode)
                    {
                        using var stream = response.Content.ReadAsStream();
                        ResponseDTO responseDTO = JsonSerializer.Deserialize<ResponseDTO>(stream, jsonOptions);
                        //RoleClaimvm = _mapper.Map<UserVM>(userDTO);
                        return Ok(responseDTO);
                    }
                    return Unauthorized(response);
                }

                return BadRequest();
            }
        }
    }
}
