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
    public class RegionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<RegionDTO, Region, RegionVM> _helper;

        public RegionController(IMapper mapper, IHelper<RegionDTO, Region, RegionVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public IActionResult GetAllRegions([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<RegionVM> regionVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Region/GetAllRegions?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(regionVMList);
        }

        [HttpPost]
        [Route("AddRegion")]
        public IActionResult AddRegion([FromHeader] string authorization, [FromBody] RegionVM regionVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/Region/AddRegion", regionVM, authorization);
            return Created(nameof(AddRegion), regionVM);
        }
        [HttpGet]
        [Route("GetRegionById/{id}")]
        public IActionResult GetRegionById([FromHeader] string authorization, int id)
        {
            var RegionVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/Region/GetRegion/{id}", authorization);

            if (RegionVM == null)
            {
                return NotFound();
            }

            return Ok(RegionVM);
        }

        [HttpPut]
        [Route("UpdateRegion")]
        public IActionResult UpdateRegion([FromHeader] string authorization, [FromBody] RegionVM regionVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/Region/UpdateRegion", regionVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveRegion")]
        public IActionResult RemoveRegion([FromHeader] string authorization, [FromBody] RegionVM regionVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/Region/RemoveRegion", regionVM, authorization);
            return Ok();
        }
        [HttpGet]
        [Route("GetFilteredData")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] RegionFillter filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<RegionVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Region/GetFilteredData?Id={filterRequest.Id}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<RegionVM>.ToGenericPagedList(vmData, pagedParameters);

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
