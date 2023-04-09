using Godot;

using utilities;

namespace Components;

public partial class MouseUncaptureOnLooseFocus : Node
{
    [Export] public bool Active = true;

    public override void _Notification(int notification)
    {
		if(Active && notification == MainLoop.NotificationApplicationFocusOut)
		{
			InputExtensions.UncaptureMouse();
		}
    }
}
