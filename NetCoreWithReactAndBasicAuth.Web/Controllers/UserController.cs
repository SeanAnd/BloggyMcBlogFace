using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWithReactAndBasicAuth.Core.Models;
using NetCoreWithReactAndBasicAuth.Core.Services;
using NetCoreWithReactAndBasicAuth.Web.Models;

namespace NetCoreWithReactAndBasicAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;
        protected readonly IMapper mapper;

        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper)
        {
            this.logger = logger;
            this.userService = userService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody]AuthenticationModel model)
        {
            User? user = await userService.Authenticate(model.Username, model.Password);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<ActionResult> Signup([FromBody] UserViewModel model)
        {
            var mappedUser = MapToCoreModel(model);
            User? user = await userService.Signup(mappedUser);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        protected User MapToCoreModel(UserViewModel viewModel)
        {
            return mapper.Map<User>(viewModel);
        }
    }
}