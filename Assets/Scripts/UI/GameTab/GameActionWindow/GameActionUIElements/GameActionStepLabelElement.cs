using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameActionStepLabelElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private TextMeshProUGUI _label;

    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    private void Awake()
    {
        if (_label == null)
        {
            Debug.LogError($"could not find label on {gameObject.name}");
        }
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        if (uiToolGameActionStep is TestStep)
        {
            _label.text = "TBA";
        }
        if (uiToolGameActionStep is CheckoutStep)
        {
            _label.text = "Checkout";
        }
        else if(uiToolGameActionStep is PlayerPickStep)
        {
            _label.text = "Select player";
        }
        else if (uiToolGameActionStep is GameActionPickStep)
        {
            Player player = GameActionHandler.CurrentGameActionSequence.GameActionCheckSum.Player;

            if(player == null)
            {
                Debug.LogError($"Could not find the required player during the {uiToolGameActionStep.GetType()} step");
            }
            _label.text = $"Select an action for {player.Name}";
        }
        else
        {
            _label.text = "UNKNOWN STEP TYPE";
        }
    }

}
