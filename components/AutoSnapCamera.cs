using Godot;
using utilities;

namespace Components;

public partial class AutoSnapCamera : Node
{
    [Export] public Camera3D Camera;

    [Export] public AutoSnapCameraPivot TargetPivot;

    private bool movedCamera = false;

    public override void _Ready()
    {
        Camera = this.FindParentNodeIfNotSet(Camera);
    }

    public bool TrySnapToActivePivotInScene()
    {
        if(this.TryFindNodeInScene(out TargetPivot, (pivot) => pivot.Active))
        {
            Camera.Transform = Transform3D.Identity;

            Camera.MoveToParent(TargetPivot);

            return true;
        }

        return false;
    }

    public override void _Process(double delta)
    {
        if(!movedCamera)
        {
            if(!TrySnapToActivePivotInScene())
            {
                GD.PrintErr($"Camera {Camera.GetPath()} did not find a valid {nameof(AutoSnapCameraPivot)} to snap to");
            }

            movedCamera = true;
        }
    }
}
