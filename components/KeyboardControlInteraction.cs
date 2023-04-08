using Components.Interaction;
using Godot;

using utilities;

namespace Components;

public partial class KeyboardControlInteraction : Node
{
    [Export] public RayCast3D Sensor;
    
    [Export] public PlayerCharacter Player;

    public override void _Ready()
    {
        Sensor = this.FindParentNodeIfNotSet(Sensor);
        Player = this.FindParentNodeIfNotSet(Player);
    }

    public override void _Process(double delta)
    {	
        if (Input.IsActionJustPressed("interact"))
        {
            if (Sensor.GetCollider() is Node3D hit)
            {
                IInteractible<PlayerCharacter> interactionTarget = null;

                if(hit.TryFindNodeInChildrenRecursively<Collectible>(out var collectible))
                {
                    interactionTarget = collectible;                    
                }
                if(hit.TryFindNodeInChildrenRecursively<Door>(out var door))
                {
                    interactionTarget = door;                    
                }
                
                interactionTarget?.OnInteraction(Player);
                return;
            }
        }
    }
}
