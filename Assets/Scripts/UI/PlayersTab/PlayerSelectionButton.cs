using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionButton : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private TextMeshProUGUI _buttonText;
	[SerializeField] private PlayersTabContainer _playersTab;
	public PlayerNumber PlayerNumber { get; private set; } = PlayerNumber.None;

	private void Awake()
	{
		if (_image == null)
		{
			Debug.LogError($"Cannot find image on {gameObject.name}");
		}

		if (_buttonText == null)
		{
			Debug.LogError($"Cannot find text on {gameObject.name}");
		}

		if (_playersTab == null)
		{
			Debug.LogError($"Cannot find _playersTab on {gameObject.name}");
		}
	}

	public void Initialise(PlayerNumber playerNumber)
    {
		PlayerNumber = playerNumber;
		Player player = PlayerManager.Instance.Players[PlayerNumber];
		_buttonText.text = player.Name;
	}

	public void OnClick()
    {
        _playersTab.SetPlayerTab(this);
    }

	public void Activate()
	{
		_image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
		UpdatePlayerDataDisplay();
	}

	public void UpdatePlayerDataDisplay()
    {
		UIPlayerData playerData = PlayerManager.Instance.GetPlayerData(PlayerNumber);
		_playersTab.UpdatePlayerStatUIContent(playerData);
		_playersTab.UpdateMonumentUI(); // The UI of the monument (component buttons) should be strictly separated from the display of the monument (the 3d model)
		_playersTab.UpdateMonumentDisplay();
	}

	public void Deactivate()
	{
		_image.color = ColourUtility.GetColour(ColourType.Empty);
	}
}
