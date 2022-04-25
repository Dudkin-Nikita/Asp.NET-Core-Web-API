using Meetups.Domain;
using Meetups.Domain.Entities;
using Meetups.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meetups.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordController : ControllerBase
    {
        private readonly DataContext context;

        public KeywordController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost("assigntag/{meetupId}/{keywordId}")]
        public async Task<ActionResult> AssignTag(int meetupId, int keywordId)
        {
            var meetup = await context.Meetups.FindAsync(meetupId);
            if (meetup == null)
            {
                return BadRequest("Meetup not found");
            }
            var keyword = await context.Keywords.FindAsync(keywordId);
            if (keyword == null)
            {
                return BadRequest("Keyword not found");
            }

            await context.Meetups.Include(x => x.Keywords).ToListAsync();
            if (!meetup.Keywords.Contains(keyword))
            {
                meetup.Keywords.Add(keyword);
                await context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<List<Keyword>>> AddMeetup(CreatedKeyword createdKeyword)
        {
            Keyword keyword = new Keyword { Id = createdKeyword.Id, Name = createdKeyword.Name };
            context.Keywords.Add(keyword);
            await context.SaveChangesAsync();
            return Ok(await context.Keywords.ToListAsync());
        }

        [HttpGet("getMeetupKeywords/{meetupId}")]
        public async Task<ActionResult<List<CreatedKeyword>>> GetMeetupKeywords(int meetupId)
        {
            var meetup = await context.Meetups.FindAsync(meetupId);
            if (meetup == null)
            {
                return BadRequest("Meetup not found");
            }
            await context.Meetups.Include(x => x.Keywords).ToListAsync();

            List<CreatedKeyword> keywords = new ();
            foreach (var keyword in meetup.Keywords)
            {
                CreatedKeyword createdKeyword = new CreatedKeyword { Id = keyword.Id, Name = keyword.Name };
                keywords.Add(createdKeyword);
            }
            return Ok(keywords);
        }

        [HttpDelete("{meetupId}/{keywordId}")]
        public async Task<ActionResult> DeleteMeetupKeyword(int meetupId, int keywordId)
        {
            var meetup = await context.Meetups.FindAsync(meetupId);
            if (meetup == null)
            {
                return BadRequest("Meetup not found");
            }
            var keyword = await context.Keywords.FindAsync(keywordId);
            if (keyword == null)
            {
                return BadRequest("Keyword not found");
            }

            await context.Meetups.Include(x => x.Keywords).ToListAsync();
            if (meetup.Keywords.Contains(keyword))
            {
                meetup.Keywords.Remove(keyword);
                await context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
