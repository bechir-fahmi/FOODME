using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.DeliveryModeData;
using Platform.ReferencialData.Business.business_services_implementations.DeliveryModeData;
using Platform.Shared.SharedClasses.Pagination;

namespace Platform.ReferencialData.WebAPI.Controllers.Config
{

    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IWebHostEnvironment _env;

        public ConfigController(ILogger<ConfigController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        [Route("Api/Config")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetConfig()
        {
            try
            {
                string filePath =  "./config.json";

                if (!System.IO.File.Exists(filePath))
                {
                    _logger.LogError($"Config file not found at path: {filePath}");
                    return NotFound(); // Return a 404 response if the file does not exist
                }

                string jsonData = System.IO.File.ReadAllText(filePath);
                var config = JsonConvert.DeserializeObject<dynamic>(jsonData);
                return Ok(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetConfig)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }
    }
}

       