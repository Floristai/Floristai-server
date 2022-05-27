namespace Floristai.Services
{
    public interface IUserIdService
    {
        int GetUserID();
        string GetUserName();
        Task<string> GetUserClaims(int userId);
    }
}