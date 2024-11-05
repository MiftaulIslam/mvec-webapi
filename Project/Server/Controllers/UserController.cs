using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, User")]
    [ApiController]
    public class UserController : ControllerBase
    {
    }
}
