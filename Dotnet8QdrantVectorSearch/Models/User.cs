using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Country { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserFirstName { get; set; }

    public string UserLastName { get; set; }

    public int? GoogleLoginId { get; set; }

    public int? FackbookLoginId { get; set; }

    public int? LineLoginId { get; set; }

    public string Selfie { get; set; }

    public string NickName { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<FacebookLogin> FacebookLogins { get; set; } = new List<FacebookLogin>();

    public virtual FacebookLogin FackbookLogin { get; set; }

    public virtual GoogleLogin GoogleLogin { get; set; }

    public virtual LineLogin LineLogin { get; set; }

    public virtual ICollection<LineLogin> LineLogins { get; set; } = new List<LineLogin>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
