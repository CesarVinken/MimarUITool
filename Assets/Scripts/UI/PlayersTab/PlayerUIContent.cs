using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIContent : MonoBehaviour
{
    [SerializeField] private AccumulativePlayerStatElementUIContainer _reputationContainer;
    [SerializeField] private AccumulativePlayerStatElementUIContainer _goldContainer;
    [SerializeField] private AccumulativePlayerStatElementUIContainer _woodResourceContainer;
    [SerializeField] private AccumulativePlayerStatElementUIContainer _marbleResourceContainer;
    [SerializeField] private AccumulativePlayerStatElementUIContainer _graniteResourceContainer;
    [SerializeField] private SingleValuedPlayerStatElementUIContainer _stockpileMaximumContainer;

    [SerializeField] private PlayersTabContainer _playersTabContainer;
    [SerializeField] private MonumentUIContainer _uiMonumentContainer;

    private Player _player;

    private void Awake()
    {
        if (_reputationContainer == null)
        {
            Debug.LogError($"could not find _reputationContainer");
        }
        if (_goldContainer == null)
        {
            Debug.LogError($"could not find _goldContainer");
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
        if (_stockpileMaximumContainer == null)
        {
            Debug.LogError($"could not find _stockpileContainer");
        }

        if (_uiMonumentContainer == null)
        {
            Debug.LogError($"could not find _uiMonumentContainer on {gameObject.name}");
        }
    }

    public void Initialise(PlayersTabContainer playersTabContainer)
    {
        _player = PlayerManager.Instance.Players[PlayerNumber.Player1];
        _playersTabContainer = playersTabContainer;

        _reputationContainer.Initialise(this, _player.Reputation);
        _goldContainer.Initialise(this, _player.Gold);
        _woodResourceContainer.Initialise(this, _player.Resources[ResourceType.Wood]);
        _marbleResourceContainer.Initialise(this, _player.Resources[ResourceType.Marble]);
        _graniteResourceContainer.Initialise(this, _player.Resources[ResourceType.Granite]);
        _stockpileMaximumContainer.Initialise(this, _player.StockpileMaximum);

        _uiMonumentContainer.Initialise(this, playersTabContainer);
        _uiMonumentContainer.GenerateItems();
    }

    public void UpdatePlayerUIContent(UIPlayerData uiPlayerData)
    {
        _player = uiPlayerData.Player;

        _reputationContainer.UpdateUI(_player, _player.Reputation); // after updating reputation, we need to reevaluate Gold as well
        _goldContainer.UpdateUI(_player, _player.Gold);
        _stockpileMaximumContainer.UpdateUI(_player, _player.StockpileMaximum);

        Dictionary<ResourceType, IResource> resources = _player.Resources;

        _woodResourceContainer.UpdateUI(_player, resources[ResourceType.Wood]);
        _marbleResourceContainer.UpdateUI(_player, resources[ResourceType.Marble]);
        _graniteResourceContainer.UpdateUI(_player, resources[ResourceType.Granite]);
    }

    private void OnReputationInputFieldChange(TMP_InputField inputField)
    {
        if (string.IsNullOrWhiteSpace(inputField.text) || int.Parse(inputField.text) == 0)
        {
            inputField.text = "0";
        }

        int oldReputation = _player.Reputation.Value;
        int newReputation = int.Parse(inputField.text);
        int amountCap = _player.Reputation.GetValueCap();

        if (newReputation > amountCap)
        {
            newReputation = amountCap;
            inputField.text = newReputation.ToString();
        }

        _player.SetReputation(newReputation);
        PlayerManager.Instance.UpdatePlayerPriority(_player, oldReputation);

        CalculateIncomeProjectionLabel(_goldContainer, _player.Gold);
    }

    public void SetPlayerStatInputFieldValue(TMP_InputField inputField, IPlayerStat playerStat)
    {
        if(playerStat is IAccumulativePlayerStat)
        {
            SetAccumulativePlayerStatInputFieldValue(inputField, playerStat as IAccumulativePlayerStat);
            return;
        }      
    }

    public void SetAccumulativePlayerStatInputFieldValue(TMP_InputField inputField, IAccumulativePlayerStat playerStat)
    {
        if (playerStat is Reputation)
        {
            OnReputationInputFieldChange(inputField);
            return;
        }
        if (string.IsNullOrWhiteSpace(inputField.text) || int.Parse(inputField.text) == 0)
        {
            inputField.text = "0";

            playerStat.SetValue(0);
            return;
        }

        int newAmount = int.Parse(inputField.text);
        int amountCap = playerStat.GetValueCap();

        if (newAmount > amountCap)
        {
            newAmount = amountCap;
            inputField.text = newAmount.ToString();
        }
        playerStat.SetValue(newAmount);
    }

    public void CalculateIncomeProjectionLabel(AccumulativePlayerStatElementUIContainer playerStatElementUIContainer, IAccumulativePlayerStat playerStat)
    {
        string projectionString = "";
        int projectedIncome;
        if (playerStat is IResource)
        {
            IResource resource = playerStat as IResource;
            ResourceType resourceType = resource.GetResourceType();
            projectedIncome = StatCalculator.CalculateResourceIncome(resourceType, _player);

            if (_player.Resources[resourceType].Value + projectedIncome > playerStat.GetValueCap())
            {
                projectionString = $"<color=red>+{projectedIncome}</color>";
            }
            else
            {
                projectionString = $"+{projectedIncome}";
            }
        }
        else if(playerStat is Gold)
        {
            projectedIncome = StatCalculator.CalculateGoldIncome(_player);
            projectionString = $"+{projectedIncome}";
        }

        playerStatElementUIContainer.SetIncomeProjectionLabel(projectionString);
    }
}
