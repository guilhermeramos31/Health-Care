using HealthCare.Utils.Interfaces;

namespace HealthCare.Utils;

public class ContextApi(IHttpContextAccessor accessor) : IContextApi
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public Task<HttpContext> GetContextAsync()
    {
        return Task.Run(() =>
        {
            var accessorHttpContext = _accessor.HttpContext ??
                                      throw new InvalidOperationException("Context is missing.");
            return accessorHttpContext;
        });
    }
}