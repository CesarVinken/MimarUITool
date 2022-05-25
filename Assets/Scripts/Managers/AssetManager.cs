using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    public GameObject ResourcesWorkerPrefab;
    public GameObject CityWorkerPrefab;

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

        Instance = this;
    }
}
