using System;
using System.Collections.Generic;

namespace TaskManager.Infrastructure.Persistence.DbEntities;

public partial class Task
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
