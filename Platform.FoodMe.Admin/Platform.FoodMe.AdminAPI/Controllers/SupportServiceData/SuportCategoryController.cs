using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.Business.SuportCategory;
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
    public class SuportCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<SuportCategoryDTO, SuportCategory, SuportCategoryVM> _helper;
        private readonly IWebHostEnvironment _env;


        public SuportCategoryController(IMapper mapper, IHelper<SuportCategoryDTO, SuportCategory, SuportCategoryVM> helper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _helper = helper;
            _env = env;
        }

        [HttpGet]
        [Route("GetAllSuportCategory")]
        public IActionResult GetAllSuportCategory([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<SuportCategoryVM> SuportDataVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/SuportCategory/GetAllSuportCategory?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(SuportDataVMList);
        }

        [HttpPost]
        [Route("AddSuportCategory")]
        public IActionResult AddSuportCategory([FromHeader] string authorization, [FromBody] SuportCategoryVM SuportCategoryDataVM)
        {
            SuportCategoryService.UploadSuportCategoryImages(SuportCategoryDataVM, _env.WebRootPath);
            _helper.Create(_mapper, $"{Microservice.RefData}/SuportCategory/AddSuportCategory", SuportCategoryDataVM, authorization);
            return Created(nameof(AddSuportCategory), SuportCategoryDataVM);
        }

        [HttpGet]
        [Route("GetSuportCategory/{id}")]
        public IActionResult GetSuportCategory([FromHeader] string authorization, int id)
        {
            var GetSuportCategoryVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/SuportCategory/GetSuportCategory/{id}", authorization);

            if (GetSuportCategoryVM == null)
            {
                return NotFound();
            }

            return Ok(GetSuportCategoryVM);
        }



        [HttpPut]
        [Route("UpdateSuportCategory")]
        public IActionResult UpdateSuportCategory([FromHeader] string authorization, [FromBody] SuportCategoryVM SuportCategoryDataVM)
        {
            SuportCategoryService.UploadSuportCategoryImages(SuportCategoryDataVM, _env.WebRootPath);
            _helper.Update(_mapper, $"{Microservice.RefData}/SuportCategory/UpdateSuportCategory", SuportCategoryDataVM, authorization);
            return NoContent();
        }

        [HttpDelete]
        [Route("RemoveSuportCategory")]
        public IActionResult RemoveSuportCategory([FromHeader] string authorization, [FromBody] SuportCategoryVM SuportCategoryDataVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/SuportCategory/RemoveSuportCategory", SuportCategoryDataVM,authorization);
            return NoContent();
        }
    }
}
