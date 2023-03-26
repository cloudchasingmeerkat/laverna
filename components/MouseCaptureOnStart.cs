using Godot;

using utilities;

namespace Components;

public partial class MouseCaptureOnStart : Node
{
    [Export] public bool Active = true;

    private bool _activated = false;

    public override void _Input(InputEvent inputEvent)
    {
        if(Active && !_activated)
        {
            InputExtensions.CaptureMouse();
            _activated = true;
        }
    }
}
