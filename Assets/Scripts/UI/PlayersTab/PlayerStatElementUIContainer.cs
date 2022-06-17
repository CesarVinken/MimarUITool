using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatElementUIContainer : MonoBehaviour
{
    private PlayerUIContent _playerUIContentContainer;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_InputField _currentAmountInputField;
    [SerializeField] private TextMeshProUGUI _incomeProjectionLabel;
    //private ResourceType _resourceType;
    private Player _player;
    private IPlayerStat _playerStat;

    private void Awake()
    {
        if (_currentAmountInputField == null)
        {
            Debug.LogError($"could not find currentAmountInputField");
        }
        if (_icon == null)
        {
            Debug.LogError($"could not find _icon on {gameObject.name}");
        }

        _currentAmountInputField.onValueChanged.AddListener(delegate { OnStatElementInputFieldChange(); });
    }

    public void Initialise(PlayerUIContent playerUIContentContainer, IPlayerStat playerStatType)
    {
        _playerUIContentContainer = playerUIContentContainer;
        _playerStat = playerStatType;
    }

    public void UpdateUI(Player player, IPlayerStat playerStat)
    {
        _player = player;
        _playerStat = playerStat;

        SetCurrentAmountDisplay(playerStat.Amount);
        if(playerStat is IResource || playerStat is Gold)
        {
            _playerUIContentContainer.CalculateIncomeProjectionLabel(this, playerStat);
        }
    }

    public void OnStatElementInputFieldChange()
    {
        _playerUIContentContainer.SetPlayerStatInputFieldValue(_currentAmountInputField, _playerStat);
    }

    private void SetCurrentAmountDisplay(int newAmount)
    {
        _currentAmountInputField.text = newAmount.ToString();
    }

    public void SetIncomeProjectionLabel(string labelText)
    {
        _incomeProjectionLabel.text = labelText;
    }
}

