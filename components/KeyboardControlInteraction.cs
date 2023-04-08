using Godot;

using utilities;

namespace Components;

public partial class KeyboardControlInteraction : Node
{
    [Export] public RayCast3D Sensor;

    public override void _Ready()
    {
        Sensor = this.FindParentNodeIfNotSet(Sensor);
    }

    public override void _Process(double delta)
    {	
        if (Input.IsActionJustPressed("interact"))
        {
            if (Sensor.GetCollider() is Node3D hit)
            {
                if(hit.TryFindNodeInChildrenRecursively<Collectible>(out Collectible collectible))
                {
                    collectible.OnInteraction(this);
                }
            }
        }
    }
}
