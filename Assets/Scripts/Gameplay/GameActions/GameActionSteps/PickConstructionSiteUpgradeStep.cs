using System.Collections.Generic;
using UnityEngine;

public class PickConstructionSiteUpgradeStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;

    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private IConstructionSiteUpgrade _selectedUpgrade;

    private Dictionary<ConstructionSiteUpgradeType, GameActionConstructionSiteUpgradeSelectionTileElement> _upgradeTileByType = new Dictionary<ConstructionSiteUpgradeType, GameActionConstructionSiteUpgradeSelectionTileElement>();

    public PickConstructionSiteUpgradeStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public List<IGameActionElement> Initialise()
    {
        //_actionTileByActionType.Clear();

        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        Player gameActionInitiator = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;

        if (gameActionInitiator == null)
        {
            Debug.LogError($"We could not find out who initiated thsi game action, but we should be able to find a player");
        }

        // List here all the possible Upgrades
        
        AddConstructionSiteUpgradeElement(gameActionInitiator, gameActionInitiator.StockpileMaximum.GetNextUpgrade());


        // by default select first available tile
        foreach (KeyValuePair<ConstructionSiteUpgradeType, GameActionConstructionSiteUpgradeSelectionTileElement> item in _upgradeTileByType)
        {
            if (item.Value.IsAvailable)
            {
                _upgradeTileByType[item.Key].Select();
                _selectedUpgrade = item.Value.ConstructionSiteUpgrade;
                return _elements;
            }
        }

        Debug.LogWarning($"We have no available actions. We need a return scenario for this.");
        return _elements;
    }

    public void SelectConstructionSiteUpgrade(IConstructionSiteUpgrade constructionSiteUpgrade)
    {
        if (constructionSiteUpgrade.ConstructionSiteUpgradeType == _selectedUpgrade.ConstructionSiteUpgradeType) return;

        IConstructionSiteUpgrade previouslyUpgradeType = _selectedUpgrade;
        _upgradeTileByType[previouslyUpgradeType.ConstructionSiteUpgradeType].Deselect(); // Deselect the current

        _selectedUpgrade = constructionSiteUpgrade;
        _upgradeTileByType[_selectedUpgrade.ConstructionSiteUpgradeType].Select();
    }

    public void NextStep()
    {
        GameActionCheckSum checkSum = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum;
        UpgradeConstructionSiteGameAction gameAction = checkSum.GameAction as UpgradeConstructionSiteGameAction;

        if(gameAction == null)
        {
            Debug.LogError($"Could not parse the game action of type {checkSum.GameAction.GetType()} as a UpgradeConstructionSiteGameAction");
        }

        gameAction.WithUpgradeType(_selectedUpgrade.ConstructionSiteUpgradeType);

        GameActionStepHandler.CurrentGameActionSequence.AddStep(new CheckoutStep());

        LocationType constructionSiteLocation = checkSum.Player.Monument.ConstructionSite;
        checkSum.WithLocation(LocationManager.Instance.GetLocation(constructionSiteLocation));
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }

    private void AddConstructionSiteUpgradeElement(Player player, IConstructionSiteUpgrade constructionSiteUpgrade)
    {
        GameActionConstructionSiteUpgradeSelectionTileElement upgradeSelectionTileElement = GameActionElementInitialiser.InitialiseConstructionSiteUpgradeSelectionTile(this, constructionSiteUpgrade);
        bool isAvailable = true;

        if (!isAvailable)
        {
            upgradeSelectionTileElement.MakeUnavailable();
        }

        _upgradeTileByType.Add(constructionSiteUpgrade.ConstructionSiteUpgradeType, upgradeSelectionTileElement);
        _elements.Add(upgradeSelectionTileElement);
    }

    public string GetEffectDescription()
    {
        return _selectedUpgrade.GetEffectDescription();
    }
}
