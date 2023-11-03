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
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<RoleDTO, Role, RoleVM> _helper;

        public RoleController(IMapper mapper, IHelper<RoleDTO, Role, RoleVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<RoleVM> RoleVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Role/GetAllRoles?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(RoleVMList);
        }

        [HttpGet]
        [Route("GetRole/{id}")]
        public IActionResult Get([FromHeader] string authorization, string id)
        {
            RoleVM RoleVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/Role/GetRole/{id}", authorization);

            return Ok(RoleVM);
        }

        [HttpPost]
        [Route("AddRole")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] RoleVM roleVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/Role/AddRole", roleVM, authorization);
            return Created(nameof(Add), roleVM);
        }

        [HttpPut]
        [Route("UpdateRole")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] RoleVM roleVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/Role/UpdateRole", roleVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveRole")]
        public IActionResult Remove([FromHeader] string authorization, [FromBody] RoleVM roleVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/Role/RemoveRole", roleVM, authorization);
            return Ok();
        }
    }
}
