using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetMating.Api.Data.Interface;
using PetMating.Api.Models;

namespace PetMating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnimalController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {

            // var list = Enum.GetValues(typeof(Colour)).Cast<Colour>().ToDictionary(t => (int)t, t => t.ToString());

            return Ok();
            // return CreatedAtAction("GetById", dog, new { id = dog.Id });
        }
    }
}