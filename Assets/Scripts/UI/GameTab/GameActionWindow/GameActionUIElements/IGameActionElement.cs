using UnityEngine;

public interface IGameActionElement
{
    Transform GetTransform();
    GameObject GetGameObject();
    void Initialise(IGameActionStep gameActionStep);
}
