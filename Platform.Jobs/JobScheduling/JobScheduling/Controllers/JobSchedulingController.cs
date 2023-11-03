using Microsoft.AspNetCore.Mvc;
using Platform.Shared.MicroservicesURLs;

namespace JobScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSchedulingController : ControllerBase
    {

        #region updateBrandActionSummary
        [HttpGet("updatebrandactionsummary")]
        public async Task<IActionResult> updateBrandActionSummary()
        {
            var endPoint = $"{Microservice.FoodMeTracking}/BrandActionSummary/AddOrUpdateBrandActionSummary";
            using (var request = new HttpRequestMessage(HttpMethod.Post, endPoint))
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.PostAsync(endPoint, null);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }
        #endregion

        #region Service Scheduling
        [HttpGet]
        public ActionResult Index()
        {
            /* every day at 2pm*/
            // RecurringJob.AddOrUpdate(() => UpdateSDM(),"0 14 * * *");
            return Ok();
        }
        #endregion

    }
}
