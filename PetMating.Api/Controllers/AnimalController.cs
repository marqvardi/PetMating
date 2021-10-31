using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMating.Api.Data.Interface;
using PetMating.Api.DTOs.Animal;
using PetMating.Api.Helpers;
using PetMating.Api.Models;

namespace PetMating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public AnimalController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Animal>>> GetAllAnimals([FromQuery] PageSpecsParams pageSpecsParams)
        {
            var animalFromDb = await _unitOfWork.Animal.GetAll(null, f => f.OrderBy(d => d.FirstName), "User");

            var animalPaged = Pagination<Animal>.PagedList(animalFromDb, pageSpecsParams);

            if (animalFromDb.Count() <= 0)
            {
                return NotFound("Not found");
            }

            return Ok(new Pagination<Animal>(pageSpecsParams.PageIndex, pageSpecsParams.PageSize, animalFromDb.Count(), animalPaged));


            // var list = Enum.GetValues(typeof(Colour)).Cast<Colour>().ToDictionary(t => (int)t, t => t.ToString());

            // return Ok();
            // return CreatedAtAction("GetById", dog, new { id = dog.Id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimalById(Guid id)
        {
            var animalFromDb = await _unitOfWork.Animal.GetAll(c => c.Id == id, null, "User");

            if (animalFromDb != null)
            {
                return Ok(animalFromDb);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateAnimalDto updateAnimalDto)
        {
            // var userFromDbExist = await _userManager.FindByIdAsync(updateAnimalDto.UserId);
            var animalAndUserFromDb = await _unitOfWork.Animal.GetFirstOrDefault(c => c.Id == updateAnimalDto.Id);
            // var animalExists = await GetAnimalById(updateAnimalDto.Id);

            if (animalAndUserFromDb != null)
            {
                try
                {
                    updateAnimalDto.DOB = DateTime.Now.AddYears(-3);
                    var updateInDb = _mapper.Map<Animal>(updateAnimalDto);

                    _unitOfWork.Animal.Update(updateInDb);

                    return Ok(updateInDb);
                }
                catch (System.Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest("User not found");
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateAnimalDto createAnimalDto)
        {
            var userFromDbExist = await _userManager.FindByIdAsync(createAnimalDto.UserId);

            if (userFromDbExist != null)
            {
                try
                {
                    createAnimalDto.DOB = DateTime.Now;
                    var createInDb = _mapper.Map<Animal>(createAnimalDto);

                    _unitOfWork.Animal.Add(createInDb);
                    _unitOfWork.Save();

                    return CreatedAtAction("GetAnimalById", new { id = createInDb.Id }, createInDb);
                }
                catch (System.Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest("User not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var animalFromDb = await _unitOfWork.Animal.GetFirstOrDefault(c => c.Id == id);

            if (animalFromDb != null)
            {
                try
                {
                    _unitOfWork.Animal.Remove(animalFromDb);
                    _unitOfWork.Save();

                    return Ok();
                }
                catch (System.Exception)
                {

                    return BadRequest();
                }

            }
            return NotFound("Animal not found in the database");
        }
    }
}