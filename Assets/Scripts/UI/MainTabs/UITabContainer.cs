using UnityEngine;

public abstract class UITabContainer : MonoBehaviour
{
    public abstract MainTabType MainTabType { get; }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
