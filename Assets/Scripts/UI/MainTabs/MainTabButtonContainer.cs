using UnityEngine;

public class MainTabButtonContainer : MonoBehaviour
{
    public void OpenTab(UITabButton tab)
    {
        NavigationManager.Instance.SetTab(tab);
    }
}
