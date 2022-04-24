namespace Meetups.Domain.Entities
{
    public class Meetup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public List<Keyword> Keywords { get; set; }
    }
}
