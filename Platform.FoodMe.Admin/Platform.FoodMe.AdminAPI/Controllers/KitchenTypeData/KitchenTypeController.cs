using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.KitchenTypeData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.KitchenTypeData
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitchenTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<KitchenTypeDTO, KitchenType, KitchenTypeVM> _helper;
        private readonly IWebHostEnvironment _env;


        public KitchenTypeController(IMapper mapper, IHelper<KitchenTypeDTO, KitchenType, KitchenTypeVM> helper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _helper = helper;
            _env = env;
        }
        [HttpGet]
        [Route("GetAllkitchens")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<KitchenTypeVM> kitchenVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/KitchenType/GetAllKitchens?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(kitchenVMList);
        }    
        [HttpGet]
        [Route("GetKitchenTypesByTag")]
        public IActionResult GetKitchenTypesByTag([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<KitchenTypeVM> kitchenVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/KitchenType/GetKitchenTypesByTag?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(kitchenVMList);
        }
        [HttpGet]
        [Route("Getkitchen/{id}")]
        public IActionResult Get([FromHeader] string authorization, int id)
        {
            KitchenTypeVM kitchenVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/KitchenType/GetKitchen/{id}", authorization);

            return Ok(kitchenVM);
        }

        [HttpPost]
        [Route("Addkitchen")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] KitchenTypeVM kitchenVM)
        {
            //KitchenService.UploadkitchenImages(kitchenVM, _env.WebRootPath);
            _helper.Create(_mapper, $"{Microservice.RefData}/KitchenType/AddKitchen", kitchenVM, authorization);
            return Created(nameof(Add), kitchenVM);
        }

        [HttpPut]
        [Route("Updatekitchen")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] KitchenTypeVM kitchenVM)
        {
            //KitchenService.UploadkitchenImages(kitchenVM, _env.WebRootPath);
            _helper.Update(_mapper, $"{Microservice.RefData}/KitchenType/UpdateKitchen", kitchenVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveBand")]
        public IActionResult Remove([FromHeader] string authorization, [FromBody] KitchenTypeVM kitchenVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/KitchenType/RemoveKitchen", kitchenVM, authorization);
            return Ok();
        }
        [HttpGet]

        [Route("GetFilteredKitchenTypes")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] KitchenTypeFillter filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<KitchenTypeVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/KitchenType/GetFilteredKitchenTypes?Name={filterRequest.Name}&Status={filterRequest.Status}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<KitchenTypeVM>.ToGenericPagedList(vmData, pagedParameters);

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
