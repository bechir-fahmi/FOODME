using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services;
using Platform.ReferentialData.DtoModel;
using Platform.ReferentialData.DtoModel.BrandData.Recommandation;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.Shared.SharedClasses.Pagination;
using Platform.Tracking.DtoModel.BrandAction;
using System.Security.Claims;

namespace Platform.ReferencialData.WebAPI.Controllers.BrandActionData;

[Route("api/[controller]")]
[ApiController]
public class BrandActionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IHelper<BrandActionSummaryDTO, BrandActionSummaryDTO, BrandActionSummaryDTO> _helper;
    private readonly ILogger<VendorController> _logger;
    private readonly IVendorService _brandService;

    public BrandActionController(IMapper mapper,
        IHelper<BrandActionSummaryDTO, BrandActionSummaryDTO, BrandActionSummaryDTO> helper, IWebHostEnvironment env,
        ILogger<VendorController> logger,
        IVendorService apiService)
    {
        _mapper = mapper;
        _helper = helper;
        _logger = logger;
        _brandService= apiService;
    }

    [HttpGet]
    [Route("GetAllBrandRecommandation")]
    public IActionResult GetAllBrandRecommandation([FromHeader] string authorization, [FromQuery] AddressToSearchDTO addressToSearchDTO)
    {
        try
        {
            //HttpContext.Request.Headers.TryGetValue("X-localization", out var headerValue);
            //int languageKey = int.Parse(headerValue);
            int languageKey = 1;
            var userId = HttpContext.User.FindFirstValue("Id");
            _logger.LogInformation($"{Microservice.FoodMeTracking}/BrandActionSummary/getAllBrandActionSummary");
            IList <BrandActionSummaryDTO> BrandActionDTOList =
            _helper.GetData(_mapper, $"{Microservice.FoodMeTracking}/BrandActionSummary/getAllBrandActionSummary", authorization);
            if(BrandActionDTOList.Count == 0)
            {
                return BadRequest("error eccoured while getting BrandActionSummary");
            }
            var BrandActionDTOListFiltredByUser = BrandActionDTOList.Where(user=> user.UserId.ToString() == userId).ToList();
            List<RecommandationDTO> brandRecommendationList= _brandService.GetBrandRecommandations(BrandActionDTOListFiltredByUser, addressToSearchDTO, languageKey);

            
            return Ok(brandRecommendationList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllBrandRecommandation)}");
            return StatusCode(500, $"Internal server Error: {ex}");
        }

    }
             
    }