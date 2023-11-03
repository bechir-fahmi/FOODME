using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.MicroservicesURLs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Platform.FoodMe.AdminAPI.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        HttpClient httpClient = new HttpClient();

        public AccountController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] LoginVM userLogin)
        {
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{Microservice.RefData}/Account/login"))
            {
                var user = _mapper.Map<User>(userLogin);

                var loginDTO = _mapper.Map<LoginDTO>(user);

                var json = JsonSerializer.Serialize(loginDTO);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = httpClient.Send(request);
                var stream = response.Content.ReadAsStream();
                ResponseDTO responseDTO = JsonSerializer.Deserialize<ResponseDTO>(stream, jsonOptions);
                var responseBM = _mapper.Map<Response>(responseDTO);
                var responseVM = _mapper.Map<ResponseVM>(responseBM);


                if (!response.IsSuccessStatusCode)
                {
                    return Unauthorized(responseVM);
                }
                return Ok(responseVM);

            }
        }

        [HttpGet]
        [Route("refreshToken")]
        [AllowAnonymous]
        public IActionResult GetRefreshToken([FromHeader] string authorization)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{Microservice.RefData}/Account/GetRefreshToken"))
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    // we have a valid AuthenticationHeaderValue that has the following details:

                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;


                    HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(scheme, parameter);

                    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                    var response = httpClient.Send(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(response);
                    }

                    return BadRequest(response);
                }
                return Unauthorized();
            }
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterViewModel registerView)
        {
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{Microservice.RefData}/Account/register"))
            {

                var user = _mapper.Map<User>(registerView);
                var userRegisterDTO = _mapper.Map<RegisterDTO>(user);

                var json = JsonSerializer.Serialize(userRegisterDTO);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = httpClient.Send(request);
                using var stream = response.Content.ReadAsStream();

                Response responseDTO = JsonSerializer.Deserialize<Response>(stream, jsonOptions);
                var responseBM = _mapper.Map<Response>(responseDTO);
                var responseVM = _mapper.Map<ResponseVM>(responseBM);

                if (!response.IsSuccessStatusCode)
                {
                    return Unauthorized(responseVM);
                }
                return Ok(responseVM);
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{Microservice.RefData}/Account/login"))
            {

                var json = JsonSerializer.Serialize(email);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = httpClient.Send(request);
                using var stream = response.Content.ReadAsStream();
                ResponseDTO responseDTO = JsonSerializer.Deserialize<ResponseDTO>(stream, jsonOptions);
                var responseVM = _mapper.Map<ResponseVM>(responseDTO);
                if (!response.IsSuccessStatusCode)
                {
                    return Unauthorized(responseVM);
                }
                return Ok(responseVM);
            }
        }
    }
}
