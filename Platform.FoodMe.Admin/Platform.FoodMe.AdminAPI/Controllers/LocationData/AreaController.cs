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
    public class AreaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<AreaDTO, Area, AreaVM> _helper;

        public AreaController(IMapper mapper, IHelper<AreaDTO, Area, AreaVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllAreas")]
        public IActionResult GetAllAreas([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<AreaVM> areaVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Area/GetAllAreas?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(areaVMList);
        }

        [HttpPost]
        [Route("AddArea")]
        public IActionResult AddArea([FromHeader] string authorization, [FromBody] AreaVM areaVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/Area/AddArea", areaVM, authorization);
            return Created(nameof(AddArea), areaVM);
        }
        [HttpGet]
        [Route("GetAreaById/{id}")]
        public IActionResult GetAreaById([FromHeader] string authorization, int id)
        {
            var AreaVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/Area/GetArea/{id}", authorization);

            if (AreaVM == null)
            {
                return NotFound();
            }

            return Ok(AreaVM);
        }

        [HttpPut]
        [Route("UpdateArea")]
        public IActionResult UpdateArea([FromHeader] string authorization, [FromBody] AreaVM ariaVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/Area/UpdateArea", ariaVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveArea/{areaId}")]
        public IActionResult RemoveArea([FromHeader] string authorization, int areaId)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/Area/RemoveArea/{areaId}", authorization);
            return Ok();
        }

        [HttpGet]
        [Route("GetFilteredData")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] AreaFillter filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<AreaVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Area/GetFilteredData?Id={filterRequest.Id}&AreaCode={filterRequest.AreaCode}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<AreaVM>.ToGenericPagedList(vmData, pagedParameters);

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
