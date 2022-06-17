using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIContent : MonoBehaviour
{
    [SerializeField] private PlayerStatElementUIContainer _reputationContainer;
    [SerializeField] private PlayerStatElementUIContainer _goldContainer;
    [SerializeField] private PlayerStatElementUIContainer _woodResourceContainer;
    [SerializeField] private PlayerStatElementUIContainer _marbleResourceContainer;
    [SerializeField] private PlayerStatElementUIContainer _graniteResourceContainer;
    [SerializeField] private PlayerStatElementUIContainer _stockpileMaximumContainer;

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

        _woodResourceContainer.UpdateUI(_player, resources[ResourceType.Wood] as IPlayerStat);
        _marbleResourceContainer.UpdateUI(_player, resources[ResourceType.Marble] as IPlayerStat);
        _graniteResourceContainer.UpdateUI(_player, resources[ResourceType.Granite] as IPlayerStat);
    }

    private void OnReputationInputFieldChange(TMP_InputField inputField)
    {
        if (string.IsNullOrWhiteSpace(inputField.text) || int.Parse(inputField.text) == 0)
        {
            inputField.text = "0";
        }

        int oldReputation = _player.Reputation.Amount;
        int newReputation = int.Parse(inputField.text);
        int amountCap = _player.Reputation.GetAmountCap();

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
        if(playerStat is Reputation)
        {
            OnReputationInputFieldChange(inputField);
            return;
        }
        if (string.IsNullOrWhiteSpace(inputField.text) || int.Parse(inputField.text) == 0)
        {
            inputField.text = "0";

            playerStat.SetAmount(0);
            return;
        }

        int newAmount = int.Parse(inputField.text);
        int amountCap = playerStat.GetAmountCap();

        if (newAmount > amountCap)
        {
            newAmount = amountCap;
            inputField.text = newAmount.ToString();
        }
        playerStat.SetAmount(newAmount);
    }

    public void CalculateIncomeProjectionLabel(PlayerStatElementUIContainer playerStatElementUIContainer, IPlayerStat playerStat)
    {
        string projectionString = "";
        int projectedIncome;
        if (playerStat is IResource)
        {
            IResource resource = playerStat as IResource;
            ResourceType resourceType = resource.GetResourceType();
            projectedIncome = StatCalculator.CalculateResourceIncome(resourceType, _player);

            if (_player.Resources[resourceType].Amount + projectedIncome > playerStat.GetAmountCap())
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
