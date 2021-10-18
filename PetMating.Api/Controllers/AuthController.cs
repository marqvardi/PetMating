using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMating.Api.Data.Interface;
using PetMating.Api.DTOs;
using PetMating.Api.DTOs.User;
using PetMating.Api.Models;

namespace PetMating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(UserManager<User> userManager, IMapper autoMapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = autoMapper;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = await _userManager.FindByEmailAsync(userForRegisterDto.Email);

            if (user != null) return BadRequest("User already exists");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);



            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var completed = await _unitOfWork.Complete();

            var userToReturn = _mapper.Map<UserDetailsToReturnDto>(userToCreate);

            if (result.Succeeded)
            {
                return Created("login", userToReturn);
            }
            return BadRequest(result.Errors);
        }
    }
}