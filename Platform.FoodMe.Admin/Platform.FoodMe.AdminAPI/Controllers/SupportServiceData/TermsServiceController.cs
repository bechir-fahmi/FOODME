using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.SupportServiceData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.ReferentialData.DtoModel.SupportService;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.SupportServiceData
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsServiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<TermsServiceDTO, TermsService, TermsServiceVM> _helper;

        public TermsServiceController(IMapper mapper, IHelper<TermsServiceDTO, TermsService, TermsServiceVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllTermsService")]
        public IActionResult GetAllTermsService([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<TermsServiceVM> TermDataVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/TermsService/GetAllTermsService?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(TermDataVMList);
        }

        [HttpPost]
        [Route("AddTermsService")]
        public IActionResult AddTermsService([FromHeader] string authorization, [FromBody] TermsServiceVM TermsServiceVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/TermsService/AddTermsService", TermsServiceVM, authorization);
            return Created(nameof(AddTermsService), TermsServiceVM);
        }
        [HttpGet]
        [Route("GetTermsService/{id}")]
        public IActionResult GetTermsService([FromHeader] string authorization, int id)
        {
            var GetTermsServiceVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/TermsService/GetTermsService/{id}", authorization);

            if (GetTermsServiceVM == null)
            {
                return NotFound();
            }

            return Ok(GetTermsServiceVM);
        }


        [HttpPut]
        [Route("UpdateTermsService")]
        public IActionResult UpdateTermsService([FromHeader] string authorization, [FromBody] TermsServiceVM TermsServiceVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/TermsService/UpdateTermsService", TermsServiceVM, authorization);
            return NoContent();
        }

        [HttpDelete]
        [Route("RemoveTermsService")]
        public IActionResult RemoveTermsService([FromHeader] string authorization, [FromBody] TermsServiceVM TermsServiceVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/TermsService/RemoveTermsService", TermsServiceVM, authorization);
            return NoContent();
        }
    }
}
