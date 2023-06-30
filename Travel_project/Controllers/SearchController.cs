using Application.Abstract;
using Application.DTOs.SearchDto;
using Microsoft.AspNetCore.Mvc;
using Persistance.Concrets;
using Persistance.DataContext;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController:ControllerBase
    {
        public readonly ISearchResultServices _search;
        public SearchController( ISearchResultServices services)
        {
            _search = services;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] int count, string city, DateTime date)
        {
            try
            {
                List<SearchResult> searchResults = await _search.Search(count, city, date);

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
