using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionConstructionSiteUpgradeSelectionTileElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;
    private PickConstructionSiteUpgradeStep _upgradeConstructionSitePickStep;

    public IConstructionSiteUpgrade ConstructionSiteUpgrade { get; private set; }

    public bool IsAvailable { get; private set; } = true;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void Awake()
    {
        if (_button == null)
        {
            Debug.LogError($"could not find button on {gameObject.name}");
        }
        if (_buttonLabel == null)
        {
            Debug.LogError($"could not find buttonLabel on {gameObject.name}");
        }
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void SetUp(IConstructionSiteUpgrade constructionSiteUpgrade, PickConstructionSiteUpgradeStep upgradeConstructionSitePickStep)
    {
        ConstructionSiteUpgrade = constructionSiteUpgrade;
        _upgradeConstructionSitePickStep = upgradeConstructionSitePickStep;
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        _buttonLabel.text = $"{ConstructionSiteUpgrade.Name}";
    }

    private void OnClick()
    {
        _upgradeConstructionSitePickStep.SelectConstructionSiteUpgrade(ConstructionSiteUpgrade);
    }

    public void Select()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
    }

    public void Deselect()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.Empty);
    }

    public void MakeUnavailable()
    {
        _button.interactable = false;
        _button.image.color = ColourUtility.GetColour(ColourType.GrayedOut);
        IsAvailable = false;
    }

    public void MakeAvailable()
    {
        _button.interactable = true;
        IsAvailable = true;
    }
}
