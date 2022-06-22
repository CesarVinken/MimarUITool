using System.Collections.Generic;
using UnityEngine;

public class MonumentsDisplayContainer : MonoBehaviour
{
    [SerializeField] private MonumentDisplay _player1Monument;
    [SerializeField] private MonumentDisplay _player2Monument;
    [SerializeField] private MonumentDisplay _player3Monument;

    private Dictionary<PlayerNumber, MonumentDisplay> _monumentsByPlayerNumber = new Dictionary<PlayerNumber, MonumentDisplay>();
    private MonumentDisplay _currentlyShownMonument = null;

    public void Setup()
    {
        if (_player1Monument == null)
        {
            Debug.LogError($"Cannot find player1Monument on {gameObject.name}");
        }
        if (_player2Monument == null)
        {
            Debug.LogError($"Cannot find _player2Monument on {gameObject.name}");
        }
        if (_player3Monument == null)
        {
            Debug.LogError($"Cannot find _player3Monument on {gameObject.name}");
        }

        _monumentsByPlayerNumber.Add(PlayerNumber.Player1, _player1Monument);
        _monumentsByPlayerNumber.Add(PlayerNumber.Player2, _player2Monument);
        _monumentsByPlayerNumber.Add(PlayerNumber.Player3, _player3Monument);
    }


    public void Initialise()
    {
        _player1Monument.SetPlayer(PlayerManager.Instance.Players[PlayerNumber.Player1]);
        _player2Monument.SetPlayer(PlayerManager.Instance.Players[PlayerNumber.Player2]);
        _player3Monument.SetPlayer(PlayerManager.Instance.Players[PlayerNumber.Player3]);

        foreach (KeyValuePair<PlayerNumber, MonumentDisplay> item in _monumentsByPlayerNumber)
        {
            item.Value.CreateMonumentComponents();
        }
    }

    public void ShowMonument(PlayerNumber playerNumber)
    {
        if(_currentlyShownMonument != null)
        {
            if(NavigationManager.Instance.CurrentTab.TabContainer.MainTabType == MainTabType.PlayersTab && _currentlyShownMonument.Player.PlayerNumber == playerNumber)
            {
                return;
            }

            _currentlyShownMonument.gameObject.SetActive(false);
        }

        _monumentsByPlayerNumber[playerNumber].UpdateComponentsVisibility();

        _monumentsByPlayerNumber[playerNumber].gameObject.SetActive(true);
        _currentlyShownMonument = _monumentsByPlayerNumber[playerNumber];
    }

    public void UpdateVisibilityForComponents(Monument monument)
    {
        List<MonumentComponent> monumentComponents = monument.GetMonumentComponents();

        for (int i = 0; i < monumentComponents.Count; i++)
        {
            MonumentComponent monumentComponent = monumentComponents[i];
            UpdateVisibilityForComponent(monumentComponent);
        }
    }

    public void UpdateVisibilityForComponent(MonumentComponent monumentComponent)
    {
        MonumentComponentState state = monumentComponent.State;
        if (state == MonumentComponentState.Complete)
        {
            SetMonumentComponentVisibility(monumentComponent.PlayerNumber, monumentComponent, MonumentComponentVisibility.Complete);
        }
        else if (state == MonumentComponentState.InProgress)
        {
            SetMonumentComponentVisibility(monumentComponent.PlayerNumber, monumentComponent, MonumentComponentVisibility.InProgress);
        }
        else
        {
            SetMonumentComponentVisibility(monumentComponent.PlayerNumber, monumentComponent, MonumentComponentVisibility.Hidden);
        }
    }

    public void SetMonumentComponentVisibility(PlayerNumber playerNumber, MonumentComponent monumentComponent, MonumentComponentVisibility monumentComponentVisibility)
    {
        MonumentDisplay monumentDisplay = _monumentsByPlayerNumber[playerNumber];
        if(monumentComponentVisibility == MonumentComponentVisibility.Hidden)
        {
            monumentDisplay.SetComponentVisibility(monumentComponent, MonumentComponentVisibility.Hidden);
        }
        else if(monumentComponentVisibility == MonumentComponentVisibility.InProgress)
        {
            Debug.Log($"have different visibility here");
            monumentDisplay.SetComponentVisibility(monumentComponent, MonumentComponentVisibility.InProgress);
        }
        else
        {
            monumentDisplay.SetComponentVisibility(monumentComponent, MonumentComponentVisibility.Complete);
        }
    }
}
