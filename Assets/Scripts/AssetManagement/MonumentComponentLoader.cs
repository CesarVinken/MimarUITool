using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MonumentComponentLoader : IAddressableAssetLoader<MonumentComponentLoader>
{
    private AssetReference _prefab;
    private Transform _parentTransform;
    private MonumentComponentType _monumentComponentType;
    private MonumentDisplay _monumentDisplay;

    public MonumentComponentLoader(MonumentComponentType monumentComponentType)
    {
        _monumentComponentType = monumentComponentType;
    }

    public void InstantiateComponent()
    {
        if (_prefab == null)
        {
            Debug.LogError($"Cannot find prefab for {_monumentComponentType}");
            return;
        }

        Addressables.InstantiateAsync(_prefab, _parentTransform.position, Quaternion.Euler(-90, 0, 0), _parentTransform).Completed += OnAssetLoaded;
    }

    public void OnAssetLoaded(AsyncOperationHandle<GameObject> handle)
    {
        GameObject componentGO = handle.Result;

        MonumentDisplayComponent monumentDisplayComponent = componentGO.GetComponent<MonumentDisplayComponent>();
        monumentDisplayComponent.Initialise();
        monumentDisplayComponent.SetMonumentComponentType(_monumentComponentType);

        _monumentDisplay.OnMonumentComponentAssetLoaded();
    }

    public MonumentComponentLoader WithPrefab(AssetReference assetPrefab)
    {
        _prefab = assetPrefab;
        return this;
    }

    public MonumentComponentLoader WithParent(Transform parentTransform)
    {
        _parentTransform = parentTransform;
        return this;
    }

    public MonumentComponentLoader WithMonumentDisplay(MonumentDisplay monumentDisplay)
    {
        _monumentDisplay = monumentDisplay;
        return this;
    }
}
