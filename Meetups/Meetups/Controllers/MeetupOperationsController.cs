using Meetups.Domain;
using Meetups.Domain.Entities;
using Meetups.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Meetups.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupOperationsController : ControllerBase
    {
        private readonly DataContext context;

        public MeetupOperationsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meetup>> Get(int id)
        {
            var meetup = await context.Meetups.FindAsync(id);
            if (meetup == null)
            {
                return BadRequest("Meetup not found");
            }
            return Ok(meetup);
        }

        [HttpGet("filter/{name}")]
        public async Task<ActionResult<List<Meetup>>> Filter(string name)
        {
            var meetups = await context.Meetups.ToListAsync();
            var meetup = meetups.Where(x => x.Name == name);
            if (meetup == null)
            {
                return BadRequest("Meetup not found");
            }
            return Ok(meetup);
        }

        [HttpGet("sorting")]
        public async Task<ActionResult<List<Meetup>>> Sorting()
        {
            var meetups = await context.Meetups.OrderBy(x => x.Name).ToListAsync();
            if (meetups == null)
            {
                return BadRequest("Meetup not found");
            }           
            return Ok(meetups);
        }
    }
}
