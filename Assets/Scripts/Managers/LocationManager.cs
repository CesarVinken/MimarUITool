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

    public IWorkerLocation GetWorkerLocation(LocationType locationType)
    {
        switch (locationType)
        {
            case LocationType.Forest:
                return _forest;
            case LocationType.MarbleQuarry:
                return _marbleQuarry;
            case LocationType.GraniteQuarry:
                return _graniteQuarry;
            case LocationType.Constantinople:
                return _constantinople;
            case LocationType.ConstructionSite1:
                return _constructionSite1;
            case LocationType.ConstructionSite2:
                return _constructionSite2;
            case LocationType.ConstructionSite3:
                return _constructionSite3;
            default:
                Debug.LogError($"Location type {locationType} was not yet implemented");
                return null;
        }
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
