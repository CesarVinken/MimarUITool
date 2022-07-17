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
        else if (uiToolGameActionStep is CheckoutStep)
        {
            _label.text = "Checkout";
        }
        else if (uiToolGameActionStep is PickTravelLocationStep)
        {
            _label.text = "Select a destination";
        }
        else if (uiToolGameActionStep is PickWorkerGameActionStep)
        {
            _label.text = "Select a worker";
        }
        else if (uiToolGameActionStep is PickHiringLocationStep)
        {
            _label.text = "Select a location";
        }
        else if (uiToolGameActionStep is PickConstructionSiteUpgradeStep)
        {
            _label.text = "Select an upgrade";
        }
        else if (uiToolGameActionStep is PlayerPickStep)
        {
            _label.text = "Select player";
        }
        else if (uiToolGameActionStep is SetHiringTermStep)
        {
            _label.text = "Set a contract length";
        }
        else if (uiToolGameActionStep is PickGameActionStep)
        {
            Player player = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;

            if(player == null)
            {
                Debug.LogError($"Could not find the required player during the {uiToolGameActionStep.GetType()} step");
            }
            _label.text = $"Select an action for {player.Name}";
        }
        else
        {
            Debug.LogWarning($"UNKNOWN STEP TYPE { uiToolGameActionStep.GetType()}");
            _label.text = $"UNKNOWN STEP TYPE {uiToolGameActionStep.GetType()}";
        }
    }

}
