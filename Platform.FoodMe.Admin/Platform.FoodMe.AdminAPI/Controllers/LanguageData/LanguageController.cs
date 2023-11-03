using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe
    .Admin.ViewModels.ViewModels.LanguageData;
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
    public class LanguageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<LanguageDTO, Language, LanguageVM> _helper;

        public LanguageController(IMapper mapper, IHelper<LanguageDTO, Language, LanguageVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }



        [HttpGet]
        [Route("GetAllLanguages")]
        public IActionResult GetAllLanguage([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<LanguageVM> languageVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Language/GetAllLanguages?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(languageVMList);
        }

        [HttpPost]
        [Route("AddLanguage")]
        public IActionResult AddLanguage([FromBody] LanguageVM languageVM, [FromHeader] string authorization)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/Language/AddLanguage", languageVM, authorization);
            return Created(nameof(AddLanguage), languageVM);
        }

        [HttpGet]
        [Route("GetLanguageById/{id}")]
        public IActionResult GetLanguageById([FromHeader] string authorization, int id)
        {
            var LanguageVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/Language/GetLanguage/{id}", authorization);

            if (LanguageVM == null)
            {
                return NotFound();
            }

            return Ok(LanguageVM);
        }

        [HttpPut]
        [Route("UpdateLanguage")]
        public IActionResult UpdateLanguage([FromBody] LanguageVM languageVM, [FromHeader] string authorization)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/Language/UpdateLanguage", languageVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("Removelanguage")]
        public IActionResult RemoveLanguage([FromBody] LanguageVM languageVM, [FromHeader] string authorization)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/Language/RemoveLanguage", languageVM, authorization);
            return Ok();
        }
    }
}
