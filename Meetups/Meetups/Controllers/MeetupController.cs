using Meetups.Domain;
using Meetups.Domain.Entities;
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

        [HttpPost]
        public async Task<ActionResult<List<Meetup>>> AddHero(Meetup meetup)
        {
            context.Meetups.Add(meetup);
            await context.SaveChangesAsync();
            return Ok(await context.Meetups.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Meetup>>> UpdateMeetup(Meetup request)
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

            //context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(await context.Meetups.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Meetup>> Delete(int id)
        {
            var hero = await context.Meetups.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Meetup not found");
            }

            context.Meetups.Remove(hero);

            await context.SaveChangesAsync();

            return Ok(await context.Meetups.ToListAsync());
        }



    }
}
