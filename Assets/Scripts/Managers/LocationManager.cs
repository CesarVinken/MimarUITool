using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;

    private Forest _forest;
    private MarbleQuarry _marbleQuarry;
    private GraniteQuarry _graniteQuarry;
    private Rome _rome;
    private Ostia _ostia;
    private ForumRomanum _forumRomanum;
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
        _rome = new Rome();
        _ostia = new Ostia();
        _forumRomanum = new ForumRomanum();
        _constructionSite1 = new ConstructionSite(LocationType.ConstructionSite1, "Construction Site 1");
        _constructionSite2 = new ConstructionSite(LocationType.ConstructionSite2, "Construction Site 2");
        _constructionSite3 = new ConstructionSite(LocationType.ConstructionSite3, "Construction Site 3");

        _locations.Add(LocationType.Rome, _rome);
        _locations.Add(LocationType.ConstructionSite1, _constructionSite1);
        _locations.Add(LocationType.ConstructionSite2, _constructionSite2);
        _locations.Add(LocationType.ConstructionSite3, _constructionSite3);
        _locations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        _locations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        _locations.Add(LocationType.Forest, _forest);
        _locations.Add(LocationType.Ostia, _ostia);
        _locations.Add(LocationType.ForumRomanum, _forumRomanum);

        _workerlLocations.Add(LocationType.Rome, _rome);
        _workerlLocations.Add(LocationType.ConstructionSite1, _constructionSite1);
        _workerlLocations.Add(LocationType.ConstructionSite2, _constructionSite2);
        _workerlLocations.Add(LocationType.ConstructionSite3, _constructionSite3);
        _workerlLocations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        _workerlLocations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        _workerlLocations.Add(LocationType.Forest, _forest);

        _labourPoolLocations.Add(LocationType.Rome, _rome);
        _labourPoolLocations.Add(LocationType.ConstructionSite1, _rome);
        _labourPoolLocations.Add(LocationType.ConstructionSite2, _rome);
        _labourPoolLocations.Add(LocationType.ConstructionSite3, _rome);
        _labourPoolLocations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        _labourPoolLocations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        _labourPoolLocations.Add(LocationType.Forest, _forest);

        foreach (KeyValuePair<LocationType, ILocation> item in _locations)
        {
            if (item.Value is IPlayerLocation)
            {
                _playerLocations.Add(item.Key, item.Value as IPlayerLocation);
            }
        }
        //    _playerLocations.Add(LocationType.ConstructionSite1, _constructionSite1);
        //    _playerLocations.Add(LocationType.ConstructionSite2, _constructionSite2);
        //    _playerLocations.Add(LocationType.ConstructionSite3, _constructionSite3);
        //    _playerLocations.Add(LocationType.GraniteQuarry, _graniteQuarry);
        //    _playerLocations.Add(LocationType.MarbleQuarry, _marbleQuarry);
        //    _playerLocations.Add(LocationType.Forest, _forest);
        //    _playerLocations.Add(LocationType.ForumRomanum, _forumRomanum);
        //    _playerLocations.Add(LocationType.Ostia, _ostia);
    }

        public void InitialiseLocations()
    {
        _constructionSite1.SetName($"{PlayerUtility.GetPossessivePlayerString(PlayerManager.Instance.Players[PlayerNumber.Player1])} Construction Site");
        _constructionSite2.SetName($"{PlayerUtility.GetPossessivePlayerString(PlayerManager.Instance.Players[PlayerNumber.Player2])} Construction Site");
        _constructionSite3.SetName($"{PlayerUtility.GetPossessivePlayerString(PlayerManager.Instance.Players[PlayerNumber.Player3])} Construction Site");
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
        UIWorkerLocationContainer locationUIContainer = NavigationManager.Instance.GetLocationUIContainer(locationType) as UIWorkerLocationContainer;

        if(locationUIContainer == null)
        {
            Debug.LogError($"Could not parse UILocationContainer for {locationType}");
        }

        ILabourPoolLocation resourcesLocation = GetLabourPoolLocation(locationType);
        locationUIContainer.SetSubTitleText(resourcesLocation.GetLabourPoolWorkers().Count);
    }
}
