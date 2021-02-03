using Microsoft.AspNetCore.Mvc;

namespace TheSocialBaz.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    // If working with backend only, inherit ControllerBase class
    public abstract class ApiController : ControllerBase
    {
        
    }
}
