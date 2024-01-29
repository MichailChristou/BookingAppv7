using System;
using System.Collections.Generic;

namespace BookingAppv7.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public int Showingid { get; set; }

    public int? Seatsno { get; set; }

    public virtual Showing Showing { get; set; } = null!;
}
