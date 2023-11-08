namespace RebusLab.ModuleA;

public class MessageA
{
    public MessageA()
    {
        Id = Guid.NewGuid();
        Name = nameof(MessageA);
    }

    public Guid Id { get; }
    public string Name { get; }

    public override string ToString()
    {
        return $"{Name} : {Id}";
    }
}
