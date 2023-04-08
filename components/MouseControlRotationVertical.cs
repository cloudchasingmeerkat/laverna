using Godot;
using static Godot.Mathf;

using utilities;

namespace Components;

public partial class MouseControlRotationVertical : Node
{
    [Export] public bool Active = true;

    [Export] public Node3D Target;

    [Export] public float MouseSensitivity = 0.3f;

    private float _rotationXAccumulated = 0f;

    public override void _Ready()
    {
        Target = this.FindParentNodeIfNotSet(Target);
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (Active && InputExtensions.IsMouseCaptured() && inputEvent is InputEventMouseMotion mouseMotion)
        {
            _rotationXAccumulated += MouseSensitivity * mouseMotion.AsRotation3dDeg().X;

            var clampedRotationX = Clamp(_rotationXAccumulated, -89, 89);

            Target.ResetRotation();

            Target.RotateObjectLocal(Vector3.Right, DegToRad(clampedRotationX));
        }
    }
}
