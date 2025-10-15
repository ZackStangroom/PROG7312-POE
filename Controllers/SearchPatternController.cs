using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;
using PROG7312_POE.Services.Interfaces;

namespace PROG7312_POE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchPatternController : ControllerBase
    {
        private readonly ISearchPatternService _searchPatternService;

        public SearchPatternController(ISearchPatternService searchPatternService)
        {
            _searchPatternService = searchPatternService;
        }

        [HttpPost("track")]
        public IActionResult TrackSearch([FromBody] SearchAction searchAction)
        {
            try
            {
                _searchPatternService.TrackSearch(searchAction);
                return Ok(new { success = true, message = "Search tracked successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("analysis")]
        public IActionResult GetAnalysis()
        {
            var analysis = _searchPatternService.AnalyzePatterns();
            return Ok(analysis);
        }

        [HttpGet("recommendations")]
        public IActionResult GetRecommendations([FromQuery] int maxResults = 10)
        {
            var recommendations = _searchPatternService.GetRecommendedEvents(maxResults);
            return Ok(recommendations);
        }

        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery] int limit = 50)
        {
            var history = _searchPatternService.GetSearchHistory(limit);
            return Ok(history);
        }

        [HttpDelete("clear")]
        public IActionResult ClearHistory()
        {
            _searchPatternService.ClearHistory();
            return Ok(new { success = true, message = "History cleared" });
        }
    }
}