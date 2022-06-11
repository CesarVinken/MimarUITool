using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class WorkerTile : MonoBehaviour
{
    public IWorker Worker;
    protected LocationType _locationType = LocationType.Constantinople;

    [SerializeField] protected Image _tileBackground;
    [SerializeField] protected Image _workerIcon;
    //[SerializeField] protected TextMeshProUGUI _statusText;
    [SerializeField] protected TMP_InputField _serviceLengthInputField;

    // called when changing the input field
    public void OnChangeServiceLengthInputField()
    {
        int newContractLength = 1;

        if (int.TryParse(_serviceLengthInputField.text, out int result))
        {
            newContractLength = result;
        }

        UpdateServiceLength(newContractLength);    
    }

    protected void UpdateServiceLength(int newContractLength)
    {
        // The user should not be allowed to set a contract to anything less than 1 in the input field. Because 0 would effectively mean the contract has ended, and should assign the employer too None.
        if (newContractLength <= 0)
        {
            newContractLength = 1;
        }

        _serviceLengthInputField.text = newContractLength.ToString();
        Worker.SetServiceLength(newContractLength);
    }

    public void SubtractServiceLength()
    {
        int newContractLength = Worker.ServiceLength - 1;

        if (newContractLength <= 0)
        {
            SetWorkerToNeutral();
        }
        else
        {
            UpdateServiceLength(newContractLength);
        }
    }

    protected abstract void SetWorkerToNeutral();

    public abstract void Initialise(LocationType locationType, IWorker worker);

    public abstract void SetEmployer(PlayerNumber newEmployer);

    public void Destroy()
    {
        if(Worker.Employer != PlayerNumber.None)
        {
            PlayerManager.Instance.Players[Worker.Employer].RemoveWorker(Worker);
        }

        Destroy(gameObject);
    }
}
