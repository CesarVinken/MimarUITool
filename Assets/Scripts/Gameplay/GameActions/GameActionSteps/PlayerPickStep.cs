using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPickStep : IGameActionStep, IUIPlayerSelectionGameActionStep
{
    public int StepNumber { get; private set; } = -1;

    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private PlayerNumber _selectedPlayer;

    private Dictionary<PlayerNumber, GameActionPlayerSelectionTileElement> _playerTileByPlayerNumber = new Dictionary<PlayerNumber, GameActionPlayerSelectionTileElement>();

    public PlayerPickStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> Initialise()
    {
        _playerTileByPlayerNumber.Clear();

        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        List<Player> players = PlayerManager.Instance.PlayersByPriority;
        List<Player> playersThatCanMove = players.Where(p => p.CanMove).ToList();

        if (playersThatCanMove.Count == 0)
        {
            IGameActionElement labelElement = GameActionElementInitialiser.InitialiseMainContentLabel(this, "All players have moved. Go the the next turn.");
            _elements.Add(labelElement);
            return _elements;
        }

        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];

            GameActionPlayerSelectionTileElement tileElement = GameActionElementInitialiser.InitialisePlayerSelectionTile(this, player) as GameActionPlayerSelectionTileElement;

            if (!player.CanMove)
            {
                tileElement.Deactivate();
            }

            _playerTileByPlayerNumber.Add(player.PlayerNumber, tileElement);
            _elements.Add(tileElement);
        }

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        _selectedPlayer = playersThatCanMove[0].PlayerNumber;
        _playerTileByPlayerNumber[_selectedPlayer].Select();

        return _elements;
    }

    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public void SelectPlayer(PlayerNumber playerNumber)
    {
        if (playerNumber == _selectedPlayer) return;

        PlayerNumber previouslySelectedPlayer = _selectedPlayer;
        _playerTileByPlayerNumber[previouslySelectedPlayer].Deselect(); // Deselect the current

        _selectedPlayer = playerNumber;
        _playerTileByPlayerNumber[_selectedPlayer].Select();
    }

    public void NextStep()
    {
        Player player = PlayerManager.Instance.Players[_selectedPlayer];
        GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.WithPlayer(player);

        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }
}

