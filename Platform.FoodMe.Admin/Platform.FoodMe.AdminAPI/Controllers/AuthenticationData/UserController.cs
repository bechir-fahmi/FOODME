
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IHelper<UserDTO, User, UserVM> _helper;
        private readonly IHelper<UserDTO, User, CreateOrEditUserDto> CreateHelper;

        public UserController(IMapper mapper, IHelper<UserDTO, User, UserVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<UserVM> UserVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/User/GetAllUsers?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (UserVMList != null && UserVMList.Count() > 0)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                
                return Ok(UserVMList);
            }
            else
            {
                return Unauthorized();
            }  
        }

        [HttpGet]
        [Route("GetUser/id/{id}")]
        public IActionResult Get([FromHeader] string authorization, string id)
        {
            UserVM UserVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/User/GetUser/id/{id}", authorization);
            if (UserVM != null)
                return Ok(UserVM);
            return Unauthorized();
        }



        [HttpGet]
        [Route("GetCurrentUser")]
        public IActionResult GetCurrentUser([FromHeader] string authorization)
        {
            UserVM UserVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/User/Current", authorization);
            if (UserVM != null)
                return Ok(UserVM);
            return Unauthorized();

        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] UserVM userVM)
        {
            //userVM.CreationTime = DateTime.Now;
            //userVM.Id = Guid.NewGuid().ToString();
            if (userVM == null || userVM.Roles.Any() || userVM.Roles.Any(x=>x.Name != "ADMINISTRATOR" && x.Name != "VENDOR_ADMINISTRATOR"))
            {
                return Ok(new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Error = "Error while creating user, reasons Type Role incorrect",
                });
            }
            _helper.Create(_mapper, $"{Microservice.RefData}/User/AddUser", userVM, authorization);
            return Created(nameof(Add), userVM);
        }

        [HttpPatch]
        [Route("UpdateUser")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] UserVM userVM)
        {
            _helper.UpdatePart(_mapper, $"{Microservice.RefData}/User/UpdateUser", userVM, authorization);
            return Ok();
        }

        [HttpPatch]
        [Route("AddUserToBlackList/{UserId}")]
        public IActionResult AddUserToBlackList([FromHeader] string authorization, string UserId)
        {
            var response = _helper.ConsumeAPI($"{Microservice.RefData}/User/AddUserToBlackList/{UserId}", System.Net.Http.HttpMethod.Patch);
            response.EnsureSuccessStatusCode();
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveUser/{userId}")]
        public IActionResult Remove([FromHeader] string authorization, string userId)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/User/RemoveUser/{userId}", authorization);
            return Ok();
        }
    }
}
