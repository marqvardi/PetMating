using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetMating.Api.Data.Interface;
using PetMating.Api.DTOs;
using PetMating.Api.DTOs.User;
using PetMating.Api.Helpers;
using PetMating.Api.Models;

namespace PetMating.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        public AuthController(SignInManager<User> signInManager, IConfiguration config, UserManager<User> userManager, IMapper autoMapper, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _config = config;
            _unitOfWork = unitOfWork;
            _mapper = autoMapper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = await _userManager.FindByEmailAsync(userForRegisterDto.Email);

            if (user != null) return BadRequest("User already exists");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var completed = await _unitOfWork.Complete();

            var userToReturn = _mapper.Map<UserDetailsToReturnDto>(userToCreate);
            userToReturn.Password = userForRegisterDto.Password;

            if (result.Succeeded)
            {
                return Created("login", userToReturn);
            }
            else
            {
                List<IdentityError> errorList = result.Errors.ToList();
                var errors = string.Join(", ", errorList.Select(e => e.Description));

                return BadRequest(errors);
            }

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(UserForLogInDto userLogInDto)
        {
            var user = await _userManager.FindByEmailAsync(userLogInDto.Email);

            if (user == null) return NotFound("User is not registered");

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogInDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = _mapper.Map<UserOutputDto>(user);

                return Ok(new
                {
                    token = GenerateJwtToken(user),
                    user = appUser
                });
            }

            return Unauthorized();
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<User>>> GetUsersAsync([FromQuery] PageSpecsParams pageSpecsParams)
        {
            var users = await _unitOfWork.User.GetAll(null, f => f.OrderBy(d => d.FirstName), "Address");

            var usersPaged = Pagination<User>.PagedList(users, pageSpecsParams);

            if (users.Count() <= 0)
            {
                return NotFound("Not found");
            }

            return Ok(new Pagination<User>(pageSpecsParams.PageIndex, pageSpecsParams.PageSize, users.Count(), usersPaged));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userFromDb = await _unitOfWork.User.GetFirstOrDefault(c => c.Id == id);
            var AnimalsFromUserDb = await _unitOfWork.Animal.GetAll(c => c.UserId == id);

            if (userFromDb != null)
            {
                try
                {
                    _unitOfWork.Animal.RemoveRange(AnimalsFromUserDb);
                    _unitOfWork.User.Remove(userFromDb);
                    _unitOfWork.Save();

                    return Ok();

                }
                catch (System.Exception)
                {

                    return BadRequest();
                }

            }
            return BadRequest();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
               {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}