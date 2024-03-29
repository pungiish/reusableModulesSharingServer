﻿using System;
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
        Dictionary<string, string> tags =
        new Dictionary<string, string>();
        public WidgetsController(WidgetsDataContext context)
        {
            _context = context;
            tags.Add("box", "<element-el></element-el>");
            tags.Add("circle", "<circle-element></circle-element>");

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
            string widgetTag = "";
            tags.TryGetValue(widget.Name, out widgetTag);
            string format = string.Format(@"D:\Centiva\reusable_modules_sharing_server\reusable_modules_sharing_server\Assets\{0}.js", widget.Name);
            using (var reader = System.IO.File.OpenText(format))
            {
                var fileText = await reader.ReadToEndAsync();
                if(widget.Colour != null && widget.Name == "box")
                    fileText = fileText.Replace("colour", "'" + widget.Colour + "'");
                else if (widget.Colour != null && widget.Name == "circle")
                    fileText = fileText.Replace("this.colour", "'" + widget.Colour + "'");
                if (widget.Text != null)
                    fileText = fileText.Replace("this.circletext", "'" + widget.Text + "'");
                fileText = fileText.Replace("webpackJsonp", "webpackJsonp" + widget.Id.Substring(0,4));

                string[] sub1 = widgetTag.Split('<');
                string[] sub2 = sub1[1].Split('>');
                string fin = sub2[0];
                string[] sub3 = widget.Tag.Split('<');
                string[] sub4 = sub3[1].Split('>');
                string fin1 = sub4[0];
                fileText = fileText.Replace(fin, fin1);

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
                w.Tag == widget.Tag &&
                w.UserId == widget.UserId).SingleOrDefault();
            if (widgetExists != null)
            {
                return Ok(JsonConvert.SerializeObject(widgetExists.Id));
            }
            // Make the widget tag unique
            int widgetCount = user.Widgets
                .Where(w => w.Name == widget.Name).ToList().Count;

            string widgetTag = "";
            tags.TryGetValue(widget.Name, out widgetTag);

            if (widgetCount > 0)
            {
                string endTag = "";
                string[] subs = widgetTag.Split('>');
                int subsLen = subs.Length - 1;
                for (int i = 0; i < (subsLen); i++)
                {
                    endTag += subs[i] + (widgetCount) + '>';
                }
                widget.Tag = endTag;
            }
            else
                widget.Tag = widgetTag;

            
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