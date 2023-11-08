namespace RebusLab.Shared
{
    public class MessageShared
    {
        public MessageShared()
        {
            Id = Guid.NewGuid();
            Name = nameof(MessageShared);
        }

        public Guid Id { get; }
        public string Name { get; }

        public override string ToString()
        {
            return $"{Name} : {Id}";
        }
    }
}
