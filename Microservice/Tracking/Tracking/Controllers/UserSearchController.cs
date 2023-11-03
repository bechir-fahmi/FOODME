using Microsoft.AspNetCore.Mvc;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.DtoModel.UserSearch;

namespace Platform.Tracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSearchController : Controller
    {
        private readonly IUserSearchService _userSearchService;

        public UserSearchController(IUserSearchService userSearchService)
        {
            _userSearchService = userSearchService;
        }

        [HttpPost("TrackUserSearch")]
        public IActionResult TrackUserSearch(UserSearchDTO userSearchDto, bool hasResults)
        {
            try
            {
                _userSearchService.TrackUserSearch(userSearchDto, hasResults);
                return Ok("User search tracked successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAllUserSearches")]
        public IActionResult GetAllUserSearches()
        {
            try
            {
                List<UserSearchDTO> userSearches = _userSearchService.GetAllUserSearches();
                return Ok(userSearches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
