using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.FoodTypeData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.FoodTypeData
{
    public class FoodTypeController : ControllerBase
    {
            private readonly IMapper _mapper;
            private readonly IHelper<FoodTypeDTO, FoodType, FoodTypeVM> _helper;
            private readonly IWebHostEnvironment _env;


            public FoodTypeController(IMapper mapper, IHelper<FoodTypeDTO, FoodType, FoodTypeVM> helper, IWebHostEnvironment env)
            {
                _mapper = mapper;
                _helper = helper;
                _env = env;
            }
            [HttpGet]
            [Route("GetAllFoodTypes")]
            public IActionResult GetAll([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
            {
                PaginationData metadata = new PaginationData();
                IList<FoodTypeVM> foodVMList =
                    _helper.GetData(_mapper, $"{Microservice.RefData}/FoodType/GetAllFoodTypes?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(foodVMList);
            }            [HttpGet]
            [Route("GetFoodTypesByTag")]
            public IActionResult GetFoodTypesByTag([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
            {
                PaginationData metadata = new PaginationData();
                IList<FoodTypeVM> foodVMList =
                    _helper.GetData(_mapper, $"{Microservice.RefData}/FoodType/GetFoodTypesByTag?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(foodVMList);
            }
            [HttpGet]
            [Route("GetFoodType/{id}")]
            public IActionResult Get([FromHeader] string authorization, int id)
            {
                FoodTypeVM foodTypeVM =
                    _helper.Get(_mapper, $"{Microservice.RefData}/FoodType/GetFoodType/{id}", authorization);

                return Ok(foodTypeVM);
            }

            [HttpPost]
            [Route("AddFoodType")]
            public IActionResult Add([FromHeader] string authorization, [FromBody] FoodTypeVM foodVM)
            {
                //FoodTypeService.UploadFoodTypeImages(foodVM, _env.WebRootPath);
                _helper.Create(_mapper, $"{Microservice.RefData}/FoodType/AddFoodType", foodVM, authorization);
                return Created(nameof(Add), foodVM);
            }

            [HttpPut]
            [Route("UpdateFoodType")]
            public IActionResult Update([FromHeader] string authorization, [FromBody] FoodTypeVM foodTypeVM)
            {
                //FoodTypeService.UploadFoodTypeImages(foodTypeVM, _env.WebRootPath);
                _helper.Update(_mapper, $"{Microservice.RefData}/FoodType/UpdateFoodType", foodTypeVM, authorization);
                return Ok();
            }

            [HttpDelete]
            [Route("RemoveFoodType")]
            public IActionResult Remove([FromHeader] string authorization, [FromBody] FoodTypeVM foodTypeVM)
            {
                _helper.Delete(_mapper, $"{Microservice.RefData}/FoodType/RemoveFoodType", foodTypeVM, authorization);
                return Ok();
            }
            [HttpGet]

            [Route("GetFilteredFoodTypes")]
            public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] FoodTypeFilterDTO filterRequest, [FromQuery] PagedParameters pagedParameters)
            {
                PaginationData metadata = new PaginationData();
                IList<FoodTypeVM> vmData =
                    _helper.GetData(_mapper, $"{Microservice.RefData}/FoodType/GetFilteredFoodTypes?Name={filterRequest.Name}&Status={filterRequest.Status}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

                if (vmData != null && vmData.Count() > 0)
                {
                    var refDataList = PagedList<FoodTypeVM>.ToGenericPagedList(vmData, pagedParameters);

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
