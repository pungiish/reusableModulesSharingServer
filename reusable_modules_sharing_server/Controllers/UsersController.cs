using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using reusable_modules_sharing_server.Data;
using reusable_modules_sharing_server.Models;

namespace reusable_modules_sharing_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserModel _context;

        public UsersController(UserModel context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
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

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
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
                if (!UserExists(id))
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
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool exists = _context.Users.Any(existingUser => existingUser.GoogleID == user.GoogleID);
            if (!exists)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                User foundUser = _context.Users.Where(user1 => user1.GoogleID == user.GoogleID).Single();
                user.ID = foundUser.ID;
            }
            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // POST: api/Users/Components
        //[ActionName("File")]
        //[HttpPost("Components")]
        //public IActionResult Download(string id)
        //{
        //    IFileProvider provider = new PhysicalFileProvider("D:/Centiva/reusable_modules_sharing_server/reusable_modules_sharing_server");
        //    IFileInfo fileInfo = provider.GetFileInfo("test.txt");
        //    var readStream = fileInfo.CreateReadStream();
        //    var mimeType = "application/octet-stream";
        //    return File(readStream, mimeType, "test.txt");
        //}
        [HttpPost("Components")]
        public async Task<FileStream> DownloadFile(string fileName)
        {
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string file = Path.Combine(currentDirectory, "Assets/elements.js");
            FileStream first = new FileStream(file, FileMode.Open, FileAccess.Read);
            return first;

            
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}