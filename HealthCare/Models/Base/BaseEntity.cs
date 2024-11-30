﻿namespace HealthCare.Models.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
}
