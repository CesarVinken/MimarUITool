using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    public GameObject ResourcesWorkerPrefab;
    public GameObject CityWorkerPrefab;

    [Header("Monument component prefabs")]

    [SerializeField] private GameObject _firstFloorMonumentPrefab;

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

        if (_firstFloorMonumentPrefab == null)
        {
            Debug.LogError($"Could not find firstFloorMonumentPrefab");
        }

        Instance = this;
    }

    public GameObject GetMonumentComponentPrefab(MonumentComponentType monumentComponentType)
    {
        switch (monumentComponentType)
        {
            case MonumentComponentType.FirstFloor:
                return _firstFloorMonumentPrefab;
            //case MonumentComponentType.SecondFloor:
            //    break;
            //case MonumentComponentType.ThirdFloor:
            //    break;
            //case MonumentComponentType.Arches:
            //    break;
            //case MonumentComponentType.Dome:
            //    break;
            //case MonumentComponentType.GroundPlane:
            //    break;
            //case MonumentComponentType.OuterWalls:
            //    break;
            //case MonumentComponentType.TowerFront:
            //    break;
            //case MonumentComponentType.TowersBack:
            //    break;
            //case MonumentComponentType.TowersMiddle:
            //    break;
            default:
                Debug.LogError($"No prefab was implemented for monumentComponentType {monumentComponentType}");
                return null;
        }
    }
}
