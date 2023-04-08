using Godot;

using utilities;

namespace Components;

public partial class Collectible : Node
{
	[Export] public CsgShape3D Parent;

	[Export] public bool Collected;
	
    public override void _Ready()
    {
        Parent = this.FindParentNodeIfNotSet(Parent);
    }

    public void OnInteraction(Node interactionSource)
    {
        if (!Collected)
        {
            Collected = true;

            Parent.Visible = false;
            Parent.UseCollision = false;

            GD.Print($"Collected {Parent.GetPath()}");
        }
    }
}
