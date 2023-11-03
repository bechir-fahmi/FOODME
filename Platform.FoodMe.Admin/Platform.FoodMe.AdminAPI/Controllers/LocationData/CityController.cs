using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.LocationData
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<CityDTO, City, CityVM> _helper;

        public CityController(IMapper mapper, IHelper<CityDTO, City, CityVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllCities")]
        public IActionResult GetAllCities([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<CityVM> cityVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/City/GetAllCities?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(cityVMList);
        }

        [HttpPost]
        [Route("AddCity")]
        public IActionResult AddCity([FromHeader] string authorization, [FromBody] CityVM cityVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/City/AddCity", cityVM, authorization);
            return Created(nameof(AddCity), cityVM);
        }
        [HttpGet]
        [Route("GetCityById/{id}")]
        public IActionResult GetCityById([FromHeader] string authorization, int id)
        {
            var CityVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/City/GetCity/{id}", authorization);

            if (CityVM == null)
            {
                return NotFound();
            }

            return Ok(CityVM);
        }

        [HttpPut]
        [Route("UpdateCity")]
        public IActionResult UpdateCity([FromHeader] string authorization, [FromBody] CityVM cityVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/City/UpdateCity", cityVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveCity")]
        public IActionResult RemoveCity([FromHeader] string authorization, [FromBody] CityVM cityVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/City/RemoveCity", cityVM, authorization);
            return Ok();
        }
        [HttpGet]
        [Route("GetFilteredData")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] CityFillter filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<CityVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/City/GetFilteredData?Id={filterRequest.Id}&CityCode={filterRequest.CityCode}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<CityVM>.ToGenericPagedList(vmData, pagedParameters);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(refDataList);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
