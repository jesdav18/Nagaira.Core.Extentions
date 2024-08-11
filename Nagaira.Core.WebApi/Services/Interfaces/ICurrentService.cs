namespace Nagaira.Core.WebApi.Services.Interfaces
{
    public interface ICurrentService
    {
        string GetCurrentUser();
        string GetCurrentToken();
    }
}
