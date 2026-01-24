using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infrastructure.Persistence.DbEntities;

public partial class Task
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public int CreatedById { get; set; }

    public DateTimeOffset CreatedDateTime { get; set; }

    public int? LastModifiedById { get; set; }

    public DateTimeOffset? LastModifiedDateTime { get; set; }
}
