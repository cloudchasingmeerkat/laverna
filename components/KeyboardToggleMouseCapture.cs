using Godot;

using utilities;

namespace Components;

public partial class KeyboardToggleMouseCapture : Node
{
    [Export] public bool Active = true;

    public override void _Input(InputEvent inputEvent)
    {
        if(Active && Input.IsActionJustPressed("cancel"))
        {
            if (InputExtensions.IsMouseCaptured())
            {
                InputExtensions.UncaptureMouse();
            } 
            else
            {
                InputExtensions.CaptureMouse();
            }
        }
    }
}
