using Components.Ai;
using Godot;
using Godot.Collections;

namespace Components;

// TODO move this into separate files
public enum AiState
{
    IDLE,
    SEEKING_PLAYER,
    PATROL
}

// TODO move this into separate files
public class AiStateData
{
    public AiState CurrentState = AiState.IDLE;

    // TODO can we handle this using NavigationAgent3D.TargetPosition instead?
    public Vector3? TargetMovementPosition;
}

public partial class Enemy : CharacterBody3D
{
    [Export]
    public NavigationAgent3D NavigationAgent;

    [Export]
    public Node3D NavigationAnchor;

    [Export]
    public float MovementSpeed = 3.0f;

    [Export]
    public float AttackRange = 5.0f;

    [Export]
    public float SightRange = 8.0f;

    [Export] public Array<Node3D> PatrolPath;
    int? CurrentPatrolPathIndex = null;

    public AiStateData AiState = new();

    bool navigationReady = false;

    [Export]
    public PlayerCharacter PlayerCharacter;

    public override void _Ready()
    {
        NavigationAgent.PathDesiredDistance = 0.5f;

        NavigationAgent.TargetDesiredDistance = 0.5f;

        // Make sure to not await during _Ready.
        // see more: https://docs.godotengine.org/en/stable/tutorials/navigation/navigation_introduction_2d.html#setup-for-2d-scene
        Callable.From(ActorSetup).CallDeferred();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!navigationReady)
        {
            return;
        }

        RefreshAiState();

        var targetMovementPosition = AiState.TargetMovementPosition;

        if (targetMovementPosition is not null)
        {
            // TODO setting this too often will cause navigation issues
            // FIXME Problem: sometimes want to set targetpos often (seek player), sometimes not often, such as when patrolling
            NavigationAgent.TargetPosition = targetMovementPosition.Value;

            AiHelpers.MoveUsingNavigation(this, NavigationAnchor, NavigationAgent, MovementSpeed);
        }
    }

    private void RefreshAiState()
    {
        switch (AiState.CurrentState)
        {
            case Components.AiState.IDLE:
                PatrolOrSeekPlayer();
                break;
            case Components.AiState.SEEKING_PLAYER:
                SeekPlayer();
                break;
            case Components.AiState.PATROL:
                Patrol();
                break;
            default:
                GD.PrintErr($"Unhandled AiState {AiState.CurrentState}");
                break;
        }
    }

    private void PatrolOrSeekPlayer()
    {
        if (PlayerCharacterDetected())
        {
            AiState.CurrentState = Components.AiState.SEEKING_PLAYER;
        }
        else
        {
            if (PatrolPath.Count > 0)
            {
                AiState.CurrentState = Components.AiState.PATROL;
                // TODO this code is ugly here
                CurrentPatrolPathIndex = null;
            }
        }
    }

    private void SeekPlayer()
    {
        if (PlayerCharacterDetected())
        {
            AiState.TargetMovementPosition = PlayerCharacter.GlobalPosition;
        }
        else
        {
            AiState.CurrentState = Components.AiState.IDLE;
        }
    }

    private void Patrol()
    {
        if (PlayerCharacterDetected())
        {
            AiState.CurrentState = Components.AiState.SEEKING_PLAYER;
            CurrentPatrolPathIndex = null;
        }

        if (PatrolPath.Count == 0)
        {
            AiState.CurrentState = Components.AiState.IDLE;
            return;
        }

        if (NavigationAgent.IsNavigationFinished())
        {
            if (CurrentPatrolPathIndex is null)
            {
                CurrentPatrolPathIndex = GetClosestPatrolPathWaypointIndex();
            }
            else
            {
                CurrentPatrolPathIndex += 1;

                if (CurrentPatrolPathIndex >= PatrolPath.Count)
                {
                    CurrentPatrolPathIndex = 0;
                }
            }

            Vector3 currentPatrolTargetPosition = PatrolPath[CurrentPatrolPathIndex.Value].GlobalPosition;


            AiState.TargetMovementPosition = currentPatrolTargetPosition;
        }
    }

    // TODO introduce extension method for this or simplify using LINQ
    private int GetClosestPatrolPathWaypointIndex()
    {
        var closestPatrolPathWaypointIndex = 0;

        var smallestDistanceUntilNow = float.MaxValue;

        for (int i = 0; i < PatrolPath.Count; i++)
        {
            var waypoint = PatrolPath[i];

            var distance = NavigationAnchor.GlobalPosition.DistanceTo(waypoint.GlobalPosition);

            if (distance < smallestDistanceUntilNow)
            {
                smallestDistanceUntilNow = distance;
                closestPatrolPathWaypointIndex = i;
            }
        }

        return closestPatrolPathWaypointIndex;
    }

    private bool PlayerCharacterDetected()
    {
        var distanceToPlayer = PlayerCharacter.GlobalPosition.DistanceTo(NavigationAnchor.GlobalPosition);

        return distanceToPlayer < SightRange;
    }

    private async void ActorSetup()
    {
        // Wait for the first physics frame so the NavigationServer can sync.
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        navigationReady = true;
    }
}
