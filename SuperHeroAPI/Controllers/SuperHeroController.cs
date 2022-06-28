using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var newHero = await _context.SuperHeros.FindAsync(hero.Id);
            if (newHero == null)
                return BadRequest("Hero not found");

            newHero.Name = hero.Name;
            newHero.FirstName = hero.FirstName;
            newHero.LastName = hero.LastName;
            newHero.Place = hero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpDelete("id")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var newHero = await _context.SuperHeros.FindAsync(id);
            if (newHero == null)
                return BadRequest("Hero not found");
            _context.SuperHeros.Remove(newHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
