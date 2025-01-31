using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using HamraKitab.DataAccess.Data;
using HamraKitab.Models;
using HamraKitab.Models.DTO;
using Microsoft.AspNetCore.Authorization;


namespace HamraKitab_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GenreController> _logger;
        public GenreController(ApplicationDbContext context, ILogger<GenreController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        //Get All Genres
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }
        [HttpGet("{id}")]
        //Get a Genre by id
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }
        //[Authorize(Roles = "Admin")]
        //Create a Genre
        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre(AddGenreDto genreDto)
        {
            var genre = new Genre
            {
                Name = genreDto.Name
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }

        //[Authorize(Roles = "Admin")]
        //Update a Specific Genre
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, UpdateGenreDto genreDto)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            genre.Name = genreDto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        //Delete a Genre
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
