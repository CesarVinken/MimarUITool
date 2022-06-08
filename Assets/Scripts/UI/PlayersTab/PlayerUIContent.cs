using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIContent : MonoBehaviour
{
    [SerializeField] private TMP_InputField _reputationInputField;
    [SerializeField] private TMP_InputField _currentGoldInputField;
    [SerializeField] private TextMeshProUGUI _goldIncomeProjectionLabel;
    [SerializeField] private TMP_InputField _currentStockpileInputField;

    [SerializeField] private PlayerResourceUIContainer _woodResourceContainer;
    [SerializeField] private PlayerResourceUIContainer _marbleResourceContainer;
    [SerializeField] private PlayerResourceUIContainer _graniteResourceContainer;

    [SerializeField] private UIMonumentContainer _uiMonumentContainer;

    private Player _player;

    private void Awake()
    {
        if (_reputationInputField == null)
        {
            Debug.LogError($"could not find reputationInputField");
        }
        if (_currentGoldInputField == null)
        {
            Debug.LogError($"could not find _currentGoldInputField");
        }
        if (_goldIncomeProjectionLabel == null)
        {
            Debug.LogError($"could not find _goldIncomeProjectionLabel");
        }
        if (_currentStockpileInputField == null)
        {
            Debug.LogError($"could not find _currentStockpileInputField");
        }

        if (_woodResourceContainer == null)
        {
            Debug.LogError($"could not find _woodResourceContainer");
        }
        if (_marbleResourceContainer == null)
        {
            Debug.LogError($"could not find _marbleResourceContainer");
        }
        if (_graniteResourceContainer == null)
        {
            Debug.LogError($"could not find _graniteResourceContainer");
        }

        if (_uiMonumentContainer == null)
        {
            Debug.LogError($"could not find _uiMonumentContainer on {gameObject.name}");
        }
    }

    public void Initialise()
    {
        _woodResourceContainer.Initialise(ResourceType.Wood);
        _marbleResourceContainer.Initialise(ResourceType.Marble);
        _graniteResourceContainer.Initialise(ResourceType.Granite);

        _uiMonumentContainer.GenerateItems();
    }

    public void UpdatePlayerUIContent(UIPlayerData uiPlayerData)
    {
        _player = uiPlayerData.Player;

        SetReputationUI(_player.Reputation); // TODO MOVE TO DEDICATED CONTAINERS LIKE RESOURCES
        SetGoldUI(_player.Gold);
        SetResourcesUI(_player.Resources);
        SetStockpileMaximumUI(_player.StockpileMaximum);

        _uiMonumentContainer.UpdateUIForItems();
    }

    public void SetReputationUI(int newReputation)
    {
        _reputationInputField.text = newReputation.ToString();

        int recalculatedIncome = StatCalculator.CalculateGoldIncome(_player);
        _goldIncomeProjectionLabel.text = $"+{recalculatedIncome.ToString()}";
    }

    public void SetResourcesUI(Dictionary<ResourceType, IResource> resources)
    {
        _woodResourceContainer.UpdateUI(_player, resources[ResourceType.Wood]);
        _marbleResourceContainer.UpdateUI(_player, resources[ResourceType.Marble]);
        _graniteResourceContainer.UpdateUI(_player, resources[ResourceType.Granite]);
    }

    public void OnReputationInputFieldChange()
    {
        if (string.IsNullOrWhiteSpace(_reputationInputField.text) || int.Parse(_reputationInputField.text) == 0)
        {
            _reputationInputField.text = "0";
        }

        int oldReputation = _player.Reputation;
        int newReputation = int.Parse(_reputationInputField.text);
        _player.SetReputation(newReputation);
        PlayerManager.Instance.UpdatePlayerPriority(_player, oldReputation);

        int recalculatedIncome = StatCalculator.CalculateGoldIncome(_player);
        _goldIncomeProjectionLabel.text = $"+{recalculatedIncome.ToString()}";
    }

    public void SetGoldUI(int newGold)
    {
        _currentGoldInputField.text = newGold.ToString();
    }

    public void OnGoldInputFieldChange()
    {
        if (string.IsNullOrWhiteSpace(_currentGoldInputField.text) || int.Parse(_currentGoldInputField.text) == 0)
        {
            _currentGoldInputField.text = "0";
        }
        
        int newGold = int.Parse(_currentGoldInputField.text);
        int goldCap = 199;
        if(newGold > goldCap)
        {
            newGold = goldCap;
        }

        _player.SetGold(newGold);
    }

    public void SetStockpileMaximumUI(int newMaximum)
    {
        _currentStockpileInputField.text = newMaximum.ToString();
    }

    public void OnStockpileMaximumInputFieldChange()
    {
        if (string.IsNullOrWhiteSpace(_currentStockpileInputField.text) || int.Parse(_currentStockpileInputField.text) == 0)
        {
            _currentStockpileInputField.text = "0";
        }
        
        int newMaximum = int.Parse(_currentStockpileInputField.text);
        _player.SetStockpileMaximum(newMaximum);
    }
}
