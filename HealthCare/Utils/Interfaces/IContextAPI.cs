namespace HealthCare.Utils.Interfaces;

public interface IContextApi
{
    Task<HttpContext> GetContextAsync();
}