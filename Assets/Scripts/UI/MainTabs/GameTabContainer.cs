using UnityEngine;

public class GameTabContainer : UITabContainer
{
    [SerializeField] private PlayerPriorityContainer _playerPriorityContainer;
    public override MainTabType MainTabType { get; } = MainTabType.GameTab;
    [SerializeField] private NextMoveButton _nextTurnButton;

    private void Awake()
    {
        if(_nextTurnButton == null)
        {
            Debug.LogError($"Cannot find next turn button");
        }
    }

    public NextMoveButton GetNextMoveButton()
    {
        return _nextTurnButton;
    }

    public void Setup()
    {
        if(_playerPriorityContainer == null)
        {
            Debug.LogError($"Could not find _playerPriorityContainer on {gameObject.name}");
        }
    }

    public void Initialise()
    {
        _playerPriorityContainer.Initialise();
    }

    public override void Activate()
    {
        UpdatePlayerPriorityUI();
        base.Activate();
    }

    public void UpdatePlayerPriorityUI()
    {
        _playerPriorityContainer.UpdatePlayerPriorityTiles();
    }


    public void RefreshPlayerMoves()
    {
        _playerPriorityContainer.RefreshPlayerMovesUI();
    }

    public void UpdatePlayerMove(Player player, bool canMove)
    {
        _playerPriorityContainer.UpdatePlayerMoveUI(player, canMove);
    }
}
