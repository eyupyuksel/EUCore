namespace EUCore.Entity.Configuration
{
    public class ConfigurationEntity : EntityBase<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
