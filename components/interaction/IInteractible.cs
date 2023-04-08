namespace Components.Interaction;

public interface IInteractible<T>
    where T : IInteractionSource
{
    public void OnInteraction(T interactionSource);
}