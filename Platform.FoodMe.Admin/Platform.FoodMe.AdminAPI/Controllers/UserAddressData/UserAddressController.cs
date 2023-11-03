using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.UserAddressData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.Authentication.UserAddressData
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<UserAddressDTO, UserAddress, UserAddressVM> _helper;

        public UserAddressController(IMapper mapper, IHelper<UserAddressDTO, UserAddress, UserAddressVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }
        
        [HttpGet]
        [Route("GetAllUserAddress")]
        public IActionResult GetAllUserAddress([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<UserAddressVM> userAddressVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/UserAddress/GetAllUserAddress?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(userAddressVMList);

        }

        [HttpGet]
        [Route("GetUserAddressById/{id}")]
        public IActionResult GetUserAddressById([FromHeader] string authorization, int id)
        {
            var userAddress =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/UserAddress/GetUserAddressById/{id}", authorization);

            if (userAddress == null)
            {
                return NotFound();
            }

            return Ok(userAddress);
        }

        [HttpGet]
        [Route("GetUserAddressByUserId/{userId}")]
        public IActionResult GetUserAddressByUserId([FromHeader] string authorization, string userId)
        {
            var userAddressList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/UserAddress/GetUserAddressByUserId/{userId}", authorization);

            if (userAddressList == null)
            {
                return NotFound();
            }

            return Ok(userAddressList);
        }

        [HttpPost]
        [Route("AddUserAddress")]
        public IActionResult AddUserAddress([FromHeader] string authorization, [FromBody] UserAddressVM userAddressVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/UserAddress/AddUserAddress", userAddressVM, authorization);
            return Created(nameof(AddUserAddress), userAddressVM);
        }

        [HttpPut]
        [Route("UpdateUserAddress")]
        public IActionResult UpdateUserAddress([FromHeader] string authorization, [FromBody] UserAddressVM userAddressVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/UserAddress/UpdateuserAddress", userAddressVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveUserAddress")]
        public IActionResult RemoveUserAddress([FromHeader] string authorization, [FromBody] UserAddressVM userAddressVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/UserAddress/RemoveUserAddress", userAddressVM, authorization);
            return Ok();
        }
    }
}
