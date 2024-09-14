using System;
using Components.Interaction;
using Godot;

namespace Components;

public partial class PlayerCharacter : CharacterBody3D
{
    [Export]
    public int Money;

    public void AddMoney(int amount)
    {
        Money += amount;
        GD.Print($"Player gained money: {amount} and now has {Money}");
    }
}
