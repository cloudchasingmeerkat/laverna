using Components.Interaction;
using Godot;
using System;

namespace Components;

public partial class PlayerCharacter : CharacterBody3D, IInteractionSource
{
	[Export] public int Money;
	
	public void AddMoney(int amount)
	{
		Money += amount;
		GD.Print($"Player gained money: {amount} and now has {Money}");
	}
}
