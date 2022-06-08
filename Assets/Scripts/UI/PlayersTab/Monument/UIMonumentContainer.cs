using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonumentContainer : MonoBehaviour
{
    [SerializeField] private GameObject _monumentComponentListItemPrefab;
    [SerializeField] private Transform _monumentComponentListContainer;

    List<MonumentComponentListItem> MonumentComponentListItems = new List<MonumentComponentListItem>();

    private void Awake()
    {
        if (_monumentComponentListItemPrefab == null)
        {
            Debug.LogError($"Could not find MonumentComponentListItemPrefab on {gameObject.name}");
        }
        if (_monumentComponentListContainer == null)
        {
            Debug.LogError($"Could not find _monumentComponentListContainer on {gameObject.name}");
        }
    }

    // Items are generated only the first time we run the game. After that we use the list of the generated items and turn them on and off as needed using UpdateUIForItems()
    public void GenerateItems()
    {
        List<MonumentComponentBlueprint> monumentComponentBlueprints = Monument.GetDefaultMonumentBlueprints();

        for (int i = 0; i < monumentComponentBlueprints.Count; i++)
        {
            GameObject listItemGO = GameObject.Instantiate(_monumentComponentListItemPrefab, _monumentComponentListContainer);
            MonumentComponentListItem monumentComponentListItem = listItemGO.GetComponent<MonumentComponentListItem>();

            monumentComponentListItem.Initialise(monumentComponentBlueprints[i]);
            MonumentComponentListItems.Add(monumentComponentListItem);
        }
    }

    public void UpdateUIForItems()
    {
        Debug.Log($"Update ui for monument components list");
    }
}
