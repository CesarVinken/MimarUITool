using UnityEngine;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
	[SerializeField] protected Image _image;
	[SerializeField] protected MainTabButtonContainer _mainTabButtonContainer;
	[SerializeField] protected UITabContainer _tabContainer;

	private void Awake()
	{
		if (_image == null)
		{
			Debug.LogError($"Cannot find image on {gameObject.name}");
		}

		if (_tabContainer == null)
		{
			Debug.LogError($"Cannot find tabContainer on {gameObject.name}");
		}

		if (_mainTabButtonContainer == null)
		{
			Debug.LogError($"Cannot find mainTabButtonContainer on {gameObject.name}");
		}
	}

	public void OnClick()
    {
		NavigationManager.Instance.SetTab(this);
    }

	public Image GetImage()
	{
		return _image;
	}

	public void Activate()
	{
		_image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
		_tabContainer.Activate();
	}

	public void Deactivate()
	{
		_image.color = ColourUtility.GetColour(ColourType.Empty);
		_tabContainer.Deactivate();
	}
}