using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.DtoModel;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.ZoneData
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<ZoneDTO, Zone, ZoneVM> _helper;
        private readonly IWebHostEnvironment _env;

        public ZoneController(IMapper mapper,
            IHelper<ZoneDTO, Zone, ZoneVM> helper,
            IWebHostEnvironment env)
        {
            _mapper = mapper;
            _helper = helper;
            _env = env;
        }

        [HttpGet]
        [Route("GetAllZones")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<ZoneVM> ZoneVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Zone/GetAllZones?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(ZoneVMList);
        }
        [HttpGet]
        [Route("GetZone/{id}")]
        public IActionResult Get([FromHeader] string authorization, int id)
        {
            ZoneVM ZoneVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/Zone/GetZoneById/{id}", authorization);

            return Ok(ZoneVM);
        }

        [HttpPost]
        [Route("AddZone")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] ZoneVM ZoneVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/Zone/AddZone", ZoneVM, authorization);
            return Created(nameof(Add), ZoneVM);
        }

        [HttpPut]
        [Route("UpdateZone")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] ZoneVM ZoneVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/Zone/UpdateZone", ZoneVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveBand")]
        public IActionResult Remove([FromHeader] string authorization,[FromBody] ZoneVM ZoneVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/Zone/RemoveZone",ZoneVM, authorization);
            return Ok();
        }
    }
}
