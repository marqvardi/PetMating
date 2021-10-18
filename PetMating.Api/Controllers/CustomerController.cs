using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetMating.Api.Data.Interface;
using PetMating.Api.Helpers;
using PetMating.Api.Models;

namespace PetMating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
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

        [HttpPost]
        public IActionResult Post()
        {
            var address = new Address()
            {
                City = "OSasco",
                Id = Guid.NewGuid(),
                StreetName = "Rua nanuque, 394",
                ZipCode = "2598-369"
            };

            var user = new User()
            {
                Address = address,
                Email = "dwdad@dwad.com",
                FirstName = "marcus",
                LastName = "vardi",
                AddressId = address.Id,
                DOB = DateTime.Now.AddYears(-43)
            };

            try
            {
                _unitOfWork.User.Add(user);
                _unitOfWork.Save();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(user);
        }
    }
}