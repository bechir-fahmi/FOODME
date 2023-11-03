using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.MealData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferentialData.DtoModel.MealData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.MealData
{
    public class MealTimingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<MealTimingDTO, MealTiming, MealTimingVM> _helper;
        private readonly IWebHostEnvironment _env;


        public MealTimingController(IMapper mapper, IHelper<MealTimingDTO, MealTiming, MealTimingVM> helper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _helper = helper;
            _env = env;
        }
        [HttpGet]
        [Route("GetAllMealTiming")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<MealTimingVM> mealTimingVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/MealTiming/GetAllMealTiming?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(mealTimingVMList);
        } 
        [HttpGet]
        [Route("GetMealTimingByTag")]
        public IActionResult GetMealTimingByTag([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<MealTimingVM> mealTimingVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/MealTiming/GetMealTimingByTag?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(mealTimingVMList);
        }
        [HttpGet]
        [Route("GetMealTiming/{id}")]
        public IActionResult Get([FromHeader] string authorization, int id)
        {
            MealTimingVM mealTimingVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/MealTiming/GetMealTiming/{id}", authorization);

            return Ok(mealTimingVM);
        }

        [HttpPost]
        [Route("AddMealTiming")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] MealTimingVM mealTimingVM)
        {
            //MealTimingService.UploadMealTimingImages(mealTimingVM, _env.WebRootPath);
            _helper.Create(_mapper, $"{Microservice.RefData}/MealTiming/AddMealTiming", mealTimingVM, authorization);
            return Created(nameof(Add), mealTimingVM);
        }

        [HttpPut]
        [Route("UpdateMealTiming")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] MealTimingVM mealTimingVM)
        {
            //MealTimingService.UploadMealTimingImages(mealTimingVM, _env.WebRootPath);
            _helper.Update(_mapper, $"{Microservice.RefData}/MealTiming/UpdateMealTiming", mealTimingVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveMealTiming")]
        public IActionResult Remove([FromHeader] string authorization, [FromBody] MealTimingVM mealTimingVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/MealTiming/RemoveMealTiming", mealTimingVM, authorization);
            return Ok();
        }
        [HttpGet]

        [Route("GetFilteredMealTiming")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] MealTimingFilterDTO filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<MealTimingVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/MealTiming/GetFilteredMealTiming?Name={filterRequest.Name}&Status={filterRequest.Status}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<MealTimingVM>.ToGenericPagedList(vmData, pagedParameters);

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
