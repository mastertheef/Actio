namespace Actio.Services.Identity.Domain.Services
{
    public interface IEncriptor
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}
