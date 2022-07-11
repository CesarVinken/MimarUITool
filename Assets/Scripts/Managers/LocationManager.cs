using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;

    private Forest _forest;
    private MarbleQuarry _marbleQuarry;
    private GraniteQuarry _graniteQuarry;
    private Constantinople _constantinople;
    private ConstructionSite _constructionSite1;
    private ConstructionSite _constructionSite2;
    private ConstructionSite _constructionSite3;

    private Dictionary<LocationType, IPlayerLocation> _playerLocations = new Dictionary<LocationType, IPlayerLocation>();
    private Dictionary<LocationType, ILocation> _locations = new Dictionary<LocationType, ILocation>();
    private Dictionary<LocationType, IWorkerLocation> _workerlLocations = new Dictionary<LocationType, IWorkerLocation>();
    private Dictionary<LocationType, ILabourPoolLocation> _labourPoolLocations = new Dictionary<LocationType, ILabourPoolLocation>();

    public void Setup()
    {
        Instance = this;

        _forest = new Forest();
        _marbleQuarry = new MarbleQuarry();
        _graniteQuarry = new GraniteQuarry();
        _constantinople = new Constantinople();
        _constructionSite1 = new ConstructionSite(LocationType.ConstructionSite1, "Construction Site 1");
        _constructionSite2 = new ConstructionSite(LocationType.ConstructionSite2, "Construction Site 2");
        _constructionSite3 = new ConstructionSite(LocationType.ConstructionSite3, "Construction Site 3");

        _locations.Add(LocationType.Constantinople, _constantinople);
        _locations.Add(LocationType.ConstructionSite1, _constructionSite1);
        _locations.Add(LocationType.ConstructionSite2, _constructionSite2);
        _locations.Add(LocationType.ConstructionSite3, _constructionSite3);
        _locations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        _locations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        _locations.Add(LocationType.Forest, _forest);

        _workerlLocations.Add(LocationType.Constantinople, _constantinople);
        _workerlLocations.Add(LocationType.ConstructionSite1, _constructionSite1);
        _workerlLocations.Add(LocationType.ConstructionSite2, _constructionSite2);
        _workerlLocations.Add(LocationType.ConstructionSite3, _constructionSite3);
        _workerlLocations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        _workerlLocations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        _workerlLocations.Add(LocationType.Forest, _forest);

        _labourPoolLocations.Add(LocationType.Constantinople, _constantinople);
        _labourPoolLocations.Add(LocationType.ConstructionSite1, _constantinople);
        _labourPoolLocations.Add(LocationType.ConstructionSite2, _constantinople);
        _labourPoolLocations.Add(LocationType.ConstructionSite3, _constantinople);
        _labourPoolLocations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        _labourPoolLocations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        _labourPoolLocations.Add(LocationType.Forest, _forest);

        _playerLocations.Add(LocationType.ConstructionSite1, _constructionSite1);
        _playerLocations.Add(LocationType.ConstructionSite2, _constructionSite2);
        _playerLocations.Add(LocationType.ConstructionSite3, _constructionSite3);
        _playerLocations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        _playerLocations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        _playerLocations.Add(LocationType.Forest, _forest);
    }

    public ILocation GetLocation(LocationType locationType)
    {
        if (_locations.TryGetValue(locationType, out ILocation playerLocation))
        {
            return playerLocation;
        }

        Debug.LogError($"Location type {locationType} was not yet implemented");
        return null;
    }

    public Dictionary<LocationType, ILabourPoolLocation> GetLabourPoolLocations()
    {
        return _labourPoolLocations;
    }

    public ILabourPoolLocation GetLabourPoolLocation(LocationType locationType)
    {
        if (_labourPoolLocations.TryGetValue(locationType, out ILabourPoolLocation labourPoolLocation))
        {
            return labourPoolLocation;
        }

        Debug.LogError($"Location type {locationType} was not yet implemented");
        return null;
    }

    public Dictionary<LocationType, IPlayerLocation> GetPlayerLocations()
    {
        return _playerLocations;
    }

    public IPlayerLocation GetPlayerLocation(LocationType locationType)
    {
        if(_playerLocations.TryGetValue(locationType, out IPlayerLocation playerLocation))
        {
            return playerLocation;
        }

        Debug.LogError($"Location type {locationType} was not yet implemented");
        return null;
    }

    public Dictionary<LocationType, IWorkerLocation> GetWorkerLocations()
    {
        return _workerlLocations;
    }

    public IWorkerLocation GetWorkerLocation(LocationType locationType)
    {
        if (_workerlLocations.TryGetValue(locationType, out IWorkerLocation workerLocation))
        {
            return workerLocation;
        }

        Debug.LogError($"Location type {locationType} was not yet implemented");
        return null;
    }


    public void UpdateLabourPoolLocationUI(LocationType locationType)
    {
        UILocationContainer locationUIContainer = NavigationManager.Instance.GetLocationUIContainer(locationType) as UILocationContainer;

        if(locationUIContainer == null)
        {
            Debug.LogError($"Could not parse UILocationContainer for {locationType}");
        }

        ILabourPoolLocation resourcesLocation = GetLabourPoolLocation(locationType);
        locationUIContainer.SetSubTitleText(resourcesLocation.GetLabourPoolWorkers().Count);
    }
}
