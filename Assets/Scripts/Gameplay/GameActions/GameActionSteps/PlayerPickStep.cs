using System.Collections.Generic;

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

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        GameActionPlayerSelectionTileElement player1TileElement = GameActionElementInitialiser.InitialisePlayerSelectionTile(this) as GameActionPlayerSelectionTileElement;
        _playerTileByPlayerNumber.Add(player1TileElement.PlayerNumber, player1TileElement);
        _elements.Add(player1TileElement);
        GameActionPlayerSelectionTileElement player2TileElement = GameActionElementInitialiser.InitialisePlayerSelectionTile(this) as GameActionPlayerSelectionTileElement;
        _playerTileByPlayerNumber.Add(player2TileElement.PlayerNumber, player2TileElement);
        _elements.Add(player2TileElement);
        GameActionPlayerSelectionTileElement player3TileElement = GameActionElementInitialiser.InitialisePlayerSelectionTile(this) as GameActionPlayerSelectionTileElement;
        _playerTileByPlayerNumber.Add(player3TileElement.PlayerNumber, player3TileElement);
        _elements.Add(player3TileElement);

        _playerTileByPlayerNumber[PlayerNumber.Player1].Select();

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
        GameActionHandler.CurrentUIGameToolAction.GameActionCheckSum.WithPlayer(player);

        GameActionHandler.CurrentUIGameToolAction.NextStep();
    }
}

