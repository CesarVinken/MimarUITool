using System.Collections.Generic;

public class PlayerPickStep : IUIToolGameActionStep, IUIPlayerSelectionGameActionStep
{
    public int StepNumber { get; private set; } = -1;

    private List<IUIToolGameActionElement> _elements = new List<IUIToolGameActionElement>();
    private PlayerNumber _selectedPlayer;

    private Dictionary<PlayerNumber, GameActionPlayerSelectionTileElement> _playerTileByPlayerNumber = new Dictionary<PlayerNumber, GameActionPlayerSelectionTileElement>();

    public PlayerPickStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IUIToolGameActionElement> Initialise()
    {
        _playerTileByPlayerNumber.Clear();

        IUIToolGameActionElement stepLabelElement = GameActionElementInitaliser.InitialiseLabel(this);
        _elements.Add(stepLabelElement);

        IUIToolGameActionElement nextStepButtonElement = GameActionElementInitaliser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        GameActionPlayerSelectionTileElement player1TileElement = GameActionElementInitaliser.InitialisePlayerSelectionTile(this) as GameActionPlayerSelectionTileElement;
        _playerTileByPlayerNumber.Add(player1TileElement.PlayerNumber, player1TileElement);
        _elements.Add(player1TileElement);
        GameActionPlayerSelectionTileElement player2TileElement = GameActionElementInitaliser.InitialisePlayerSelectionTile(this) as GameActionPlayerSelectionTileElement;
        _playerTileByPlayerNumber.Add(player2TileElement.PlayerNumber, player2TileElement);
        _elements.Add(player2TileElement);
        GameActionPlayerSelectionTileElement player3TileElement = GameActionElementInitaliser.InitialisePlayerSelectionTile(this) as GameActionPlayerSelectionTileElement;
        _playerTileByPlayerNumber.Add(player3TileElement.PlayerNumber, player3TileElement);
        _elements.Add(player3TileElement);

        _playerTileByPlayerNumber[PlayerNumber.Player1].Select();

        return _elements;
    }

    public List<IUIToolGameActionElement> GetUIElements()
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
        UIToolGameActionHandler.CurrentUIGameToolAction.GameActionCheckSum.WithPlayer(player);

        UIToolGameActionHandler.CurrentUIGameToolAction.NextStep();
    }
}

