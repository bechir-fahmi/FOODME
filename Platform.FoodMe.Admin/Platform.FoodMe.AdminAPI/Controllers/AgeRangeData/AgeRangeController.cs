using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.AgeRangeData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.AgeRangeData;
using Platform.ReferentialData.DtoModel.AgeRangeData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.AgeRangeData
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeRangeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<AgeRangeDTO, AgeRange, AgeRangeVM> _helper;
        private readonly IWebHostEnvironment _env;


        public AgeRangeController(IMapper mapper, IHelper<AgeRangeDTO, AgeRange, AgeRangeVM> helper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _helper = helper;
            _env = env;
        }
        [HttpGet]
        [Route("GetAllAgeRanges")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<AgeRangeVM> ageRangeVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/AgeRange/GetAllAgeRanges?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(ageRangeVMList);
        }
        [HttpGet]
        [Route("GetAgeRange/{id}")]
        public IActionResult Get([FromHeader] string authorization, int id)
        {
            AgeRangeVM ageRangeVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/AgeRange/GetAgeRange/{id}", authorization);

            return Ok(ageRangeVM);
        }

        [HttpPost]
        [Route("AddAgeRange")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] AgeRangeVM ageRangeVM)
        {
            
            _helper.Create(_mapper, $"{Microservice.RefData}/AgeRange/AddAgeRange", ageRangeVM, authorization);
            return Created(nameof(Add), ageRangeVM);
        }

        [HttpPut]
        [Route("UpdateAgeRange")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] AgeRangeVM ageRangeVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/AgeRange/UpdateAgeRange", ageRangeVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveAgeRange")]
        public IActionResult Remove([FromHeader] string authorization, [FromBody] AgeRangeVM ageRangeVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/AgeRange/RemoveAgeRange", ageRangeVM, authorization);
            return Ok();
        }
       /* [HttpGet]

        [Route("GetFilteredAgeRanges")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] AgeRangeFilter filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<AgeRangeVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/AgeRange/GetFilteredAgeRanges?Name={filterRequest.Name}&Status={filterRequest.Status}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<AgeRangeVM>.ToGenericPagedList(vmData, pagedParameters);

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