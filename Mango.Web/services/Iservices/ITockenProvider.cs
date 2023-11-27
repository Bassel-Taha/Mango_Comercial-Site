namespace Mango.Web.services.Iservices
{
    public interface ITockenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
