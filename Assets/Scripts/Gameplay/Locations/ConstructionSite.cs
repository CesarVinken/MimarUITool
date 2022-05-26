using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : ILocation
{
    public LocationType LocationType { get; private set; }
    public string Name { get; private set; } = "";

    public List<IWorker> LabourPoolWorkers { get; private set; } = new List<IWorker>();

    public ConstructionSite(LocationType locationType, string name)
    {
        LocationType = locationType;
        Name = name;
    }
}
