using Godot;

namespace Components.Ai.Interests;

public partial class SeekPlayerInterest : IAiInterest<PlayerCharacter>
{
    public int Bias { get ; set; } = 10;
    public bool HasInfiniteRange { get; set; } = false;
    public int Range { get; set; } = 15;

    public Node3D NavigationAnchor { get; set; }

    public NavigationAgent3D NavigationAgent;
    
    public PlayerCharacter PlayerCharacter { get; private set; }

    public SeekPlayerInterest(PlayerCharacter playerCharacter, Node3D navigationAnchor, NavigationAgent3D navigationAgent)
    {
        PlayerCharacter = playerCharacter;
        NavigationAnchor = navigationAnchor;
        NavigationAgent = navigationAgent;
    }

    public bool CanBeAchieved(IAiAgent agent)
    {
        // FIXME do we even need this?
        if(agent is null || agent as Node3D is null)
        {
            return false;
        }
        
        if(HasInfiniteRange)
        {
            return true;
        }
        
        var distance = NavigationAnchor.GlobalPosition.DistanceTo(PlayerCharacter.GlobalPosition);
        
        return distance <= Range;
    }
    
    public void Achieve(IAiAgent agent)
    {
        NavigationAgent.TargetPosition = PlayerCharacter.GlobalPosition;
    }
}