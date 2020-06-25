using EUCore.Entity.Enums;

namespace EUCore.Entity.Common
{
    public interface IPhone : IAllowContact
    {
        PhoneType? PhoneType { get; set; }
        string Phone { get; set; }
    }
}
