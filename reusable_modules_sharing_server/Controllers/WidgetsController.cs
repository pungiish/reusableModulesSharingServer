using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using reusable_modules_sharing_server.Models;
using reusable_modules_sharing_server.ViewModels;
using WidgetServer.Data;

namespace reusable_modules_sharing_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WidgetsController : ControllerBase
    {
        private readonly WidgetsDataContext _context;

        public WidgetsController(WidgetsDataContext context)
        {
            _context = context;
        }

        // GET: api/widgets
        [HttpGet]
        public async Task<IEnumerable<ListWidgetViewModel>> GetWidgets()
        {
            var widgets = await _context.Widgets
                .Include(w => w.User)
                .Select(w => w.ToViewModel())
                .ToListAsync();

            return widgets;                
        }

        // GET; api/widgets/{id}.js
        // returns javascript f
        [Route("{id}.js")]
        [HttpGet]
        public async Task<IActionResult> CreateUri([FromRoute] string id)
        {
            var widget = _context.Widgets.Where(
                w => w.Id == id).FirstOrDefault();
            if (widget == null)
                return NotFound();
            string format = string.Format(@"D:\Centiva\reusable_modules_sharing_server\reusable_modules_sharing_server\Assets\{0}.js", widget.Name);
            string text = System.IO.File.ReadAllText(format);
            return Content(text, "text/javascript");
        }

        // POST: api/widgets/new
        [Route("new")]
        [HttpPost]
        public async Task<IActionResult> New([FromBody] NewWidgetViewModel viewmodel)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.Find(viewmodel.UserId);
            if (user == null)
                return NotFound();

            var widget = viewmodel.ToModel();
            widget.UserId = user.Email;
            _context.Widgets.Add(widget);
            await _context.SaveChangesAsync();

            string id = JsonConvert.SerializeObject(widget.Id);
            return Ok(id);

        }


        private bool UserExists(string emailAddress)
        {
            return _context.Users.Any(e => e.Email == emailAddress);
        }
    }


}