using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;

        private readonly IMapper _mapper;
        private  IUnitOfWork _unitOfWork;
        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

     
        [HttpPost]
        public async Task <IActionResult> Post(SigninDto signinDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var userIsExist = await _userManager.FindByEmailAsync(signinDto.Email);
            if (userIsExist != null)
            {

                var result = await _signinManager.PasswordSignInAsync(signinDto.Email, signinDto.Password, false, false);
                if (!result.Succeeded)
                {
                    return BadRequest();
                }
                var user = await _userManager.FindByNameAsync(signinDto.Email);
                return Ok(_unitOfWork.Users.CreateToken(user));

            }
            else
            {
                var user = _mapper.Map<IdentityUser>(signinDto);
                var result = await _userManager.CreateAsync(user, signinDto.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                await _signinManager.SignInAsync(user, isPersistent: false);
                return Ok(_unitOfWork.Users.CreateToken(user));

            }
            return BadRequest();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
