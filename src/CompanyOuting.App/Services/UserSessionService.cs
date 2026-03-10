namespace CompanyOuting.App.Services;

using Microsoft.JSInterop;

public enum UserRole
{
    Guest,
    Customer,
    ExperienceAgent
}

public sealed class UserSessionService
{
    private const string RoleStorageKey = "company-outing.role";
    private const string EmailStorageKey = "company-outing.email";

    private bool _isLoaded;

    public event Action? Changed;

    public UserRole CurrentRole { get; private set; } = UserRole.Guest;

    public string? Email { get; private set; }

    public bool IsProvider => CurrentRole == UserRole.ExperienceAgent;

    public bool IsLoaded => _isLoaded;

    public async Task EnsureLoadedAsync(IJSRuntime jsRuntime)
    {
        if (_isLoaded)
        {
            return;
        }

        try
        {
            var storedRole = await jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", RoleStorageKey);
            var storedEmail = await jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", EmailStorageKey);

            if (Enum.TryParse<UserRole>(storedRole, out var parsedRole))
            {
                CurrentRole = parsedRole;
                Email = storedEmail;
            }
        }
        catch (InvalidOperationException)
        {
            return;
        }

        _isLoaded = true;
        Changed?.Invoke();
    }

    public async Task SignInAsProviderAsync(string email, IJSRuntime jsRuntime)
    {
        CurrentRole = UserRole.ExperienceAgent;
        Email = email;
        _isLoaded = true;
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", RoleStorageKey, CurrentRole.ToString());
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", EmailStorageKey, email);
        Changed?.Invoke();
    }

    public async Task SignOutAsync(IJSRuntime jsRuntime)
    {
        CurrentRole = UserRole.Guest;
        Email = null;
        _isLoaded = true;
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", RoleStorageKey);
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", EmailStorageKey);
        Changed?.Invoke();
    }
}
