using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.ReferentialData.DtoModel;
using Platform.ReferentialData.DtoModel.BrandData;
using Platform.ReferentialData.DtoModel.BrandData.Integration;
using Platform.ReferentialData.DtoModel.QueryData;
using Platform.Shared.Enum;
using Platform.Shared.SharedClasses.Pagination;
using System.Security.Claims;

namespace Platform.ReferencialData.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;
    private readonly ILogger<VendorController> _logger;
    private readonly IMapper _mapper;

    public VendorController(IVendorService apiDataService, ILogger<VendorController> logger, IMapper mapper)
    {
        _vendorService = apiDataService;
        _logger = logger;
        _mapper = mapper;
    }

    [Route("GetAllVendorData")]
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllVendorData([FromQuery] PagedParameters pagedParameters, VendorType vendorType)
    {
        try
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var vendorList = _vendorService.GetAll(pagedParameters, vendorType, userId);

            PaginationData metadata = new PaginationData
            {
                TotalCount = vendorList.TotalCount,
                PageSize = vendorList.PageSize,
                CurrentPage = vendorList.CurrentPage,
                TotalPages = vendorList.TotalPages,
                HasNext = vendorList.HasNext,
                HasPrevious = vendorList.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(vendorList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllVendorData)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("GetAllActiveVendors")]
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllActiveVendors([FromQuery] PagedParameters pagedParameters, VendorType vendorType)
    {
        try
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var vendorList = _vendorService.GetAllActiveData(pagedParameters, vendorType, userId);

            PaginationData metadata = new PaginationData
            {
                TotalCount = vendorList.TotalCount,
                PageSize = vendorList.PageSize,
                CurrentPage = vendorList.CurrentPage,
                TotalPages = vendorList.TotalPages,
                HasNext = vendorList.HasNext,
                HasPrevious = vendorList.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(vendorList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllActiveVendors)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("GetVendorByTag/{tag}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetVendorByTag(string tag, [FromQuery] PagedParameters pagedParameters, VendorType vendorType)
    {
        try
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var vendorListByTag = _vendorService.Get(tag, pagedParameters, vendorType);

            PaginationData metadata = new PaginationData
            {
                TotalCount = vendorListByTag.TotalCount,
                PageSize = vendorListByTag.PageSize,
                CurrentPage = vendorListByTag.CurrentPage,
                TotalPages = vendorListByTag.TotalPages,
                HasNext = vendorListByTag.HasNext,
                HasPrevious = vendorListByTag.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(vendorListByTag);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetVendorByTag)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("AddGeneralInformations")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddGeneralInformation([FromBody] VendorGeneralInformationDTO vendorGeneralInformation)
    {
        try
        {
            var vendor = _vendorService.AddGeneralInformations(vendorGeneralInformation);
            return Created(nameof(AddGeneralInformation), vendor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(AddGeneralInformation)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("UpdateGeneralInformation")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Update")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateGeneralInformation([FromBody] VendorGeneralInformationDTO vendorGeneralInformation)
    {
        try
        {
            var vendor = _vendorService.UpdateGeneralInformations(vendorGeneralInformation);
            return Created(nameof(UpdateGeneralInformation), vendor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateGeneralInformation)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("AddDeliveryZonesToVendor")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddDeliveryZonesToVendor([FromBody] List<VendorDeliveryZoneDTO> vendorDeliveryZones, Guid vendorId, VendorType vendorType)
    {
        try
        {
            _vendorService.Add(vendorDeliveryZones, vendorId, vendorType);
            return Created(nameof(AddDeliveryZonesToVendor), vendorDeliveryZones);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(AddDeliveryZonesToVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("UpdateDeliveryZones")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Update")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateDeliveryZones([FromBody] List<VendorDeliveryZoneDTO> vendorDeliveryZones, Guid vendorId, VendorType vendorType)
    {
        try
        {
            _vendorService.Update(vendorDeliveryZones, vendorId, vendorType);
            return Created(nameof(UpdateDeliveryZones), vendorDeliveryZones);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateDeliveryZones)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("AddDataToVendor")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddDataToVendor([FromBody] VendorDataDTO vendorData, Guid vendorId, VendorType vendorType)
    {
        try
        {
            _vendorService.Add(vendorData, vendorId, vendorType);
            return Created(nameof(AddDataToVendor), vendorData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(AddDataToVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("UpdateVendorData")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Update")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateVendorData([FromBody] VendorDataDTO vendorData, Guid vendorId)
    {
        try
        {
            _vendorService.Update(vendorData, vendorId);
            return Created(nameof(UpdateVendorData), vendorData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateVendorData)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("AddStaticIntegrationToVendor")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddStaticIntegrationToVendor([FromBody] List<StaticIntegrationDTO> vendorStaticIntegration, Guid vendorId, VendorType vendorType)
    {
        try
        {
            _vendorService.AddStaticIntegration(vendorStaticIntegration, vendorId, vendorType);
            return Created(nameof(AddStaticIntegrationToVendor), vendorStaticIntegration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(AddStaticIntegrationToVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("UpdateStaticIntegration")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Update")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateStaticIntegration([FromBody] StaticIntegrationDTO vendorStaticIntegration, Guid vendorId, VendorType vendorType)
    {
        try
        {
            _vendorService.UpdateStaticIntegration(vendorStaticIntegration, vendorId, vendorType);
            return Created(nameof(UpdateStaticIntegration), vendorStaticIntegration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateStaticIntegration)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("AddDynamicIntegrationToVendor")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddDynamicIntegrationToVendor([FromBody] DynamicIntegrationDTO vendorDynamicIntegration, Guid vendorId, VendorType vendorType, [FromQuery] QueryDTO queryDTO)
    {
        try
        {
            _vendorService.AddDynamicIntegration(vendorDynamicIntegration, vendorId, vendorType, queryDTO);
            return Created(nameof(AddDynamicIntegrationToVendor), vendorDynamicIntegration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(AddDynamicIntegrationToVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("UpdateDynamicIntegration")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Update")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateDynamicIntegration([FromBody] DynamicIntegrationDTO vendorDynamicIntegration, Guid vendorId)
    {
        try
        {
            _vendorService.UpdateDynamicIntegration(vendorDynamicIntegration, vendorId);
            return Created(nameof(UpdateDynamicIntegration), vendorDynamicIntegration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateDynamicIntegration)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("GetVendorByName/{name}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetVendorByName(string name)
    {
        try
        {
            VendorDTO vendorDto = _vendorService.GetVendorByName(name);
            return Ok(vendorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetVendorByName)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }

    }
    [Route("GetVendor/{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetVendor(Guid id, VendorType vendorType)
    {
        try
        {
            VendorDTO vendorDto = _vendorService.Get(id, vendorType);
            return Ok(vendorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }

    }
    [Route("GetGeneralInformation/{id}")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.View")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetGeneralInformations(Guid id, VendorType apiType)
    {
        try
        {
            VendorGeneralInformationDTO apiDto = _vendorService.GetGeneralInformations(id, apiType);
            return Ok(apiDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGeneralInformations)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }

    }

    [Route("GetVendorData/{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetVendorData(Guid id, VendorType vendorType)
    {
        try
        {
            VendorDataDTO vendorDto = _vendorService.GetVendorData(id, vendorType);
            return Ok(vendorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetVendorData)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }
    [Route("GetVendorDeliveryZones/{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetVendorDeliveryZones(Guid id, VendorType vendorType)
    {
        try
        {
            List<VendorDeliveryZoneDTO> VendorZoneDto = _vendorService.GetVendorDeliveryZones(id, vendorType);
            return Ok(VendorZoneDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetVendorDeliveryZones)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("GetStaticIntegration/{id}")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.View")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetStaticIntegration(Guid id)
    {
        try
        {
            StaticIntegrationDTO staticIntegrationDTO = _vendorService.GetStaticIntegration(id);
            return Ok(staticIntegrationDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetStaticIntegration)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }
    [Route("GetDynamicIntegration/{id}")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.View")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetDynamicIntegration(Guid id, VendorType vendorType)
    {
        try
        {
            DynamicIntegrationEntity dynamicIntegrationDTO = _vendorService.GetDynamicIntegration(id);
            return Ok(dynamicIntegrationDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetStaticIntegration)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("GetDynamicIntegrationsByVendor/{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetDynamicIntegrationsByVendor(Guid id, VendorType vendorType)
    {
        try
        {
            List<DynamicIntegrationDTO> dynamicInetgrations = _vendorService.GetDynamicIntegrationsByVendor(id, vendorType);
            return Ok(dynamicInetgrations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetDynamicIntegrationsByVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [HttpDelete]
    [AllowAnonymous]
    [Route("RemoveVendor/{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult RemoveVendor(Guid Id, VendorType vendorType)
    {
        try
        {
            _vendorService.Remove(Id, vendorType);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("AddVendor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddVendor(VendorDTO vendorDto)
    {
        try
        {
            _vendorService.Add(vendorDto);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(AddVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }
    [HttpPut]
    [AllowAnonymous]
    [Route("UpdateVendor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateVendor(VendorDTO vendorDto, VendorType vendorType)
    {
        try
        {
            _vendorService.Update(vendorDto, vendorType);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateVendor)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.ViewAll")]
    [Route("getDeals")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeals([FromQuery] AddressToSearchDTO addressToSearch)
    {
        try
        {
            int languageKey = 1;
            //if (HttpContext.Request.Headers.TryGetValue("X-localization", out var headerValue))
            //    languageKey = int.Parse(headerValue);
            List<DealsDTO> deals = await _vendorService.GetDeals(addressToSearch, languageKey);
            return Ok(deals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetDeals)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }

    }

    [HttpGet]
    [AllowAnonymous]
    [Route("GetPagedDeals")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetPagedDeals([FromQuery] AddressToSearchDTO addressToSearch, [FromQuery] PagedParameters pagedParameters)
    {
        try
        {
            //HttpContext.Request.Headers.TryGetValue("X-localization", out var headerValue);
            //int languageKey = int.Parse(headerValue);
            int languageKey = 1;
            var allDeals = _vendorService.GetPagedDeals(pagedParameters, addressToSearch, languageKey);
            PaginationData metadata = new PaginationData
            {
                TotalCount = allDeals.TotalCount,
                PageSize = allDeals.PageSize,
                CurrentPage = allDeals.CurrentPage,
                TotalPages = allDeals.TotalPages,
                HasNext = allDeals.HasNext,
                HasPrevious = allDeals.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


            return Ok(allDeals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetPagedDeals)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");

        }

    }

    [HttpGet]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.ViewAll")]
    [Route("GetPagedFilteredDeals")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetFilteredData([FromQuery] VendorFilter filter, [FromQuery] PagedParameters pagedParameters)
    {
        try
        {
            //HttpContext.Request.Headers.TryGetValue("X-localization", out var headerValue);
            //int languageKey = int.Parse(headerValue);
            int languageKey = 1;
            var allDeals = _vendorService.GetPagedFilteredDeals(pagedParameters, filter, languageKey);
            PaginationData metadata = new PaginationData
            {
                TotalCount = allDeals.TotalCount,
                PageSize = allDeals.PageSize,
                CurrentPage = allDeals.CurrentPage,
                TotalPages = allDeals.TotalPages,
                HasNext = allDeals.HasNext,
                HasPrevious = allDeals.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(allDeals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetFilteredData)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [HttpGet]
    [Route("VendorHaveDynamicInetgration")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult VendorHaveDynamicInetgration(Guid vendorId, VendorType vendorType)
    {
        try
        {
            bool hasDynamicIntegration = _vendorService.VendorHaveDynamicInetgration(vendorId, vendorType);
            var response = new { value = hasDynamicIntegration };
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(VendorHaveDynamicInetgration)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("MatchBrands")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult MatchBrands(List<BrandMatchingDTO> matching)
    {
        try
        {
            _vendorService.MatchBrands(matching);
            return Created(nameof(MatchBrands), null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(MatchBrands)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }

    [Route("GetDistantBrandsByAggregator")]
    [AllowAnonymous]
    //[Authorize("Permissions.Brand.Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDistantBrandsByAggregator([FromBody] DynamicIntegrationDTO dynamicIntegration)
    {
        try
        {
            List<DealsDTO> deals = await _vendorService.GetBrandsToMatch(dynamicIntegration);
            return Created(nameof(GetDistantBrandsByAggregator), deals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetDistantBrandsByAggregator)}");
            return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
        }
    }
}
