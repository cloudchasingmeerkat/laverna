namespace Components.Ai.Interests;

public partial class PatrolPathInterest : IAiInterest<PlayerCharacter>
{
    public int Bias { get ; set; } = 1;
    public bool HasInfiniteRange { get; set; } = false;
    public int Range { get; set; } = 15;

    public void Achieve(IAiAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public bool CanBeAchieved(IAiAgent agent)
    {
        throw new System.NotImplementedException();
    }
}