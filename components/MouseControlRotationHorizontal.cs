using Godot;
using static Godot.Mathf;

using utilities;

namespace Components;

public partial class MouseControlRotationHorizontal : Node
{
    [Export] public bool Active = true;

    [Export] public Node3D Target;

    [Export] public float MouseSensitivity = 0.3f;

    private float _rotationYAccumulated = 0f;

    public override void _Ready()
    {
        Target = this.FindParentNodeIfNotSet(Target);
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (Active && InputExtensions.IsMouseCaptured() && inputEvent is InputEventMouseMotion mouseMotion)
        {
            _rotationYAccumulated += MouseSensitivity * mouseMotion.AsRotation3dDeg().Y;

            Target.ResetRotation();

            Target.RotateObjectLocal(Vector3.Up, DegToRad(_rotationYAccumulated));
        }
    }
}
