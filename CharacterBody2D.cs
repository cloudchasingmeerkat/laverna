using Godot;
using System;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	public override void _Ready() {
		Velocity = new Vector2(1.0f,1.0f);
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
}
