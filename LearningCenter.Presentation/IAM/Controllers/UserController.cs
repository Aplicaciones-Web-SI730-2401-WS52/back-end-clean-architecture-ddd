using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningCenter.Domain.IAM.Models.Comands;
using LearningCenter.Domain.IAM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.Presentation.IAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCommandService _userCommandService;
        
        public UserController(IUserCommandService userCommandService)
        {
            _userCommandService = userCommandService;
        }
        
        
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost("register")]
        
        public async Task<IActionResult> RegisterAsync([FromBody] SingupCommand command)
        {
            _userCommandService.Handle(command);
            return CreatedAtAction("Post", new { id = 1 });
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] SigninCommand command)
        {
            _userCommandService.Handle(command);
            return CreatedAtAction("Post", new { jwt = 1 });
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
       // [Route("Update")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
