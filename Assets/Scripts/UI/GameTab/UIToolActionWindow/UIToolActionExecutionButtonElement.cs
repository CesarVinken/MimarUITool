using UnityEngine;

public class UIToolActionExecutionButtonElement : MonoBehaviour, IUIToolGameActionElement
{
    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

}