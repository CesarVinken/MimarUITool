
using System.Collections.Generic;

public interface IConstructionSiteUpgrade
{
    public string Name { get; }
    public ConstructionSiteUpgradeType ConstructionSiteUpgradeType { get; }
    public List<IAccumulativePlayerStat> Costs { get; }
    public string GetEffectDescription();

}
