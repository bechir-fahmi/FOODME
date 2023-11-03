using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.SupportServiceData;
using Platform.ReferentialData.DtoModel.SupportService;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.SupportServiceData
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswerController : ControllerBase
    {
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly ILogger<QuestionAnswerController> _logger;
        private readonly IMapper _mapper;

        public QuestionAnswerController(IQuestionAnswerService questionAnswerService, ILogger<QuestionAnswerController> logger, IMapper mapper)
        {
            _questionAnswerService = questionAnswerService;
            _logger = logger;
            _mapper = mapper;
        }
        [Route("GetAllQuestions")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllQuestions([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var QuestionList = _questionAnswerService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = QuestionList.TotalCount,
                    PageSize = QuestionList.PageSize,
                    CurrentPage = QuestionList.CurrentPage,
                    TotalPages = QuestionList.TotalPages,
                    HasNext = QuestionList.HasNext,
                    HasPrevious = QuestionList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(QuestionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllQuestions)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }

        [Route("AddQuestionAnswer")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddQuestionAnswer([FromBody] QuestionAnswerDTO QuestionAnswer)
        {
            try
            {
                _questionAnswerService.Add(QuestionAnswer);
                return Created(nameof(AddQuestionAnswer), QuestionAnswer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddQuestionAnswer)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
        [Route("GetQuestionAnswer/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetQuestionAnswer(int id)
        {
            try
            {
                QuestionAnswerDTO QuestionAnswerDTO = _questionAnswerService.Get(id);
                return Ok(QuestionAnswerDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetQuestionAnswer)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }

        }


        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveQuestionAnswer/{VendorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveQuestionAnswer(int Id)
        {
            try
            {
                _questionAnswerService.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveQuestionAnswer)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateQuestionAnswer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateQuestionAnswer(QuestionAnswerDTO QuestionAnswer)
        {
            try
            {
                _questionAnswerService.Update(QuestionAnswer);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateQuestionAnswer)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
