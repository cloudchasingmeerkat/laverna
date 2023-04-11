namespace Components.Ai;

public interface IAiInterest
{
    public bool HasInfiniteRange { get; set; }
    public int Range { get; set; }

    public int Bias { get; set; }

    public bool CanBeAchieved(IAiAgent agent);
    public void Achieve(IAiAgent agent);
}


public interface IAiInterest<T> : IAiInterest
{
}