using Godot;
using Components.Interaction;

using utilities;

namespace Components;

public partial class Door : Node, IInteractible<PlayerCharacter>
{
	[Export] public CsgShape3D DoorVisual;
    
	[Export] public AnimationPlayer DoorAnimationPlayer;

	[Export] public bool Opened;
	
    public override void _Ready()
    {
        DoorVisual = this.FindParentNodeIfNotSet(DoorVisual);
    }

    public void OnInteraction(PlayerCharacter player)
    {
        if (!DoorAnimationPlayer.IsPlaying())
        {
            if (!Opened)
            {
                Opened = true;
                
                DoorAnimationPlayer.Play("DoorOpenAnimation");
            }
            else 
            {
                Opened = false;
                
                DoorAnimationPlayer.PlayBackwards("DoorOpenAnimation");
            }
        }
    }
}
