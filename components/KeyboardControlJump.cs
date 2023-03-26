using Godot;

using utilities;

namespace Components;

public partial class KeyboardControlJump : Node
{
    [Export] public CharacterBody3D Target;

    [Export] public int JumpHeight = 200;

    public override void _Ready()
    {
        Target = this.FindParentNodeIfNotSet(Target);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("jump") && Target.IsOnFloor())
        {
            Target.Velocity = Vector3.Up * JumpHeight;

            Target.MoveAndSlide();
        }
    }
}
