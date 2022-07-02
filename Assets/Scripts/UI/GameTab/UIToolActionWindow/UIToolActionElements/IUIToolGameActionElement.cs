using UnityEngine;

public interface IUIToolGameActionElement
{
    Transform GetTransform();
    GameObject GetGameObject();
    void Initialise(IUIToolGameActionStep uiToolGameActionStep);
}
