using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningCenter.Domain.IAM.Models.Comands;
using LearningCenter.Domain.IAM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.Presentation.IAM.Controllers
{
    [Route("v1/[controller]")]
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
        [Route("getall")]
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
        [AllowAnonymous]
        
        public async Task<IActionResult> RegisterAsync([FromBody] SingupCommand command)
        {
           var retun =  await _userCommandService.Handle(command);
            return CreatedAtAction("register", new { id = retun });
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] SigninCommand command)
        {
            var retun =  await _userCommandService.Handle(command);
            return CreatedAtAction("login", new { jwt = retun });
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
