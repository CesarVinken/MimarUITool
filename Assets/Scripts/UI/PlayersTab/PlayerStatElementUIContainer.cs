using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatElementUIContainer : MonoBehaviour
{
    protected PlayerUIContent _playerUIContentContainer;
    [SerializeField] protected Image _icon;
    [SerializeField] protected TMP_InputField _currentAmountInputField;

    protected Player _player;
    protected IPlayerStat _playerStat;

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


    

    public void OnStatElementInputFieldChange()
    {
        _playerUIContentContainer.SetPlayerStatInputFieldValue(_currentAmountInputField, _playerStat);
    }

    protected void SetCurrentAmountDisplay(int newAmount)
    {
        _currentAmountInputField.text = newAmount.ToString();
    }


}

