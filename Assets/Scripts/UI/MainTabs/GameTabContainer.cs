
using UnityEngine;

public class GameTabContainer : UITabContainer
{
    [SerializeField] private PlayerPriorityContainer _playerPriorityContainer;
    public override MainTabType MainTabType { get; } = MainTabType.GameTab;

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
}
