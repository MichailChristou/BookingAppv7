using System;
using System.Collections.Generic;

namespace BookingAppv7.Models;

public partial class Showing
{
    public int Id { get; set; }

    public int Movieid { get; set; }

    public DateTime Datet { get; set; }

    public int? Seats { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
