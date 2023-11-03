using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.GenericRepository;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.LanguageData
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageResourceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<LanguageResourceDTO, LanguageResource, LanguageResourceVM> _helper;

        public LanguageResourceController(IMapper mapper, IHelper<LanguageResourceDTO, LanguageResource, LanguageResourceVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllLanguageResource")]
        public IActionResult GetAllLanguageResource([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<LanguageResourceVM> languageResourceVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/LanguageResource/GetAllLanguageResource?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(languageResourceVMList);
        }


        [HttpGet("GetLanguageResourcesByCountryCode/{countryCode}")]
        public IActionResult GetLanguageResourcesByCountryCode(Guid countryCode, [FromHeader] string authorization)
        {



            IList<LanguageResourceVM> languageResourceVMList =
               _helper.GetData(_mapper, $"{Microservice.RefData}/LanguageResource/GetLanguageResourcesByCountryCode/{countryCode}", authorization);

            return Ok(languageResourceVMList);

        }

        [HttpPost]
        [Route("AddLanguageResource")]
        public IActionResult AddLanguageResource([FromBody] LanguageResourceVM languageResourceVM, [FromHeader] string authorization)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/LanguageResource/AddLanguageResource", languageResourceVM, authorization);
            return Created(nameof(AddLanguageResource), languageResourceVM);
        }

        [HttpGet]
        [Route("GetLanguageById/{id}")]
        public IActionResult GetLanguageResourceById([FromHeader] string authorization, int id)
        {
            var LanguageResorceVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/LanguageResource/GetLanguageResource/{id}", authorization);

            if (LanguageResorceVM == null)
            {
                return NotFound();
            }

            return Ok(LanguageResorceVM);
        }

        [HttpPut]
        [Route("UpdateLanguageResource")]
        public IActionResult UpdateLanguageResource([FromBody] LanguageResourceVM languageResourceVM, [FromHeader] string authorization)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/LanguageResource/UpdateLanguageResource", languageResourceVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveLanguageResource/{languageResourceId}")]
        public IActionResult RemoveLanguageResource(int LanguageResourceId, [FromHeader] string authorization)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/LanguageResource/RemoveLanguageResource/{LanguageResourceId}", authorization);
            return Ok();
        }
    }
}
