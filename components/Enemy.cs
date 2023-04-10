using Godot;
using utilities;

namespace Components;

public partial class Enemy : CharacterBody3D
{
    private NavigationAgent3D _navigationAgent;

    private float _movementSpeed = 5.0f;
    private Vector3 _movementTargetPosition = new(15f, 3.362f, 20f);
	
	[Export] public Node3D target;

    public Vector3 MovementTarget
    {
        get { return _navigationAgent.TargetPosition; }
        set { _navigationAgent.TargetPosition = value; }
    }

    public override void _Ready()
    {
        this.TryFindNodeInChildrenRecursively(out _navigationAgent);

        // These values need to be adjusted for the actor's speed
        // and the navigation layout.
        _navigationAgent.PathDesiredDistance = 0.5f;
        _navigationAgent.TargetDesiredDistance = 0.5f;

        // Make sure to not await during _Ready.
        Callable.From(ActorSetup).CallDeferred();
    }

    public override void _PhysicsProcess(double delta)
    {
		if(target is not null)
		{
			MovementTarget = target.GlobalPosition;
		}
		
		// GD.Print($"Target: {MovementTarget}");

        if (_navigationAgent.IsNavigationFinished())
        {
			GD.Print("Navigation finished");
            return;
        }
        


        Vector3 currentAgentPosition = GetNode<Node3D>("CollisionShape3D/NavigationAnchor").GlobalPosition;
        Vector3 nextPathPosition = _navigationAgent.GetNextPathPosition();

        //GD.Print(_navigationAgent.GetCurrentNavigationPathIndex());
        //GD.Print(_navigationAgent.GetCurrentNavigationPath());
		
		// GD.Print($"CurrentPosition: {nextPathPosition}");
		// GD.Print($"NextPosition: {nextPathPosition}");

        Vector3 newVelocity = (nextPathPosition - currentAgentPosition).Normalized();
        newVelocity *= _movementSpeed;
        
        Velocity = new Vector3(newVelocity.X, 0f, newVelocity.Z);
        //Velocity = newVelocity;

        MoveAndSlide();
    }

    private async void ActorSetup()
    {
        // Wait for the first physics frame so the NavigationServer can sync.
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        // Now that the navigation map is no longer empty, set the movement target.
        MovementTarget = _movementTargetPosition;
    }
}