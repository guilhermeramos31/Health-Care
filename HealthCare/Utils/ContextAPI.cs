using HealthCare.Utils.Interfaces;

namespace HealthCare.Utils;

public class ContextApi(IHttpContextAccessor accessor) : IContextApi
{
    public Task<HttpContext> GetContextAsync()
    {
        return Task.Run(() =>
        {
            var accessorHttpContext = accessor.HttpContext ??
                                      throw new InvalidOperationException("Context is missing.");
            return accessorHttpContext;
        });
    }
}