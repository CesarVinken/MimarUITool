using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    public GameObject ResourcesWorkerPrefab;
    public GameObject CityWorkerPrefab;

    [Header("Monument component prefabs")]

    [SerializeField] private GameObject _floorFirstMonumentPrefab;// TODO: Use different way of resource loading such as addressables
    [SerializeField] private GameObject _floorSecondMonumentPrefab;
    [SerializeField] private GameObject _floorThirdMonumentPrefab;
    [SerializeField] private GameObject _archesMonumentPrefab;
    [SerializeField] private GameObject _domeMonumentPrefab;
    [SerializeField] private GameObject _groundPlaneMonumentPrefab;
    [SerializeField] private GameObject _outerWallsMonumentPrefab;
    [SerializeField] private GameObject _towersBackMonumentPrefab;
    [SerializeField] private GameObject _towersMiddleMonumentPrefab;
    [SerializeField] private GameObject _towersFrontMonumentPrefab;
    public void Awake()
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

    public GameObject GetMonumentComponentPrefab(MonumentComponentType monumentComponentType)
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
}
