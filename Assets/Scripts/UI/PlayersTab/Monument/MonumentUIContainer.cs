using System.Collections.Generic;
using UnityEngine;

public class MonumentUIContainer : MonoBehaviour
{
    [SerializeField] private GameObject _monumentComponentListItemPrefab;
    [SerializeField] private Transform _monumentComponentListContainer;
    private PlayerUIContent _playerUIContentContainer;
    private PlayersTabContainer _playersTabContainer;

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

    public void Initialise(PlayerUIContent playerUIContentContainer, PlayersTabContainer playersTabContainer)
    {
        _playerUIContentContainer = playerUIContentContainer;
        _playersTabContainer = playersTabContainer;
    }

    // Items are generated only the first time we run the game. After that we use the list of the generated items and turn them on and off as needed using UpdateUIForItems()
    public void GenerateItems()
    {
        List<MonumentComponentBlueprint> monumentComponentBlueprints = Monument.DefaultMonumentBlueprints;

        for (int i = 0; i < monumentComponentBlueprints.Count; i++)
        {
            GameObject listItemGO = GameObject.Instantiate(_monumentComponentListItemPrefab, _monumentComponentListContainer);
            MonumentComponentListItem monumentComponentListItem = listItemGO.GetComponent<MonumentComponentListItem>();

            monumentComponentListItem.Initialise(monumentComponentBlueprints[i], _playersTabContainer);
            MonumentComponentListItems.Add(monumentComponentListItem);
        }
    }

    // When we select a player, we need to update to show the correct button state for all the items so that it fits that player
    public void UpdateUIForItems(Monument monument)
    {
        Debug.Log($"possibly we need to update ui button for new states here");

        for (int i = 0; i < MonumentComponentListItems.Count; i++)
        {
            MonumentComponentListItems[i].UpdateUIForButtonState(monument);
        }
    }
}
