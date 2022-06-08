
using TMPro;
using UnityEngine;

public class MonumentComponentListItem : MonoBehaviour
{
    private MonumentComponentBlueprint _monumentComponentBlueprint;

    [SerializeField] private TextMeshProUGUI _componentNameLabel;

    public void Initialise(MonumentComponentBlueprint blueprint)
    {
        _monumentComponentBlueprint = blueprint;
        SetName(blueprint.Name);
        Debug.Log($"Initialise item");
    }

    private void SetName(string itemName)
    {
        _componentNameLabel.text = itemName;
    }
}
