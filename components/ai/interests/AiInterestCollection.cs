using System.Collections.Generic;
using System.Linq;

namespace Components.Ai;

public class AiInterestCollection
{
    public List<IAiInterest> Interests { get; set; } = new();
    
    public void AddInterest(IAiInterest interest)
    {
        Interests.Add(interest);
    }
    
    public IEnumerable<IAiInterest> GetAchievableInterests(IAiAgent agent)
    {
        return Interests.Where(i => i.CanBeAchieved(agent));
    }
}