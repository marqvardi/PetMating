using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMating.Api.Data.Interface;
using PetMating.Api.Models;

namespace PetMating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLikePetController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserLikePetController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this._userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetActionResultAsync()
        {
            string id = "a77fa9cd-a5fe-44a6-b4ad-d00f3df55ace";
            Guid Aid = Guid.Parse("ab7eb5de-d51a-43ee-3184-08d9a301b860");

            var userLikePet = await unitOfWork.UserLikePet.GetAll();

            var favorite = await unitOfWork.UserLikePet.GetAll(g => g.UserId == id && g.IsFavourite == true);
            var avoid = await unitOfWork.UserLikePet.GetAll(g => g.UserId == id && g.IsFavourite == false);

            var GetAll = await unitOfWork.Animal.GetAll();

            // var ListWithoutFavouriteAndAvoid = await unitOfWork.UserLikePet.GetAllWithInclude(g => g.IsFavourite == false, null, "Animal");

            var userLikePet1 = await unitOfWork.UserLikePet.GetAll(c => c.AnimalId != Aid && c.UserId != id);
            var result = userLikePet.Any(g => g.AnimalId == Aid && g.UserId == id);

            if (result)
            {                
                return Ok(GetAll);
            }
            else
            {
                return BadRequest("Pet id or user id not found");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserLikePetById(Guid id)
        {
            var animal = await unitOfWork.Animal.GetFirstOrDefault(c => c.Id == id);

            if (animal != null)
            {
                return Ok(animal);
            }

            return BadRequest("Animal not found");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserLikePet userLikePet)
        {
            var userFromDbExist = await _userManager.FindByIdAsync(userLikePet.UserId);

            var AnimalFromDbExist = unitOfWork.Animal.GetFirstOrDefault(c => c.Id == userLikePet.AnimalId).Result;

            if (userFromDbExist != null && AnimalFromDbExist != null)
            {
                try
                {
                    unitOfWork.UserLikePet.Add(new UserLikePet()
                    { AnimalId = userLikePet.AnimalId, UserId = userLikePet.UserId, IsFavourite = true });
                    unitOfWork.Save();
                    return CreatedAtAction("GetUserLikePetById", new { id = userLikePet.AnimalId }, null);
                }
                catch (System.Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest("User or pet not found");
        }
    }
}