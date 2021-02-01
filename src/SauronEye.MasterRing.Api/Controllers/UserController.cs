using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SauronEye.AllDomain.Domain.Contracts.Services;
using SauronEye.AllDomain.Domain.Services;
using SauronEye.MasterRing.Api.Model;
using System.Threading.Tasks;

namespace SauronEye.MasterRing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IServiceUser service;

        public UserController(IConfiguration config)
        {
            service = new ServiceUser(config);
        }

        [HttpGet]
        [Route("username/{username}")]
        public async Task<IActionResult> Get([FromRoute] string username)
        {
            return Ok(await service.GetByUsername(username));
        }

        [HttpGet]
        [Route("empty")]
        public User GetEmpty()
        {
            return new Model.User
            {
                Username = "",
                Name = ""
            };
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Save(User user)
        {
            var entity = user.GetEntity();
            if (entity!=null) await service.Save(entity);

            return Ok();
        }
    }
}
