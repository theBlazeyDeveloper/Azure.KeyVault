namespace VisAbility.Azure.KeyVault
{
    public class Tag
    {
        public Tag(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        public string Name { get; }
        public string Value { get; }
    }
}
