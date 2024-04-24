using EFCorePractice.PatrickGodTutorial.Data;
using EFCorePractice.PatrickGodTutorial.DTOs;
using EFCorePractice.PatrickGodTutorial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice.PatrickGodTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableRelationExampleController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TableRelationExampleController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacters()
        {
            var characters = await _dataContext.Characters.Include(c => c.Backpack)
                                                   .Include(c => c.Weapons)
                                                   .Include(c => c.Teams)
                                                   .ToListAsync();
            return Ok(characters);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            var character = await _dataContext.Characters.Include(c => c.Backpack)
                                                   .Include(c => c.Weapons)
                                                   .Include(c => c.Teams)
                                                   .FirstOrDefaultAsync(c => c.Id == id);
            if(character is null)
            {
                return NotFound();
            }
            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDTO request)
        {
            var newCharacter = new Character()
            {
                Name = request.Name,
            };
            var newBackpack = new Backpack()
            {
                Description = request.Backpack.Description,
                Character = newCharacter
            };
            var newWeapon = request.Weapons.Select(weapon => new Weapon { Name = weapon.Name ,Character = newCharacter}).ToList();
            var newTeams = request.Teams.Select(team => new Team { Name = team.Name ,Character = new List<Character> { newCharacter } }).ToList();
            newCharacter.Backpack = newBackpack;
            newCharacter.Weapons = newWeapon;
            newCharacter.Teams = newTeams;

            _dataContext.Characters.Add(newCharacter);
            await _dataContext.SaveChangesAsync();
        
            return Ok(await _dataContext.Characters.Include(c => c.Backpack)
                                                   .Include(c=>c.Weapons) 
                                                   .Include(c=>c.Teams)
                                                   .ToListAsync()); 
        }
    }
}
