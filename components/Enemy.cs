using System.Collections.Generic;
using System.Linq;
using Components.Ai;
using Components.Ai.Interests;
using Godot;
using utilities;

namespace Components;

public partial class Enemy : CharacterBody3D, IAiAgent
{
    [Export] public NavigationAgent3D NavigationAgent;
    [Export] public Node3D NavigationAnchor;

    [Export] public float MovementSpeed = 5.0f;
	
	[Export] public PlayerCharacter PlayerCharacter;

    public AiInterestCollection Interests { get; set; } = new();

    public override void _Ready()
    {
        // TODO check if this is set in editor
        this.TryFindNodeInChildrenRecursively(out NavigationAgent);
        
        // TODO check if this is set in editor
        this.TryFindNodeInScene(out PlayerCharacter);
        
        NavigationAnchor = GetNode<Node3D>("CollisionShape3D/NavigationAnchor");

        // These values need to be adjusted for the actor's speed
        // and the navigation layout.
        NavigationAgent.PathDesiredDistance = 0.5f;
        NavigationAgent.TargetDesiredDistance = 0.5f;

        // Make sure to not await during _Ready.
        Callable.From(ActorSetup).CallDeferred();
        
        var seekPlayerInterest = new SeekPlayerInterest(PlayerCharacter, NavigationAnchor, NavigationAgent);
        
        Interests.AddInterest(seekPlayerInterest);
    }

    public override void _PhysicsProcess(double delta)
    {
        // NOTE this is set by Interests
		// if(target is not null)
		// {
		// 	MovementTarget = target.GlobalPosition;
		// }
		
		// GD.Print($"Target: {MovementTarget}");

        // if (NavigationAgent.IsNavigationFinished())
        // {
		// 	GD.Print("Navigation finished");
        //     return;
        // }
        
        var interests = Interests.GetAchievableInterests(this);
        
        var chosenInterest = interests.FirstOrDefault();
        
        chosenInterest?.Achieve(this);
        
        if(NavigationAgent.IsNavigationFinished())
        {
            return;
        }
        
        Vector3 currentAgentPosition = NavigationAnchor.GlobalPosition;
        Vector3 nextPathPosition = NavigationAgent.GetNextPathPosition();
        
        //GD.Print(NavigationAgent.GetCurrentNavigationPath().Length);

        //GD.Print(_navigationAgent.GetCurrentNavigationPathIndex());
        //GD.Print(_navigationAgent.GetCurrentNavigationPath());
		
		// GD.Print($"CurrentPosition: {nextPathPosition}");
		// GD.Print($"NextPosition: {nextPathPosition}");

        Vector3 newVelocity = (nextPathPosition - currentAgentPosition).Normalized();
        newVelocity *= MovementSpeed;
        
        Velocity = new Vector3(newVelocity.X, 0f, newVelocity.Z);

        MoveAndSlide();
    }

    private async void ActorSetup()
    {
        // Wait for the first physics frame so the NavigationServer can sync.
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
    }
}