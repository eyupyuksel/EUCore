using Newtonsoft.Json;

namespace EUCore.Entity
{
    public interface IUserEntity : IEntity<int>
    {
        string FirstName { get; }
        string LastName { get; }
        string MailAddress { get; }
        [JsonIgnore]
        string Password { get; }
        string ProfileImage { get; }
    }

    public interface ITokenUser
    {
        int Id { get; }
        string Subject { get; }
        string Audience { get; }
        string UniqueName { get; }
    }
}
