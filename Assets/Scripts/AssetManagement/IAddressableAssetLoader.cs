using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public interface IAddressableAssetLoader<T>
{
    T WithPrefab(AssetReference prefab);
    T WithParent(Transform parentTransform);

    void InstantiateComponent();
    void OnAssetLoaded(AsyncOperationHandle<GameObject> handle);
}