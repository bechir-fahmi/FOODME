using AutoMapper;
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
    public class QuestionAnswerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<QuestionAnswerDTO, QuestionAnswer, QuestionAnswerVM> _helper;

        public QuestionAnswerController(IMapper mapper, IHelper<QuestionAnswerDTO, QuestionAnswer, QuestionAnswerVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }
        [HttpGet]
        [Route("GetAllQuestion")]
        public IActionResult GetAllQuestion([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<QuestionAnswerVM> QuestionDataVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/QuestionAnswer/GetAllQuestions?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(QuestionDataVMList);
        }

        [HttpPost]
        [Route("AddQuestionAnswer")]
        public IActionResult AddQuestionAnswer([FromHeader] string authorization, [FromBody] QuestionAnswerVM QuestionDataVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/QuestionAnswer/AddQuestionAnswer", QuestionDataVM, authorization);
            return Created(nameof(AddQuestionAnswer), QuestionDataVM);
        }

        [HttpGet]
        [Route("GetQuestionAnswer/{id}")]
        public IActionResult GetQuestionAnswer([FromHeader] string authorization, int id)
        {
            var GetQuestionAnswerVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/QuestionAnswer/GetQuestionAnswer/{id}", authorization);

            if (GetQuestionAnswerVM == null)
            {
                return NotFound();
            }

            return Ok(GetQuestionAnswerVM);
        }

        [HttpPut]
        [Route("UpdateQuestionAnswer")]
        public IActionResult UpdateQuestionAnswer([FromHeader] string authorization, [FromBody] QuestionAnswerVM QuestionDataVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/QuestionAnswer/UpdateQuestionAnswer", QuestionDataVM, authorization);
            return NoContent();
        }

        [HttpDelete]
        [Route("RemoveQuestionAnswer/{Id}")]
        public IActionResult RemoveQuestionAnswer([FromHeader] string authorization, int Id)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/QuestionAnswer/RemoveQuestionAnswer/{Id}", authorization);
            return NoContent();
        }
    }
}
