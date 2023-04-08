using Godot;
using Components.Interaction;

using utilities;

namespace Components;

public partial class Collectible : Node, IInteractible<PlayerCharacter>
{
	[Export] public CsgShape3D CollectibleVisual;
    
	[Export] public int MonetaryValue = 30;

	[Export] public bool Collected;
	
    public override void _Ready()
    {
        CollectibleVisual = this.FindParentNodeIfNotSet(CollectibleVisual);
    }

    public void OnInteraction(PlayerCharacter player)
    {
        if (!Collected)
        {
            Collected = true;

            CollectibleVisual.Visible = false;
            CollectibleVisual.UseCollision = false;

            GD.Print($"{player.GetPath()} collected {this.GetPath()}");
            player.AddMoney(MonetaryValue);
        }
    }
}
