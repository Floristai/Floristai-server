namespace Floristai.Services
{
    public interface IJwtKeyHoldingService
    {
        string JwtTokenKey { get; set; }
    }
}