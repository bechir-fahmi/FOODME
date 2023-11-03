using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.DeliveryModeData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.DeliveryModeData;
using Platform.ReferentialData.DtoModel.DeliveryModeData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.DeliveryModeData
{
    public class DeliveryModeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<DeliveryModeDTO, DeliveryMode, DeliveryModeVM> _helper;
        private readonly IWebHostEnvironment _env;


        public DeliveryModeController(IMapper mapper, IHelper<DeliveryModeDTO,DeliveryMode, DeliveryModeVM> helper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _helper = helper;
            _env = env;
        }
        [HttpGet]
        [Route("GetAllDeliveryModes")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<DeliveryModeVM> deliveryModeVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/DeliveryMode/GetAllDeliveryModes?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(deliveryModeVMList);
        }
        [HttpGet]
        [Route("GetDeliveryMode/{id}")]
        public IActionResult Get([FromHeader] string authorization, int id)
        {
            DeliveryModeVM deliveryModeVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/DeliveryMode/GetDeliveryMode/{id}", authorization);

            return Ok(deliveryModeVM);
        }

        [HttpPost]
        [Route("AddDeliveryMode")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] DeliveryModeVM deliveryModeVM)
        {

            _helper.Create(_mapper, $"{Microservice.RefData}/DeliveryMode/AddDeliveryMode", deliveryModeVM, authorization);
            return Created(nameof(Add), deliveryModeVM);
        }

        [HttpPut]
        [Route("UpdateDeliveryMode")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] DeliveryModeVM deliveryModeVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/DeliveryMode/UpdateDeliveryMode", deliveryModeVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveDeliveryMode")]
        public IActionResult Remove([FromHeader] string authorization, [FromBody] DeliveryModeVM deliveryModeVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/DeliveryMode/RemoveDeliveryMode", deliveryModeVM, authorization);
            return Ok();
        }
        /* [HttpGet]

         [Route("GetFilteredDeliveryModes")]
         public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] DeliveryModeFilter filterRequest, [FromQuery] PagedParameters pagedParameters)
         {
             PaginationData metadata = new PaginationData();
             IList<DeliveryModeVM> vmData =
                 _helper.GetData(_mapper, $"{Microservice.RefData}/DeliveryMode/GetFilteredDeliveryModes?Name={filterRequest.Name}&Status={filterRequest.Status}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

             if (vmData != null && vmData.Count() > 0)
             {
                 var refDataList = PagedList<DeliveryModeVM>.ToGenericPagedList(vmData, pagedParameters);

                 Response.Headers.AddGeneralInformations("X-Pagination", JsonConvert.SerializeObject(metadata));
                 return Ok(refDataList);
             }
             else
             {
                 return NotFound();
             }
         }*/
    }
}