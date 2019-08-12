using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WidgetServer.Data;
using WidgetServer.Models;
using WidgetServer.ViewModels;

namespace WidgetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WidgetsDataContext _context;

        public UsersController(WidgetsDataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Widgets)
                .Select(u => u.ToListViewModel())
                .ToListAsync();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users
                .Include(u => u.Widgets)
                .Select(u => u.ToListViewModel())
                .Where(u => u.Email == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/users/5
        [HttpPut("{emailAddress}")]
        public async Task<IActionResult> PutUser([FromRoute] string emailAddress, [FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (emailAddress != user.Email)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(emailAddress))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = model.ToUser(model);

            bool exists = UserExists(user.Email);
            if (!exists)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                var users = _context.Users
                .Where(u => u.Email == model.Email)
               .Include(u => u.Widgets).Select(w => w.ToListViewModel()).FirstOrDefault();
                return Ok(users);
            }
            return CreatedAtAction("GetUser", new { id = user.Email }, user.ToListViewModel());
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(string emailAddres)
        {
            return _context.Users.Any(e => e.Email == emailAddres);
        }
    }
}