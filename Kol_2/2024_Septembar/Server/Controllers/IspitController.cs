namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }

    [HttpGet]
    [Route("get")]
    public ActionResult Get()
    {
        return Ok("Hello from IspitController");
    }
}
