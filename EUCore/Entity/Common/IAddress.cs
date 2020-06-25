using EUCore.Entity.Enums;

namespace EUCore.Entity.Common
{
    public interface IAddress : IBasicAddress
    {
        AddressType? AddressType { get; set; }
        int? CountryId { get; set; }
        int? CityId { get; set; }
        int? CountyId { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
    }

    public interface IBasicAddress
    {
        int? NeighborhoodId { get; set; }
        string Address { get; set; }
    }
}
