using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.UserAddressData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.UserAddressData
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<UserAddressTypeDTO, UserAddressType, UserAddressTypeVM> _helper;

        public UserAddressTypeController(IMapper mapper, IHelper<UserAddressTypeDTO, UserAddressType, UserAddressTypeVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllUserAddressType")]
        public IActionResult GetAllUserAddressType([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<UserAddressTypeVM> userAddressTypeVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/UserAddressType/GetAllUserAddressType?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(userAddressTypeVMList);
        }

        [HttpPost]
        [Route("AddUserAddressType")]
        public IActionResult AddUserAddressType([FromHeader] string authorization, [FromBody] UserAddressTypeVM userAddressVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/UserAddressType/AddUserAddressType", userAddressVM, authorization);
            return Created(nameof(AddUserAddressType), userAddressVM);
        }

        [HttpPut]
        [Route("UpdateUserAddressType")]
        public IActionResult UpdateUserAddressType([FromHeader] string authorization, [FromBody] UserAddressTypeVM userAddressVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/UserAddressType/UpdateuserAddressType", userAddressVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveUserAddressType")]
        public IActionResult RemoveUserAddressType([FromHeader] string authorization, [FromBody] UserAddressTypeVM userAddressVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/UserAddressType/RemoveUserAddressType", userAddressVM, authorization);
            return Ok();
        }
    }
}
