using Godot;
using utilities;

namespace Components;

public partial class AppliedGravity : Node
{
	[Export] public CharacterBody3D Target;
	
	[Export] public int GravityStrength = 700;
	
	public override void _Ready()
	{
		Target = this.FindParentNodeIfNotSet(Target);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!Target.IsOnFloor())
		{
			Target.Velocity = Vector3.Down * GravityStrength * (float) delta;
			
			Target.MoveAndSlide();
		}
	}
}
