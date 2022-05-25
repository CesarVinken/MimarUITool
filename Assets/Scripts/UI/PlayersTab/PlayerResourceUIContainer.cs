using TMPro;
using UnityEngine;

public class PlayerResourceUIContainer : MonoBehaviour
{
    [SerializeField] private TMP_InputField _currentAmountInputField;
    [SerializeField] private TextMeshProUGUI _incomeProjectionLabel;
    private ResourceType _resourceType;
    private Player _player;

    private void Awake()
    {
        if (_currentAmountInputField == null)
        {
            Debug.LogError($"could not find currentAmountInputField");
        }
        if (_incomeProjectionLabel == null)
        {
            Debug.LogError($"could not find  incomeProjectionLabel");
        }
    }

    public void Initialise(ResourceType resourceType)
    {
        _resourceType = resourceType;
    }

    public void FillInPlayerContent(UIPlayerData uiPlayerData)
    {
        _player = uiPlayerData.Player;

        SetResource(uiPlayerData.Player.Resources[_resourceType]);
    }

    public void SetResource(int newAmount)
    {
        if(newAmount > _player.StockpileMaximum)
        {
            newAmount = _player.StockpileMaximum;
        }

        _currentAmountInputField.text = newAmount.ToString();
    }

    public void OnResourceInputFieldChange()
    {
        if (string.IsNullOrWhiteSpace(_currentAmountInputField.text) || int.Parse(_currentAmountInputField.text) == 0)
        {
            _currentAmountInputField.text = "0";
        }

        int newAmount = int.Parse(_currentAmountInputField.text);

        if (newAmount > _player.StockpileMaximum)
        {
            newAmount = _player.StockpileMaximum;
            _currentAmountInputField.text = newAmount.ToString();
        }

        _player.SetResource(_resourceType, newAmount);
    }
}
