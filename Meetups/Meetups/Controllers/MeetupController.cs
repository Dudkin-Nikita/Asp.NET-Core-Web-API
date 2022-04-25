using Meetups.Domain;
using Meetups.Domain.Entities;
using Meetups.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meetups.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private readonly DataContext context;

        public MeetupController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Meetup>>> Get()
        {
            return Ok(await context.Meetups.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Meetup>>> AddMeetup(CreatedMeetup createdMeetup)
        {
            Meetup meetup = new Meetup{ Id = createdMeetup.Id, Name = createdMeetup.Name, Description = createdMeetup.Description, Date = createdMeetup.Date, Place = createdMeetup.Place};
            context.Meetups.Add(meetup);
            await context.SaveChangesAsync();
            return Ok(await context.Meetups.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Meetup>>> UpdateMeetup(CreatedMeetup request)
        {
            var meetup = await context.Meetups.FindAsync(request.Id);
            if (meetup == null)
            {
                return BadRequest("Meetup not found");
            }
            meetup.Name = request.Name;
            meetup.Description = request.Description;
            meetup.Place = request.Place;
            meetup.Date = request.Date;

            await context.SaveChangesAsync();

            return Ok(await context.Meetups.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Meetup>> Delete(int id)
        {
            var meetup = await context.Meetups.FindAsync(id);
            if (meetup == null)
            {
                return BadRequest("Meetup not found");
            }

            context.Meetups.Remove(meetup);

            await context.SaveChangesAsync();

            return Ok(await context.Meetups.ToListAsync());
        }
    }
}
