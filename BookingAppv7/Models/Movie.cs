using System;
using System.Collections.Generic;

namespace BookingAppv7.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string Director { get; set; } = null!;

    public virtual ICollection<Showing> Showings { get; set; } = new List<Showing>();
}
