using Godot;

namespace Components.Ai;

public interface IAiAgent
{
    public AiInterestCollection Interests { get; set; }
}
