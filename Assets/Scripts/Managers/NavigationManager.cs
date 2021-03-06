using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour {
    public static NavigationManager Instance;

    public UITabButton CurrentTab { get; private set; } = null;

    [Header("Buttons")]

    [SerializeField] private UITabButton _gameTabButton;
    [SerializeField] private UITabButton _playersTabButton;
    [SerializeField] private UITabButton _statsTabButton;
    [SerializeField] private UITabButton _optionsTabButton;

    [Header("Containers")]

    [SerializeField] private UILocationContainer _forestContainer;
    [SerializeField] private UILocationContainer _marbleQuarryContainer;
    [SerializeField] private UILocationContainer _graniteQuarryContainer;
    [SerializeField] private UILocationContainer _constantinopleContainer;
    [SerializeField] private MonumentLocationUIContainer _constructionSite1Container;
    [SerializeField] private MonumentLocationUIContainer _constructionSite2Container;
    [SerializeField] private MonumentLocationUIContainer _constructionSite3Container;

    [SerializeField] private GameTabContainer _gameTabContainer;
    [SerializeField] private PlayersTabContainer _playersTabContainer;
    [SerializeField] private StatsTabContainer _statsTabContainer;
    [SerializeField] private OptionsTabContainer _optionsTabContainer;

    private Dictionary<LocationType, MonumentLocationUIContainer> _constructionSiteContainersByLocationType = new Dictionary<LocationType, MonumentLocationUIContainer>();

    private static int _initialForestLabourPoolSize = 6;
    private static int _initialGraniteLabourpoolSize = 5;
    private static int _initialMarbleLabourPoolSize = 4;
    private static int _initialCityLabourPoolSize = 8;

    public void Setup()
    {
        Instance = this;

        if (_gameTabButton == null)
        {
            Debug.LogError("_gameTabButton is not set");
        }
        if (_playersTabButton == null)
        {
            Debug.LogError("_playersTabButton is not set");
        }
        if (_statsTabButton == null)
        {
            Debug.LogError("_statsTabButton is not set");
        }
        if (_optionsTabButton == null)
        {
            Debug.LogError("_optionsTabButton is not set");
        }

        if (_forestContainer == null)
        {
            Debug.LogError("_forestContainer is not set");
        }
        if (_marbleQuarryContainer == null)
        {
            Debug.LogError("_marbleQuarryContainer is not set");
        }
        if (_graniteQuarryContainer == null)
        {
            Debug.LogError("_graniteQuarryContainer is not set");
        }
        if (_constantinopleContainer == null)
        {
            Debug.LogError("_constantinopleContainer is not set");
        }
        if (_constructionSite1Container == null)
        {
            Debug.LogError("_constructionSite1Container is not set");
        }
        if (_constructionSite2Container == null)
        {
            Debug.LogError("_constructionSite2Container is not set");
        }
        if (_constructionSite3Container == null)
        {
            Debug.LogError("_constructionSite3Container is not set");
        }

        if (_gameTabContainer == null)
        {
            Debug.LogError("_gameTabContainer is not set");
        }
        if (_playersTabContainer == null)
        {
            Debug.LogError("_playersTabContainer is not set");
        }
        if (_statsTabContainer == null)
        {
            Debug.LogError("_statsTabContainer is not set");
        }
        if (_optionsTabContainer == null)
        {
            Debug.LogError("_optionsTabContainer is not set");
        }

        _forestContainer.SetLocationType(LocationType.Forest);
        _marbleQuarryContainer.SetLocationType(LocationType.MarbleQuarry);
        _graniteQuarryContainer.SetLocationType(LocationType.GraniteQuarry);
        _constantinopleContainer.SetLocationType(LocationType.Constantinople);

        _constructionSite1Container.SetLocationType(LocationType.ConstructionSite1);
        _constructionSite2Container.SetLocationType(LocationType.ConstructionSite2);
        _constructionSite3Container.SetLocationType(LocationType.ConstructionSite3);
        _constructionSiteContainersByLocationType.Add(LocationType.ConstructionSite1, _constructionSite1Container);
        _constructionSiteContainersByLocationType.Add(LocationType.ConstructionSite2, _constructionSite2Container);
        _constructionSiteContainersByLocationType.Add(LocationType.ConstructionSite3, _constructionSite3Container);

        SetupTabContainers();
    }

    public void InitialiseLabourPools()
    {
        for (int i = 0; i < _initialForestLabourPoolSize; i++)
        {
            _forestContainer.GrowLabourPool();
        }
        for (int j = 0; j < _initialGraniteLabourpoolSize; j++)
        {
            _graniteQuarryContainer.GrowLabourPool();
        }
        for (int k = 0; k < _initialMarbleLabourPoolSize; k++)
        {
            _marbleQuarryContainer.GrowLabourPool();
        }
        for (int l = 0; l < _initialCityLabourPoolSize; l++)
        {
            _constantinopleContainer.GrowLabourPool();
        }

    }

    public void InitialiseTabButtons()
    {
        _gameTabButton.Initialise(_gameTabContainer);
        _playersTabButton.Initialise(_playersTabContainer);
        _statsTabButton.Initialise(_statsTabContainer);
        _optionsTabButton.Initialise(_optionsTabContainer);

        _playersTabButton.Deactivate();
        _statsTabButton.Deactivate();
        _optionsTabButton.Deactivate();
    }

    public void SetupTabContainers()
    {
        _gameTabContainer.Setup();
        _playersTabContainer.Setup();
        _statsTabContainer.Setup();
        _optionsTabContainer.Setup();
    }

    public void InitialiseTabContainers()
    {
        _gameTabContainer.Initialise();
        _playersTabContainer.Initialise();
        _statsTabContainer.Initialise();
        _optionsTabContainer.Initialise();
    }

    private void Start()
    {
        SetTab(_gameTabButton);
    }

    public void SetTab(UITabButton newTab)
    {
        if(CurrentTab == null)
        {
            CurrentTab = newTab;
            CurrentTab.Activate();
            return;
        }

        if (CurrentTab == newTab)
        {
            CurrentTab.Activate();
            return;
        }

        CurrentTab.Deactivate();
        CurrentTab = newTab;
        CurrentTab.Activate();
    }

    public UILocationContainer GetLocation(LocationType locationType)
    {
        switch (locationType)
        {
            case LocationType.Forest:
                return _forestContainer;
            case LocationType.MarbleQuarry:
                return _marbleQuarryContainer;
            case LocationType.GraniteQuarry:
                return _graniteQuarryContainer;
            case LocationType.Constantinople:
                return _constantinopleContainer;
            default:
                Debug.LogError($"Location type {locationType} was not yet implemented");
                return null;
        }
    }

    public MonumentLocationUIContainer GetMonumentLocationUIContainer(LocationType locationType)
    {
        if(_constructionSiteContainersByLocationType.TryGetValue(locationType, out MonumentLocationUIContainer constructionSiteContainer))
        {
            return constructionSiteContainer;
        }

        Debug.LogError($"Could not find constructionSiteContainer for location type {locationType}");
        return null;
    }

    public UITabContainer GetMainTabContainer(MainTabType mainTabType)
    {
        switch (mainTabType)
        {
            case MainTabType.GameTab:
                return _gameTabContainer;
            case MainTabType.PlayersTab:
                return _playersTabContainer;
            case MainTabType.StatsTab:
                return _statsTabContainer;
            case MainTabType.OptionsTab:
                return _optionsTabContainer;
            default:
                Debug.LogError($"Unknown main tab type {mainTabType}");
                return null;
        }
    }
}
