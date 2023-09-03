using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly DataContext _context;
        public CitiesController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<List<City>>> Get()
        {
            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<City>>> Add(City requestCity)
        {
            _context.Cities.Add(requestCity);
            await _context.SaveChangesAsync();

            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {

            var dbCity = await _context.Cities.FindAsync(id);
            if (dbCity == null) return BadRequest("City not found.");
            return Ok(dbCity);
        }

        [HttpPut]
        public async Task<ActionResult<List<City>>> UpdateCity(City requestCity)
        {
            var dbCity = await _context.Cities.FindAsync(requestCity.Id);

            if (dbCity == null) return BadRequest("City not found.");

            dbCity.Name = requestCity.Name;
            dbCity.Population = requestCity.Population;
            dbCity.Longitude = requestCity.Longitude;
            dbCity.Latitude = requestCity.Latitude;

            await _context.SaveChangesAsync();

            return Ok(await _context.Cities.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult<List<City>>> DeleteCity(int id)
        {
            var dbCity = await _context.Cities.FindAsync(id);
            if (dbCity == null) return BadRequest("City not found.");
            _context.Cities.Remove(dbCity);
            await _context.SaveChangesAsync();
            return Ok(await _context.Cities.ToListAsync());
        }

    }
}
