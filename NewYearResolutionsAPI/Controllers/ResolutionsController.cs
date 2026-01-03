using Microsoft.AspNetCore.Mvc;

namespace NewYearResolutionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResolutionsController(ILogger<ResolutionsController> logger) : ControllerBase
    {
        public static List<Resolution> resolutions = [];

        private readonly ILogger<ResolutionsController> _logger = logger;

        // GET METHODS
        [HttpGet]
        public IActionResult GetAll([FromQuery] bool? isDone, [FromQuery] string? title)
        {
            var list = resolutions;
            if (isDone != null && title == null)
            {
                list = [.. resolutions.Where(x => x.IsDone == Convert.ToBoolean(isDone))];
            }
            else if (isDone == null && title != null)
            {
                list = [.. resolutions.Where(x => x.Title!.Contains(title, StringComparison.CurrentCultureIgnoreCase))];
            }
            else if (isDone != null && title != null)
            {
                list = [.. resolutions.Where(x => x.IsDone == Convert.ToBoolean(isDone) && x.Title!.Contains(title, StringComparison.CurrentCultureIgnoreCase))];
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var res = resolutions.Find(x => x.Id == id);
            return Ok(res);
        }

        // POST METHODS
        [HttpPost]
        public IActionResult Create([FromBody] CreateResolution createResolution)
        {
            var res = new Resolution()
            {
                Id = resolutions.Count + 1,
                Title = createResolution.Title,
                IsDone = createResolution.IsDone,
                CreatedAt = DateTime.Now
            };
            resolutions.Add(res);
            return Ok(res);
        }

        // PUT METHOD
        [HttpPut("{id}")]
        public IActionResult UpdateById([FromRoute] int id, [FromBody] CreateResolution updateResolution)
        {
            var resolution = resolutions.Find(x => x.Id == id);
            resolution!.Title = updateResolution.Title;
            resolution!.IsDone = updateResolution.IsDone;
            return Ok(resolution);
        }

        // DELETE METHOD
        [HttpDelete("{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            var resolutionId = resolutions.FindIndex(x => x.Id == id);
            resolutions.RemoveAt(resolutionId);
            return NoContent();
        }
    }

    public class CreateResolution
    {
        public string? Title { get; set; }
        public bool IsDone { get; set; }
    }
}
