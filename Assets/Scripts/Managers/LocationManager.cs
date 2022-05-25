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

    public void Awake()
    {
        Instance = this;

        _forest = new Forest();
        _marbleQuarry = new MarbleQuarry();
        _graniteQuarry = new GraniteQuarry();
        _constantinople = new Constantinople();
        _constructionSite1 = new ConstructionSite(LocationType.ConstructionSite1, "Construction Site 1");
        _constructionSite2 = new ConstructionSite(LocationType.ConstructionSite2, "Construction Site 2");
        _constructionSite3 = new ConstructionSite(LocationType.ConstructionSite3, "Construction Site 3");
    }

    public ILocation GetLocation(LocationType locationType)
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

    public ILabourPoolLocation GetLabourPoolLocation(LocationType locationType)
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
            default:
                Debug.LogError($"Location type {locationType} was not yet implemented");
                return null;
        }
    }

    public void UpdateLabourPoolLocationUI(LocationType locationType)
    {
        UILocationContainer locationUIContainer = NavigationManager.Instance.GetLocation(locationType);
        ILabourPoolLocation resourcesLocation = GetLabourPoolLocation(locationType);
        locationUIContainer.SetSubTitleText(resourcesLocation.LabourPoolWorkers.Count);
    }
}
