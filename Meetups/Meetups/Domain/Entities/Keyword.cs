namespace Meetups.Domain.Entities
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MeetupId { get; set; }
        public Meetup Meetup { get; set; }
    }
}
