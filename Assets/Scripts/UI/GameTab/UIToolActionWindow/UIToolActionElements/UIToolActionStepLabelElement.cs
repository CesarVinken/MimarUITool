using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIToolActionStepLabelElement : MonoBehaviour, IUIToolGameActionElement
{
    [SerializeField] private TextMeshProUGUI _label;

    private UIToolGameActionHandler _gameActionHandler = null;

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

    public void Initialise(IUIToolGameActionStep uiToolGameActionStep)
    {
        if (uiToolGameActionStep is TestStep)
        {
            _label.text = "TBA";
        }
        else if(uiToolGameActionStep is PlayerPickStep)
        {
            _label.text = "Select player";
        }
        else
        {
            _label.text = "UNKNOWN STEP TYPE";
        }
    }

}
