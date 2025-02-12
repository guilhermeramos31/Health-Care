﻿namespace HealthCare.Infrastructure.Configurations;

public static class ContextAccessor
{
    public static HttpContext Get(this IHttpContextAccessor accessor)
    {
        return accessor.HttpContext ??
               throw new InvalidOperationException("Context is missing.");
    }
}