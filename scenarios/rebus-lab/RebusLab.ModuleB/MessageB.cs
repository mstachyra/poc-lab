namespace RebusLab.ModuleB
{
    public class MessageB
    {
        public MessageB()
        {
            Id = Guid.NewGuid();
            Name = nameof(MessageB);
        }

        public Guid Id { get; }
        public string Name { get; }

        public override string ToString()
        {
            return $"{Name} : {Id}";
        }
    }
}
