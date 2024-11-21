using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TASK1
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectoriesController:ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<DirectoriesController> _logger;  // Добавьте это поле


        public DirectoriesController(AppDbContext context, ILogger<DirectoriesController> logger)
        {
            this.context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Directory>>> GetDirectories()
        {           
            try
            {
                var directories = await context.Directories
               .Include(d => d.Children)
               .ToListAsync();
                return Ok(directories);
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, "Ошибка при получении списка директорий");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Directory>> GetDirectory(int id)
        {
            var directory = await context.Directories
                .Include(d => d.Children)
                .FirstOrDefaultAsync(d => d.Id == id);

            if(directory == null) return NotFound();
            return Ok(directory);   
        }

        [HttpPost]
        public async Task<ActionResult<Directory>> CreateDirectory(Directory directory)
        {
            context.Directories.Add(directory);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateDirectory), new {id = directory.Id}, directory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDirectory(int id, Directory directory)
        {
            if (id != directory.Id) return BadRequest();

            context.Entry(directory).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirectory(int id)
        {
            var directory = await context.Directories.FindAsync(id);
            if (directory == null) return NotFound();

            context.Directories.Remove(directory);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
