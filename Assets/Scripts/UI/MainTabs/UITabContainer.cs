using UnityEngine;

public abstract class UITabContainer : MonoBehaviour
{
    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
