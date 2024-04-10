using API_DotNet8.Data;
using API_DotNet8.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var Heroes  = await _context.SuperHeroes.ToListAsync();

            return Ok(Heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var Hero = await _context.SuperHeroes.FindAsync(id);

            if(Hero == null)
            {
                return NotFound("Hero not found");
            }

            return Hero;
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero Hero)
        {
            await _context.SuperHeroes.AddAsync(Hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
            
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero Hero)
        {
            var FindHero = await _context.SuperHeroes.FindAsync(Hero.Id);

            if (FindHero == null)
            {
                return NotFound("Hero not found");
            }

            FindHero.Name = Hero.Name;
            FindHero.FirstName  = Hero.FirstName;
            FindHero.LastName = Hero.LastName;
            FindHero.Place = Hero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var Hero = await _context.SuperHeroes.FindAsync(id);

            if (Hero == null)
            {
                return NotFound("Hero not found");
            }

            _context.SuperHeroes.Remove(Hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
