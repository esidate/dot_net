using Microsoft.AspNetCore.Mvc;

namespace dot_net.Controllers
{

    [ApiController]
    [Route("")]
    public class IndexController : ControllerBase
    {
        public IndexController()
        {
        }

        [HttpGet]
        public OkResult index()
        {
            return Ok();
        }
    }
}