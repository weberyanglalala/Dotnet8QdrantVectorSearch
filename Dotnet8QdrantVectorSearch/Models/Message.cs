using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int BookingId { get; set; }

    public string MessageContent { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public string MessageType { get; set; }

    public virtual Booking Booking { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User User { get; set; }
}
