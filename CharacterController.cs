using Godot;
using System;

public partial class CharacterController : CharacterBody3D
{
	public override void _Ready()
	{
		Velocity = new Vector3(1.0f,0.0f,0.0f);
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
}
