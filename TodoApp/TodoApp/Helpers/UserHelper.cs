using System.Security.Claims;

public class UserHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
}
