using Godot;

namespace Components.Ai;

public static class AiHelpers
{
    public static void MoveUsingNavigation(
        CharacterBody3D characterBody,
        Node3D navigationAnchor,
        NavigationAgent3D navigationAgent,
        float movementSpeed
    )
    {
        if (navigationAgent.IsNavigationFinished())
        {
            return;
        }

        Vector3 currentAgentPosition = navigationAnchor.GlobalPosition;
        Vector3 nextPathPosition = navigationAgent.GetNextPathPosition();

        Vector3 newVelocity = (nextPathPosition - currentAgentPosition).Normalized();
        newVelocity *= movementSpeed;

        characterBody.Velocity = new Vector3(newVelocity.X, 0f, newVelocity.Z);

        characterBody.MoveAndSlide();
    }
}