using Application.Abstract;
using Application.DTOs.SearchDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public readonly ISearchResultServices _search;
        public SearchController(ISearchResultServices search)
        {
            _search = search;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] int? count, [FromQuery] int? cityId, [FromQuery] DateTime? date)
        {
                try
                {
                    List<Hotel> searchResults = await _search.Search(count, cityId, date);

                    if (searchResults.Count > 0)
                    {
                        return Ok(searchResults);
                    }
                    else
                    {
                        return NotFound("No search results found.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
        }
        
    }
}
