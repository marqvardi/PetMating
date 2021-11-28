using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMating.Api.Data.Interface;
using PetMating.Api.DTOs.Animal;
using PetMating.Api.Helpers;
using PetMating.Api.Models;
using Newtonsoft.Json;

namespace PetMating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly Data.ApplicationDbContext _dbContext;
        public AnimalController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, Data.ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        // [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<Pagination<ReturnAnimalDto>>> GetAllAnimals([FromQuery] PageSpecsParams pageSpecsParams)
        {
            if (pageSpecsParams.Search == null)
            {
                pageSpecsParams.Search = "";
            }

            // var teste = Path.GetDirectoryName("data/AnimalBreeds.json");
            // StreamReader r = new StreamReader(Path.GetDirectoryName("data/AnimalBreeds.json"));
            // string jsonString = r.ReadToEnd();
            // var m = JsonConvert.DeserializeObject<object>(jsonString);


            var animalFromDb = await _unitOfWork.Animal.GetAllWithInclude(null, null, "User", "Address");

            // var animalFromDb = await _unitOfWork.Animal.GetAll(c => c.FirstName.Contains(pageSpecsParams.Search.ToLower()) ||
            //                     c.LastName.Contains(pageSpecsParams.Search.ToLower()),
            //                      f => f.OrderBy(d => d.FirstName), "User");

            var returnAnimal = _mapper.Map<IReadOnlyList<ReturnAnimalDto>>(animalFromDb);

            var animalPaged = Pagination<ReturnAnimalDto>.PagedList(returnAnimal, pageSpecsParams);

            if (animalFromDb.Count() <= 0)
            {
                return NotFound("Not found");
            }

            return Ok(new Pagination<ReturnAnimalDto>(pageSpecsParams.PageIndex, pageSpecsParams.PageSize, returnAnimal.Count(), animalPaged));


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