using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.MealData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.MealData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.MealData
{
    public class MealTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<MealTypeDTO, MealType, MealTypeVM> _helper;
        private readonly IWebHostEnvironment _env;


        public MealTypeController(IMapper mapper, IHelper<MealTypeDTO, MealType, MealTypeVM> helper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _helper = helper;
            _env = env;
        }
        [HttpGet]
        [Route("GetAllMealTypes")]
        public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<MealTypeVM> mealTypeVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/MealType/GetAllMealTypes?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(mealTypeVMList);
        }    
        [HttpGet]
        [Route("GetMealTypesByTag")]
        public IActionResult GetMealTypesByTag([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<MealTypeVM> mealTypeVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/MealType/GetMealTypesByTag?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(mealTypeVMList);
        }
        [HttpGet]
        [Route("GetMealType/{id}")]
        public IActionResult Get([FromHeader] string authorization, int id)
        {
           MealTypeVM mealTypeVM =
                _helper.Get(_mapper, $"{Microservice.RefData}/MealType/GetMealType/{id}", authorization);

            return Ok(mealTypeVM);
        }

        [HttpPost]
        [Route("AddMealType")]
        public IActionResult Add([FromHeader] string authorization, [FromBody] MealTypeVM mealTypeVM)
        {
            //MealTypeService.UploadMealTypeImages(mealTypeVM, _env.WebRootPath);
            _helper.Create(_mapper, $"{Microservice.RefData}/MealType/AddMealType", mealTypeVM, authorization);
            return Created(nameof(Add), mealTypeVM);
        }

        [HttpPut]
        [Route("UpdateMealType")]
        public IActionResult Update([FromHeader] string authorization, [FromBody] MealTypeVM mealTypeVM)
        {
            //MealTypeService.UploadMealTypeImages(mealTypeVM, _env.WebRootPath);
            _helper.Update(_mapper, $"{Microservice.RefData}/MealType/UpdateMealType", mealTypeVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveMealType")]
        public IActionResult Remove([FromHeader] string authorization,[FromBody] MealTypeVM mealTypeVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/MealType/RemoveMealType", mealTypeVM, authorization);
            return Ok();
        }
        [HttpGet]

        [Route("GetFilteredMealTypes")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] KitchenTypeFillter filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<MealTypeVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/MealType/GetFilteredMealTypes?Name={filterRequest.Name}&Status={filterRequest.Status}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<MealTypeVM>.ToGenericPagedList(vmData, pagedParameters);

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

