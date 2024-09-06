namespace Components.Interaction;

public interface IInteractible<T>
{
    public void OnInteraction(T interactionSource);
}