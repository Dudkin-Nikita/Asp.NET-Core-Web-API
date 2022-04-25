﻿namespace Meetups.Domain.Entities
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Meetup> Meetups { get; set; } = new List<Meetup>();

    }
}
