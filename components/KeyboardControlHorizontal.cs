using Godot;
using utilities;

namespace Components;

public partial class KeyboardControlHorizontal : Node
{
	[Export] public CharacterBody3D Target;
	
	[Export] public int MovementSpeed = 10;
	
	public override void _Ready()
	{
		Target = this.FindParentNodeIfNotSet(Target);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector3.Zero;

		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z -= 1f;
		}
		
		if (Input.IsActionPressed("move_back"))
		{
			direction.Z += 1f;
		}

		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1f;
		}

		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1f;
		}
		
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();

			var basis = Target.Transform.Basis;
			
			basis.Z *= direction.Z;			
			basis.X *= direction.X;
			
			Target.Velocity = (basis.Z + basis.X) * MovementSpeed;

			Target.MoveAndSlide();
		}
	}
}
