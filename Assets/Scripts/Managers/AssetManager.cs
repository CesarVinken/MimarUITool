using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    public GameObject ResourcesWorkerPrefab;
    public GameObject CityWorkerPrefab;

    [Header("Monument component prefabs")]

    [SerializeField] private List<AssetReference> _monumentComponentPrefabs;

    [SerializeField] private AssetReference _floorFirstMonumentPrefab;// TODO: Use different way of resource loading such as addressables
    [SerializeField] private AssetReference _floorSecondMonumentPrefab;
    [SerializeField] private AssetReference _floorThirdMonumentPrefab;
    [SerializeField] private AssetReference _archesMonumentPrefab;
    [SerializeField] private AssetReference _domeMonumentPrefab;
    [SerializeField] private AssetReference _groundPlaneMonumentPrefab;
    [SerializeField] private AssetReference _outerWallsMonumentPrefab;
    [SerializeField] private AssetReference _towersBackMonumentPrefab;
    [SerializeField] private AssetReference _towersMiddleMonumentPrefab;
    [SerializeField] private AssetReference _towersFrontMonumentPrefab;

    [Header("Icons")]

    [SerializeField] private Sprite _lockIcon;
    [SerializeField] private Sprite _cogIcon;

    public void Setup()
    {
        if (ResourcesWorkerPrefab == null)
        {
            Debug.LogError($"Could not find ResourcesWorkerPrefab");
        }
        if (CityWorkerPrefab == null)
        {
            Debug.LogError($"Could not find CityWorkerPrefab");
        }

        if (_floorFirstMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _floorFirstMonumentPrefab");
        }
        if (_floorSecondMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _floorSecondMonumentPrefab");
        }
        if (_floorThirdMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _floorThirdMonumentPrefab");
        }
        if (_archesMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _archesMonumentPrefab");
        }
        if (_domeMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _domeMonumentPrefab");
        }
        if (_groundPlaneMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _groundPlaneMonumentPrefab");
        }
        if (_outerWallsMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _outerWallsMonumentPrefab");
        }
        if (_towersBackMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _towersBackMonumentPrefab");
        }
        if (_towersMiddleMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _towersMiddleMonumentPrefab");
        }
        if (_towersFrontMonumentPrefab == null)
        {
            Debug.LogError($"Could not find _towersFrontMonumentPrefab");
        }

        Instance = this;
    }

    private AssetReference GetMonumentComponentPrefab(MonumentComponentType monumentComponentType)
    {
        switch (monumentComponentType)
        {
            case MonumentComponentType.FloorFirst:
                return _floorFirstMonumentPrefab;
            case MonumentComponentType.FloorSecond:
                return _floorSecondMonumentPrefab;
            case MonumentComponentType.FloorThird:
                return _floorThirdMonumentPrefab;
            case MonumentComponentType.Arches:
                return _archesMonumentPrefab;
            case MonumentComponentType.Dome:
                return _domeMonumentPrefab;
            case MonumentComponentType.GroundPlane:
                return _groundPlaneMonumentPrefab;
            case MonumentComponentType.OuterWalls:
                return _outerWallsMonumentPrefab;
            case MonumentComponentType.TowersFront:
                return _towersFrontMonumentPrefab;
            case MonumentComponentType.TowersBack:
                return _towersBackMonumentPrefab;
            case MonumentComponentType.TowersMiddle:
                return _towersMiddleMonumentPrefab;
            default:
                Debug.LogError($"No prefab was implemented for monumentComponentType {monumentComponentType}");
                return null;
        }
    }
    
    public Sprite GetMonumentComponentListItemIcon(MonumentComponentDisplayButtonState monumentComponentDisplayButtonState)
    {
        if(monumentComponentDisplayButtonState is MonumentComponentDisplayButtonLockedState)
        {
            return _lockIcon;
        }
        if(monumentComponentDisplayButtonState is MonumentComponentDisplayButtonInProgressState)
        {
            return _cogIcon;
        }
        return null;
    }

    public string GetPlayerStatInlineIcon(IPlayerStat playerStat)
    {
        if (playerStat is Gold)
        {
            return "<sprite=\"Placeholder\" index=1>";
        }
        if (playerStat is Reputation)
        {
            return "<sprite=\"Placeholder\" index=2>";
        }
        if (playerStat is Wood)
        {
            return "<sprite=\"Placeholder\" index=3>";
        }
        if (playerStat is Granite)
        {
            return "<sprite=\"Placeholder\" index=4>";
        }
        if (playerStat is Marble)
        {
            return "<sprite=\"Placeholder\" index=5>";
        }
        return "UNKNOWN ICON";
    }

    public void InstantiateMonumentComponent(MonumentDisplay monumentDisplay, MonumentComponentType monumentComponentType)
    {
        AssetReference prefab = GetMonumentComponentPrefab(monumentComponentType);
        MonumentComponentLoader monumentComponentLoader = new MonumentComponentLoader(
            monumentComponentType)
            .WithPrefab(prefab)
            .WithParent(monumentDisplay.transform)
            .WithMonumentDisplay(monumentDisplay);
        monumentComponentLoader.InstantiateComponent();
    }
}

