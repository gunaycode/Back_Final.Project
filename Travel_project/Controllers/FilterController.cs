using Application.Abstract;
using Application.DTOs.Filter;
using Application.DTOs.SearchDto;
using Microsoft.AspNetCore.Mvc;
using Persistance.Migrations;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController:ControllerBase
    {
        public readonly IFilterResultServices _filterResult;
        public FilterController(IFilterResultServices filterResult)
        {
            _filterResult = filterResult;
        }
        [HttpGet("filter")]
        public async Task<IActionResult>Filter([FromQuery] int rating, int price, bool wifi, string location, bool breakfast, bool pet, bool pool, bool parking )
        {
            try
            {
                List<FilterResult> filterResults = await _filterResult.Filter(rating, price, wifi, location, pet, pool, parking, breakfast);

                if (filterResults.Count > 0)
                {
                    return Ok(filterResults);
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
