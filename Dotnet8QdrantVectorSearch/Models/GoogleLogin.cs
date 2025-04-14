using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class GoogleLogin
{
    public int Id { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// 外部系統提供的唯一識別碼
    /// </summary>
    public string ExternalId { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
