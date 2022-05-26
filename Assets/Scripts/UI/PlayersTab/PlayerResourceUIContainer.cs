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

        _currentAmountInputField.onValueChanged.AddListener(delegate { OnResourceInputFieldChange(); });

    }

    public void Initialise(ResourceType resourceType)
    {
        _resourceType = resourceType;
    }

    public void UpdateUI(Player player, IResource resource)
    {
        _player = player;

        SetCurrentResourceDisplay(resource.Amount);
        SetIncomeProjectionLabel();
    }

    public void OnResourceInputFieldChange()
    {
        if (string.IsNullOrWhiteSpace(_currentAmountInputField.text) || int.Parse(_currentAmountInputField.text) == 0)
        {
            SetCurrentResourceDisplay(0);
            _player.SetResource(_resourceType, 0);
            return;
        }
        
        int newAmount = int.Parse(_currentAmountInputField.text);

        if (newAmount > _player.StockpileMaximum)
        {
            newAmount = _player.StockpileMaximum;
            SetCurrentResourceDisplay(newAmount);
        }
        _player.SetResource(_resourceType, newAmount);
    }

    private void SetCurrentResourceDisplay(int newAmount)
    {
        //if(newAmount > _player.StockpileMaximum)
        //{
        //    newAmount = _player.StockpileMaximum;
        //    //_player.SetResource(_resourceType, newAmount);
        //}
        Debug.Log($"newAmount of {_resourceType} is {newAmount}");

        _currentAmountInputField.text = newAmount.ToString();
    }

    private void SetIncomeProjectionLabel()
    {
        int projectedResourceIncome = StatCalculator.CalculateResourceIncome(_resourceType, _player);
        _incomeProjectionLabel.text = $"+{projectedResourceIncome}";
    }
}
