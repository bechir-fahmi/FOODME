using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.FoodMe.AdminAPI.Controllers.LocationData
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<CountryDTO, Country, CountryVM> _helper;

        public CountryController(IMapper mapper, IHelper<CountryDTO, Country, CountryVM> helper)
        {
            _mapper = mapper;
            _helper = helper;
        }

        [HttpGet]
        [Route("GetAllCountries")]

        public IActionResult GetAllCountries([FromHeader] string authorization, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<CountryVM> countryVMList =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Country/GetAllCountries?PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(countryVMList);
        }

        [HttpPost]
        [Route("AddCountry")]
        public IActionResult AddCountry([FromHeader] string authorization, [FromBody] CountryVM countryVM)
        {
            _helper.Create(_mapper, $"{Microservice.RefData}/Country/AddCountry", countryVM, authorization);
            return Created(nameof(AddCountry), countryVM);
        }

        [HttpGet]
        [Route("GetCountryById/{id}")]
        public IActionResult GetCountryById([FromHeader] string authorization, int id)
        {
            var CountryVM =
                _helper.GetObjData(_mapper, $"{Microservice.RefData}/Country/GetCountry/{id}", authorization);

            if (CountryVM == null)
            {
                return NotFound();
            }

            return Ok(CountryVM);
        }

        [HttpPut]
        [Route("UpdateCountry")]
        public IActionResult UpdateCountry([FromHeader] string authorization, [FromBody] CountryVM countryVM)
        {
            _helper.Update(_mapper, $"{Microservice.RefData}/Country/UpdateCountry", countryVM, authorization);
            return Ok();
        }

        [HttpDelete]
        [Route("RemoveCountry")]
        public IActionResult RemoveCountry([FromHeader] string authorization,[FromBody] CountryVM countryVM)
        {
            _helper.Delete(_mapper, $"{Microservice.RefData}/Country/RemoveCountry", countryVM, authorization);
            return Ok();
        }
        [HttpGet]
        [Route("GetFilteredData")]
        public IActionResult GetFilteredData([FromHeader] string authorization, [FromQuery] CountryFillter filterRequest, [FromQuery] PagedParameters pagedParameters)
        {
            PaginationData metadata = new PaginationData();
            IList<CountryVM> vmData =
                _helper.GetData(_mapper, $"{Microservice.RefData}/Country/GetFilteredData?Id={filterRequest.Id}&AreaCode={filterRequest.Code}&CountryKey={filterRequest.CountryKey}&PageNumber={pagedParameters.PageNumber}&PageSize={pagedParameters.PageSize}", authorization, ref metadata);

            if (vmData != null && vmData.Count() > 0)
            {
                var refDataList = PagedList<CountryVM>.ToGenericPagedList(vmData, pagedParameters);

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
