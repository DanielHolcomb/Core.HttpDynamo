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
        [Route("FromThis")]
        public async Task<IActionResult> GetLudumDareProfile()
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
    }
}