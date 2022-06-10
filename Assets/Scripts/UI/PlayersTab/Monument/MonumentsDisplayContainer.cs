using System.Collections.Generic;
using UnityEngine;

public class MonumentsDisplayContainer : MonoBehaviour
{
    [SerializeField] private MonumentDisplay _player1Monument;
    [SerializeField] private MonumentDisplay _player2Monument;
    [SerializeField] private MonumentDisplay _player3Monument;

    private Dictionary<PlayerNumber, MonumentDisplay> _monumentsByPlayerNumber = new Dictionary<PlayerNumber, MonumentDisplay>();
    private MonumentDisplay _currentlyShownMonument = null;

    private void Awake()
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
            if(_currentlyShownMonument.Player.PlayerNumber == playerNumber)
            {
                return;
            }

            _currentlyShownMonument.gameObject.SetActive(false);
        }

        _monumentsByPlayerNumber[playerNumber].UpdateComponentsVisibility();

        _monumentsByPlayerNumber[playerNumber].gameObject.SetActive(true);
        _currentlyShownMonument = _monumentsByPlayerNumber[playerNumber];
    }

    public void ShowMonumentComponent(PlayerNumber playerNumber, MonumentComponent monumentComponent)
    {
        MonumentDisplay monumentDisplay = _monumentsByPlayerNumber[playerNumber];
        monumentDisplay.ShowComponent(monumentComponent);
    }

    public void HideMonumentComponent(PlayerNumber playerNumber, MonumentComponent monumentComponent)
    {
        MonumentDisplay monumentDisplay = _monumentsByPlayerNumber[playerNumber];
        monumentDisplay.HideComponent(monumentComponent);
    }
}
