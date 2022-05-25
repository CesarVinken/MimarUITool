using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class WorkerTile : MonoBehaviour
{
    public IWorker Worker;
    protected LocationType _locationType = LocationType.Constantinople;

    [SerializeField] protected Image _tileBackground;
    [SerializeField] protected TextMeshProUGUI _statusText;
    [SerializeField] protected TMP_InputField _serviceLengthInputField;

    public void OnChangeServiceLengthInputField()
    {
        int newContractLength = int.Parse(_serviceLengthInputField.text);

        // The user should not be allowed to set a contract to anything less than 1 in the input field. Because 0 would effectively mean the contract has ended, and should assign the employer too None.
        if(newContractLength <= 0)
        {
            _serviceLengthInputField.text = "1";
        }

        Worker.SetServiceLength(newContractLength);
    }


    public abstract void Initialise(LocationType locationType, IWorker worker);

    protected void SetEmployer(PlayerNumber newEmployer)
    {
        if(newEmployer == PlayerNumber.None)
        {
            _statusText.gameObject.SetActive(false);
            _serviceLengthInputField.gameObject.SetActive(false);
        }
        else
        {
            _statusText.gameObject.SetActive(true);
            _serviceLengthInputField.gameObject.SetActive(true);
        }
        Worker.SetEmployer(newEmployer);
    }

    public void Destroy()
    {
        if(Worker.Employer != PlayerNumber.None)
        {
            PlayerManager.Instance.Players[Worker.Employer].RemoveWorker(Worker);
        }

        Destroy(gameObject);
    }
}
