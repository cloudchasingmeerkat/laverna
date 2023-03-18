using Godot;
using System;

public partial class CSGBox3D : CsgBox3D
{
	private Godot.Collections.Array<bool> SecretFlags;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SecretFlags = new Godot.Collections.Array<bool>();
		SecretFlags.Add(false);
		SecretFlags.Add(false);
		SecretFlags.Add(false);
		Timer t1 = GetNode<Timer>("Timer");
		t1.Timeout += () => {
		};
	}
}
