
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonumentComponentListItem : MonoBehaviour
{
    private MonumentComponentBlueprint _monumentComponentBlueprint;
    [SerializeField] Button _button; 

    [SerializeField] private TextMeshProUGUI _componentNameLabel;
    private PlayersTabContainer _playersTabContainer;

    private void Awake()
    {
        if(_button == null)
        {
            Debug.LogError($"could not find _button on {gameObject.name}");
        }
    }

    private void Start()
    {
        _button.onClick.AddListener(delegate { ToggleComponentCompleted(); });
    }
    public void Initialise(MonumentComponentBlueprint blueprint, PlayersTabContainer playersTabContainer)
    {
        _monumentComponentBlueprint = blueprint;
        _playersTabContainer = playersTabContainer;
        SetName(blueprint.Name);
        Debug.Log($"Initialise item");
    }

    private void SetName(string itemName)
    {
        _componentNameLabel.text = itemName;
    }

    // force-update the completion status of a monument component
    private void ToggleComponentCompleted()
    {
        bool isCompleted = _playersTabContainer.HandleMonumentComponentCompletion(_monumentComponentBlueprint);

        //currentPlayer.HandleMonumentComponentCompletion(monumentComponentBlueprint);
    }
}
