using UnityEngine;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
	[SerializeField] protected Image _image;
	[SerializeField] protected MainTabButtonContainer _mainTabButtonContainer;
	[SerializeField] public UITabContainer TabContainer { get; private set; }

	private void Awake()
	{
		if (_image == null)
		{
			Debug.LogError($"Cannot find image on {gameObject.name}");
		}

		if (_mainTabButtonContainer == null)
		{
			Debug.LogError($"Cannot find mainTabButtonContainer on {gameObject.name}");
		}
	}

	public void Initialise(UITabContainer uiTabContainer)
    {
		TabContainer = uiTabContainer;
	}

	public void OnClick()
	{
		if (GameActionHandler.CurrentGameActionSequence != null) return;

		NavigationManager.Instance.SetTab(this);
    }

	public Image GetImage()
	{
		return _image;
	}

	public void Activate()
	{
		_image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
		TabContainer.Activate();
	}

	public void Deactivate()
	{
		_image.color = ColourUtility.GetColour(ColourType.Empty);
		TabContainer.Deactivate();
	}

}