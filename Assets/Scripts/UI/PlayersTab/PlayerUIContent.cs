using TMPro;
using UnityEngine;

public class PlayerUIContent : MonoBehaviour
{
    [SerializeField] private TMP_InputField _reputationInputField;
    [SerializeField] private TMP_InputField _currentGoldInputField;
    [SerializeField] private TextMeshProUGUI _incomeLabel;
    [SerializeField] private TMP_InputField _currentStockpileInputField;

    [SerializeField] private PlayerResourceUIContainer _woodResourceContainer;
    [SerializeField] private PlayerResourceUIContainer _marbleResourceContainer;
    [SerializeField] private PlayerResourceUIContainer _graniteResourceContainer;

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
        if (_incomeLabel == null)
        {
            Debug.LogError($"could not find _incomeLabel");
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
    }

    public void Initialise()
    {
        _woodResourceContainer.Initialise(ResourceType.Wood);
        _marbleResourceContainer.Initialise(ResourceType.Marble);
        _graniteResourceContainer.Initialise(ResourceType.Granite);
    }

    public void FillInPlayerContent(UIPlayerData uiPlayerData)
    {
        _player = uiPlayerData.Player;

        SetReputation(uiPlayerData.Reputation); // TODO MOVE TO DEDICATED CONTAINERS LIKE RESOURCES
        SetGold(uiPlayerData.Gold);
        SetStockpileMaximum(uiPlayerData.StockpileMaximum);

        _woodResourceContainer.FillInPlayerContent(uiPlayerData);
        _marbleResourceContainer.FillInPlayerContent(uiPlayerData);
        _graniteResourceContainer.FillInPlayerContent(uiPlayerData);
    }

    public void SetReputation(int newReputation)
    {
        _reputationInputField.text = newReputation.ToString();

        int recalculatedIncome = StatCalculator.CalculateIncome(_player);
        _incomeLabel.text = $"+({recalculatedIncome.ToString()})";
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

        int recalculatedIncome = StatCalculator.CalculateIncome(_player);
        _incomeLabel.text = $"+{recalculatedIncome.ToString()}";
    }

    public void SetGold(int newGold)
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

    public void SetStockpileMaximum(int newMaximum)
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
