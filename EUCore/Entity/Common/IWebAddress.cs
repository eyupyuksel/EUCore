using EUCore.Entity.Enums;

namespace EUCore.Entity.Common
{
    public interface IWebAddress
    {
        WebAddressType? Type { get; set; }
        string Url { get; set; }
    }
}
