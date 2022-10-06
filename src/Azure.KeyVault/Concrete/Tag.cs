namespace Azure.KeyVault
{
    public record Tag
    {
        public Tag(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; }
        public string Value { get; }
    }
}
