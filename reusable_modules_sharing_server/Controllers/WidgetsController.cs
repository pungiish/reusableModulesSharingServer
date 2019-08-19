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
using WidgetServer.Models;
using WidgetServer.ViewModels;

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
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetWidgets([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == null)
                return NotFound();

            var user = _context.Users.Find(id).ToViewModel();

            if (user == null)
                return NotFound();

            var widgets = await _context.Widgets
                .Where(w => w.UserId == user.Email)
                .Select(w => w.ToNewViewModel())
                .OrderBy(w => w.Colour)
                .ToListAsync();

            if (widgets == null)
                return NotFound();

            return Ok(widgets);                
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
            using (var reader = System.IO.File.OpenText(format))
            {
                var fileText = await reader.ReadToEndAsync();
                if(widget.Colour != null)
                    fileText = fileText.Replace("colour", "'" + widget.Colour + "'");
                if(widget.Text != null)
                    fileText = fileText.Replace("circleText", "'" + widget.Text + "'");

                return Content(fileText, "text/javascript");
            }

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
            var user = _context.Users
                .Find(viewmodel.UserId);

            if (user == null)
                return NotFound();

            user.Widgets = await _context.Widgets
                .Where(w => w.User == user).ToListAsync();

           

            var widget = viewmodel.ToModel();
            widget.UserId = user.Email;
            var widgetExists = user.Widgets
                .Where(w => w.Colour == widget.Colour &&
                w.Name == widget.Name &&
                w.Text == widget.Text &&
                w.UserId == widget.UserId).SingleOrDefault();
            if (widgetExists != null)
            {
                return Ok(JsonConvert.SerializeObject(widgetExists.Id));
            }

            _context.Widgets.Add(widget);
            await _context.SaveChangesAsync();
            return Ok(JsonConvert.SerializeObject(widget.Id));

        }

        // PUT: api/widgets/update
        [HttpPut("update")]
        public async Task<IActionResult> PutUser([FromBody] UpdateWidgetViewModel viewModel)
        {
            var widget = viewModel.ToModel();
            widget.User = _context.Users.FirstOrDefault(u => u.Email == widget.UserId);
            _context.Entry(widget).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WidgetExists(widget.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }


        private bool WidgetExists(string id)
        {
            return _context.Widgets.Any(w => w.Id == id);
        }
    }


}