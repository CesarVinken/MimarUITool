using System.Collections.Generic;
using UnityEngine;

public class UIToolActionWindow : MonoBehaviour
{
    [SerializeField] private Transform _stepLabelContainer;
    [SerializeField] private Transform _nextStepButtonContainer;

    private List<GameObject> _spawnedUIElements = new List<GameObject>();

    private void Awake()
    {
        if (_stepLabelContainer == null)
        {
            Debug.LogError($"Could not find _stepLabelContainer");
        }
        if (_nextStepButtonContainer == null)
        {
            Debug.LogError($"Could not find _nextStepButtonContainer");
        }
    }

    public void LoadStepUI(IUIToolGameActionStep newStep)
    {
        Debug.Log($"Load new step: {newStep.GetType()}");
        for (int i = _spawnedUIElements.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(_spawnedUIElements[i]);
        }

        List<IUIToolGameActionElement> elements = newStep.Initialise();

        for (int j = 0; j < elements.Count; j++)
        {
            Debug.Log($"add element on step");
            IUIToolGameActionElement element = elements[j];
            SetParentForElement(element);
            _spawnedUIElements.Add(element.GetGameObject());
        }
    }

    private void SetParentForElement(IUIToolGameActionElement element)
    {
        Transform elementTransform = element.GetTransform();
        if (element is UIToolActionNextStepButtonElement) // todo: make switch
        {
            elementTransform.SetParent(_nextStepButtonContainer);
            elementTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else if(element is UIToolActionStepLabelElement)
        {
            elementTransform.SetParent(_stepLabelContainer);
            RectTransform rect = elementTransform.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(0, 0);
        }
        else
        {
            Debug.LogError($"Unknown element type of {element.GetType()}");
        }
    }

    public void EmptyWindowUI()
    {
        for (int i = _spawnedUIElements.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedUIElements[i]);
        }
    }

    public void DestroyWindow()
    {
        Destroy(gameObject);
    }
}
