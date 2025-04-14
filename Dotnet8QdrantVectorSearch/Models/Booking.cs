using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string GroupOrderId { get; set; }

    public int? ConversationId { get; set; }

    public int BuildingId { get; set; }

    public int RoomId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public int CustomerNumber { get; set; }

    public string CustomerFirstName { get; set; }

    public string CustomerLastName { get; set; }

    public string ContactPhone { get; set; }

    public string CustomerCountry { get; set; }

    public decimal? Commission { get; set; }

    public decimal TotalPrice { get; set; }

    public string OrderPayment { get; set; }

    public string OrderState { get; set; }

    public string PaymentState { get; set; }

    public string OrderRemark { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public int RoomCount { get; set; }

    public virtual Building Building { get; set; }

    public virtual Message Conversation { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Room Room { get; set; }

    public virtual User User { get; set; }
}
