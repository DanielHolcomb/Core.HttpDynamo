using Core.HttpDynamo;
using Microsoft.AspNetCore.Mvc;

namespace SampleHttpDynamoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SampleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("GetFromThis")]
        public async Task<IActionResult> GetFromThis()
        {
            var getThis = await HttpDynamo.GetRequestAsync(_httpClientFactory, "https://localhost:7144/Sample/GetThis");

            return Ok(getThis);
        }

        [HttpGet]
        [Route("GetThis")]
        public IActionResult GetThis()
        {
            return Ok("This");
        }

        [HttpPost]
        [Route("PostFromThis")]
        public async Task<IActionResult> GetLudumDareProfile([FromBody] PostItem item)
        {

            var postThis = await HttpDynamo.PostRequestAsync<PostItem>(_httpClientFactory, "https://localhost:7144/Sample/PostThis", item);

            return Ok(postThis);
        }

        [HttpPost]
        [Route("PostThis")]
        public IActionResult PostThis([FromBody] PostItem item)
        {
            item.Name = "Posted";
            return Ok(item);
        }
    }
}